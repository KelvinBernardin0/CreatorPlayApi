using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.LogActivity.Commands.LogActivityCreate;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Authentication.Commands.AuthenticationCreate;

public class LogActivityCreateCommandHandler(ILogger<LogActivityCreateCommandHandler> logger,
											 ICreatorPlayContext context,
											 UserManager<ApplicationUser> userManager) : IRequestHandler<LogActivityCreateCommandRequest, ResponseApi<LogActivityCreateCommandResponse>>
{
	private readonly ILogger<LogActivityCreateCommandHandler> _logger = logger;
	private readonly ICreatorPlayContext _context = context;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<ResponseApi<LogActivityCreateCommandResponse>> Handle(LogActivityCreateCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<LogActivityCreateCommandResponse>();

		try
		{
			var user = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
			if (user == null)
			{
				response.SetError(new ResponseError(TypeError.UserNotFound, TypeError.UserNotFound.GetDescription()), HttpStatusCode.Unauthorized.GetHashCode());
				return response;
			}

			var userRoles = await _userManager.GetRolesAsync(user);
			var newLog = await _context.LogActivity.AddAsync(new Domain.Entities.LogActivity(request.LogLevel, user.Email, userRoles.First(), request.Message), cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			response.SetSuccess(new LogActivityCreateCommandResponse(true), HttpStatusCode.OK.GetHashCode());	
			return response;
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(LogActivityCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
			response.SetSuccess(new LogActivityCreateCommandResponse(false), HttpStatusCode.OK.GetHashCode());
		}
		return response;
	}
}
