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

public class TeamMemberCreateCommandHandler(ILogger<TeamMemberCreateCommandHandler> logger, ICreatorPlayContext context,
												 UserManager<ApplicationUser> userManager) : IRequestHandler<TeamMemberCreateCommandRequest, ResponseApi<TeamMemberCreateCommandResponse>>
{
	private readonly ILogger<TeamMemberCreateCommandHandler> _logger = logger;
	private readonly ICreatorPlayContext _context = context;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<ResponseApi<TeamMemberCreateCommandResponse>> Handle(TeamMemberCreateCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<TeamMemberCreateCommandResponse>();

		try
		{
			var newTeamMember = new Domain.Entities.TeamMember();

			if (request.IsLeader)
			{
				newTeamMember.AddTeamMember(request.TeamId, request.UserId, request.IsLeader);
			}
			else
			{
				if (!Extensions.IsValidEmail(request.UserEmail))
				{
					response.SetError(new ResponseError(TypeError.InvalidEmail, TypeError.InvalidEmail.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
					return response;
				}

				var user = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.Email == request.UserEmail, cancellationToken);
				if (user != null)
				{
					var memberAlreadyExists = await _context.TeamMember.AnyAsync(x => x.UserId == user.Id && x.TeamId == request.TeamId, cancellationToken);
					if (memberAlreadyExists)
					{
						response.SetError(new ResponseError(TypeError.MemberAlreadyExistsInTeam, TypeError.MemberAlreadyExistsInTeam.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
						return response;
					}
					newTeamMember.AddTeamMember(request.TeamId, user.Id);
				}
				else
				{
					response.SetError(new ResponseError(TypeError.EmailNotFound, TypeError.EmailNotFound.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
					return response;
				}
			}

			await _context.TeamMember.AddAsync(newTeamMember, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			response.SetSuccess(new TeamMemberCreateCommandResponse("Membro da equipe adicinado com sucesso!"), HttpStatusCode.Created.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(TeamMemberCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}

		return response;
	}
}
