using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Users.Commands.UsersCreate;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

			var user = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
			if (user != null)
			{
				var error = await ValidateRequest(request, cancellationToken);
				if (error == null)
				{

					user.Email = request.Email;
					var usuarioSenhaAlterada = await _userManager.ChangePasswordAsync(user, request.Password, request.PasswordConfirmation);

					if (!usuarioSenhaAlterada.Succeeded)
					{
						var tiposErros = Extensions.GetEnumValues<TypeError>();
						foreach (var tipoErro in tiposErros)
						{
							if (usuarioSenhaAlterada.Errors.Any(x => x.Code == tipoErro.ToString()))
							{
								response.SetError(new ResponseError(tipoErro, tipoErro.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
								return response;
							}
						}
					}


					_context.ApplicationUser.Update(user);
					await _context.SaveChangesAsync(cancellationToken);

					response.SetSuccess(new UsersUpdateCommandResponse($"Usuário atualizado encontrado: {user.Email}"), HttpStatusCode.OK.GetHashCode());
				}
				else
					response.SetError(new ResponseError(error.Value, error.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
			}
			else
			{
				response.SetError(new ResponseError(99, $"Usuário não encontrado. ID: {request.Id}"));
				return response;
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"Erro in {nameof(UsersUpdateCommandHandler)}. Request: {request.ToIndentedJson()}");
			response.SetError(new ResponseError(99, "Ocorreu um erro interno."), HttpStatusCode.Unauthorized.GetHashCode());
		}

		return response;
	}

	private async Task<TypeError?> ValidateRequest(UsersUpdateCommandRequest request, CancellationToken cancellationToken)
	{

		if (!Extensions.IsValidEmail(request.Email))
			return TypeError.InvalidEmail;

		if (request.Password != request.PasswordConfirmation)
			return TypeError.ConfirmPasswordNotEqual;

		return null;
	}
}
