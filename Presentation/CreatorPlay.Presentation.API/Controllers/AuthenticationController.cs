using CreatorPlay.Application.Authentication.Commands.AuthenticationCreate;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Presentation.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PortalDeBeneficiosApi.Presentation.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthenticationController(IMediator mediator) : BaseController
{
	private readonly IMediator _mediator = mediator;

	[HttpPost("Autenticar")]
	[ProducesResponseType(typeof(AuthenticationCreateCommandResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> AuthenticationUserAsync([FromBody] AuthenticationCreateCommandRequest request)
	{
		var response = await _mediator.Send(request);
		return StatusCode(response.HttpStatusCode, response.GetResultData);
	}
}
