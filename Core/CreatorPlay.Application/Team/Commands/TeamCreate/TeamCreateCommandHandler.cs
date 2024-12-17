using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.TeamMember.Commands.TeamMemberCreate;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Team.Commands.TeamCreate;

public class TeamCreateCommandHandler(ILogger<TeamCreateCommandHandler> logger, ICreatorPlayContext context, IMediator mediator) : IRequestHandler<TeamCreateCommandRequest, ResponseApi<TeamCreateCommandResponse>>
{
	private readonly ILogger<TeamCreateCommandHandler> _logger = logger;
	private readonly ICreatorPlayContext _context = context;
	private readonly IMediator _mediator = mediator;

	public async Task<ResponseApi<TeamCreateCommandResponse>> Handle(TeamCreateCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<TeamCreateCommandResponse>();
		var IsLeader = false;
		try
		{
			var requestError = ValidateRequest(request);
			if (requestError == null)
			{
				if (string.IsNullOrEmpty(request.LeaderId)){
					request.LeaderId =  request.Creator;
					IsLeader = true;
				}
					

				var newTeam = new Domain.Entities.Team(request.Name, request.LeaderId,request.Description,request.Creator);
				

                var teamAlreadyExists = await _context.Team.AnyAsync(x => x.Name == request.Name,  cancellationToken);
				if(teamAlreadyExists)
                {
                    response.SetError(new ResponseError(TypeError.TeamAlreadyExistsInTeam, TypeError.TeamAlreadyExistsInTeam.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
                    return response;
                }			

                // newTeam.AddTeam(request.Name, request.LeaderId,request.Description,request.Creator,DateTime.UtcNow);

                await _context.Team.AddAsync(newTeam, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);

				await _mediator.Send(new TeamMemberCreateCommandRequest(newTeam.LeaderId, newTeam.Id, IsLeader, default), cancellationToken);

				response.SetSuccess(new TeamCreateCommandResponse(newTeam.Id), HttpStatusCode.Created.GetHashCode());
			}
			else
				response.SetError(new ResponseError(requestError.Value, requestError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(TeamCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}

		return response;
	}

	private static TypeError? ValidateRequest(TeamCreateCommandRequest request)
	{
		if (!request.Name.HasValue())
			return TypeError.TeamNameRequired;

		if (!request.Creator.HasValue())
			return TypeError.TeamCreatorRequired;

		return null;
	}
}
