using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using CreatorPlay.Application.Common.Models.Response;

namespace CreatorPlay.Presentation.API.Controllers;

public class BaseController : ControllerBase
{
	protected JwtDataUser JwtUserData()
	{
		try
		{
			string authorizationHeader = Request.Headers.Authorization;
			string? token = authorizationHeader?["Bearer ".Length..].Trim();

			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadJwtToken(token);

			var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "id")?.Value?.ToString();
			var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "userName")?.Value?.ToString();
			var email = jwtToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value?.ToString();
			var phoneNumber = jwtToken.Claims.FirstOrDefault(x => x.Type == "telefone")?.Value?.ToString();

			var user = new JwtDataUser
			{
				Id = userId,
				UserName = userName,
				Email = email,
				PhoneNumber = phoneNumber
			};

			return user;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}
}
