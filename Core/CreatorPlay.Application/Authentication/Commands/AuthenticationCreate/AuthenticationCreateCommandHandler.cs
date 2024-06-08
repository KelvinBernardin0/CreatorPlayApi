using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CreatorPlay.Application.Authentication.Commands.AuthenticationCreate;

public class AuthenticationCreateCommanddHandler(ILogger<AuthenticationCreateCommanddHandler> logger,
												UserManager<ApplicationUser> userManager) : IRequestHandler<AuthenticationCreateCommandRequest, ResponseApi<AuthenticationCreateCommandResponse>>
{
	private readonly ILogger<AuthenticationCreateCommanddHandler> _logger = logger;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<ResponseApi<AuthenticationCreateCommandResponse>> Handle(AuthenticationCreateCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<AuthenticationCreateCommandResponse>();

		try
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user != null)
			{
				var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
				if (!passwordValid)
				{
					response.SetError(new ResponseError(TypeError.IncorrectPassword, TypeError.IncorrectPassword.GetDescription()), HttpStatusCode.Unauthorized.GetHashCode());
					return response;
				}

				var userRoles = await _userManager.GetRolesAsync(user);
				if (!userRoles.Any())
				{
					response.SetError(new ResponseError(TypeError.RoleNotFound, TypeError.RoleNotFound.GetDescription()), HttpStatusCode.Unauthorized.GetHashCode());
					return response;
				}

				var claims = new List<Claim>
				{
					new("id", user.Id),
					new("email", user.Email),
					new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new(ClaimTypes.Role, userRoles.First())
				};

				var jwtSecurityToken = GetToken(claims);
				var stringJWTToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
				response.SetSuccess(new AuthenticationCreateCommandResponse(stringJWTToken, jwtSecurityToken.ValidTo.ToUnixTimestamp()), HttpStatusCode.OK.GetHashCode());
				return response;
			}
			else
				response.SetError(new ResponseError(TypeError.UserNotFound, TypeError.UserNotFound.GetDescription()), HttpStatusCode.Unauthorized.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(AuthenticationCreateCommanddHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}
		return response;
	}

	private static JwtSecurityToken GetToken(List<Claim> claims)
	{
		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Secret));

		return new JwtSecurityToken(issuer: Configuration.ValidIssuer,
										 audience: Configuration.ValidAudience,
										 expires: DateTime.Now.AddMinutes(Configuration.Expiration),
										 claims: claims,
										 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
	}
}
