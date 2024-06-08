using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Users.Commands.UsersUpdate;

public class UsersUpdateCommandHandler(ILogger<UsersUpdateCommandHandler> _logger, ICreatorPlayContext _context, UserManager<ApplicationUser> userManager) : IRequestHandler<UsersUpdateCommandRequest, ResponseApi<UsersUpdateCommandResponse>>
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<ResponseApi<UsersUpdateCommandResponse>> Handle(UsersUpdateCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<UsersUpdateCommandResponse>();

		try
		{
			var user = await _userManager.FindByIdAsync(request.Id);
			if (user != null)
			{
				var requestError = ValidateRequest(request);
				if (requestError == null)
				{
					user.Email = request.Email;
					user.ModifiedAt = DateTime.Now;

					var changePassword = await _userManager.ChangePasswordAsync(user, request.Password, request.PasswordConfirmation);

					if (!changePassword.Succeeded)
					{
						var typeErrors = Extensions.GetEnumValues<TypeError>();
						foreach (var typeError in typeErrors)
						{
							if (changePassword.Errors.Any(x => x.Code == typeError.ToString()))
							{
								response.SetError(new ResponseError(typeError, typeError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
								return response;
							}
						}
					}

					_context.ApplicationUser.Update(user);
					await _context.SaveChangesAsync(cancellationToken);

					response.SetSuccess(new UsersUpdateCommandResponse($"Usuário {user.Email} atualizado com sucesso!"), HttpStatusCode.OK.GetHashCode());
				}
				else
					response.SetError(new ResponseError(requestError.Value, requestError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
			}
			else
				response.SetError(new ResponseError(TypeError.UserNotFound, $"Usuário não encontrado. ID: {request.Id}"));
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(UsersUpdateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}

		return response;
	}

	private static TypeError? ValidateRequest(UsersUpdateCommandRequest request)
	{
		if (!Extensions.IsValidEmail(request.Email))
			return TypeError.InvalidEmail;

		if (request.Password != request.PasswordConfirmation)
			return TypeError.ConfirmPasswordNotEqual;

		return null;
	}
}
