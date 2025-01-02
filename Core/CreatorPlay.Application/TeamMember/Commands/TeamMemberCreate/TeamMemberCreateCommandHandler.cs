using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.TeamMember.Commands.TeamMemberCreate;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Team.Commands.TeamCreate;

public class TeamMemberCreateCommandHandler(ILogger<TeamMemberCreateCommandHandler> logger, ICreatorPlayContext context) : IRequestHandler<TeamMemberCreateCommandRequest, ResponseApi<TeamMemberCreateCommandResponse>>
{
	private readonly ILogger<TeamMemberCreateCommandHandler> _logger = logger;
	private readonly ICreatorPlayContext _context = context;

	public async Task<ResponseApi<TeamMemberCreateCommandResponse>> Handle(TeamMemberCreateCommandRequest request, CancellationToken cancellationToken)
	{

		var response = new ResponseApi<TeamMemberCreateCommandResponse>();

		try
		{

var teamLeader = await _context.TeamMember.FirstOrDefaultAsync(x=>x.TeamId==request.TeamId && x.IsLeader==true , cancellationToken);
if (teamLeader== null  || teamLeader.UserId== request.RequestUserId){

			var newTeamMember = new Domain.Entities.TeamMember();

				
                var user = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.Id == request.UserId || x.Email==request.UserEmail, cancellationToken);

				
				
				if (user != null)
				{
					var memberAlreadyExists = await _context.TeamMember.AnyAsync(x => x.UserId == user.Id && x.TeamId == request.TeamId, cancellationToken);
					if (memberAlreadyExists)
					{
						response.SetError(new ResponseError(TypeError.MemberAlreadyExistsInTeam, TypeError.MemberAlreadyExistsInTeam.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
						return response;
					}
					var leaderAlreadyExists = await _context.TeamMember.AnyAsync(x => x.IsLeader ==true && x.TeamId == request.TeamId, cancellationToken);
					if (request.IsLeader && leaderAlreadyExists)
					{
						response.SetError(new ResponseError(TypeError.LeaderAlreadyExistsInTeam, TypeError.LeaderAlreadyExistsInTeam.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
						return response;
					}
					newTeamMember.AddTeamMember(request.TeamId, user.Id, request.IsLeader);
					await _context.TeamMember.AddAsync(newTeamMember, cancellationToken);
					await _context.SaveChangesAsync(cancellationToken);	
					response.SetSuccess(new TeamMemberCreateCommandResponse("Membro da equipe adicinado com sucesso!"), HttpStatusCode.Created.GetHashCode());

				}
				else
				{
					response.SetError(new ResponseError(TypeError.EmailNotFound, TypeError.EmailNotFound.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
					return response;
				}
			
}else{
					response.SetError(new ResponseError(TypeError.NotTeamLeader, TypeError.NotTeamLeader.GetDescription()), HttpStatusCode.Forbidden.GetHashCode());
					return response;
}

		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(TeamMemberCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}

		return response;
	}
}
