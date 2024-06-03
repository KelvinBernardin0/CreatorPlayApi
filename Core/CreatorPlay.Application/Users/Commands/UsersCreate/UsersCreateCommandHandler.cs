using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Users.Commands.UsersCreate;

public class UsersCreateCommandHandler(ILogger<UsersCreateCommandHandler> logger,
									   ICreatorPlayContext context,
									   UserManager<ApplicationUser> userManager) : IRequestHandler<UsersCreateCommandRequest, ResponseApi<UsersCreateCommandResponse>>
{
	private readonly ILogger<UsersCreateCommandHandler> _logger = logger;
	private readonly ICreatorPlayContext _context = context;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<ResponseApi<UsersCreateCommandResponse>> Handle(UsersCreateCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<UsersCreateCommandResponse>();

		try
		{
			var error = await ValidateRequest(request, cancellationToken);
			if (error == null)
			{
				var newUser = new ApplicationUser
				{
					UserName = request.Email,
					Email = request.Email
				};

				var userCreated = await _userManager.CreateAsync(newUser, request.Password);
				if (userCreated.Succeeded)
				{
					var roleCreated = request.Role == Roles.Default_Access ? await _userManager.AddToRoleAsync(newUser, Roles.Default_Access.ToString())
																		   : await _userManager.AddToRoleAsync(newUser, Roles.Commercial_Access.ToString());

					if (roleCreated.Succeeded)
					{
						response.SetSuccess(new UsersCreateCommandResponse("Cadastro realizado com sucesso!"), HttpStatusCode.Created.GetHashCode());
					}
					else
					{
						var typeErros = Extensions.GetEnumValues<TypeError>();

						foreach (var typeError in typeErros)
						{
							if (roleCreated.Errors.Any(x => x.Code == typeError.ToString()))
							{
								response.SetError(new ResponseError(typeError, typeError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
								break;
							}
						}
					}
				}
				else
				{
					var typeErros = Extensions.GetEnumValues<TypeError>();
					foreach (var typeError in typeErros)
					{
						if (userCreated.Errors.Any(x => x.Code == typeError.ToString()))
						{
							response.SetError(new ResponseError(typeError, typeError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
							break;
						}
					}
				}
			}
			else
				response.SetError(new ResponseError(error.Value, error.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError("{ex},{message}", ex, ex.Message);
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.Unauthorized.GetHashCode());
		}

		return response;
	}

	private async Task<TypeError?> ValidateRequest(UsersCreateCommandRequest request, CancellationToken cancellationToken)
	{

		if (!Extensions.IsValidEmail(request.Email))
			return TypeError.InvalidEmail;

		var duplicateEmail = await _context.ApplicationUser.AnyAsync(x => x.Email == request.Email, cancellationToken);
		if (duplicateEmail)
			return TypeError.DuplicateEmail;

		if (request.Password != request.PasswordConfirmation)
			return TypeError.ConfirmPasswordNotEqual;

		return null;
	}
}
