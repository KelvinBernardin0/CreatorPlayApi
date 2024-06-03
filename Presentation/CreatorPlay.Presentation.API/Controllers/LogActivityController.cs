using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CreatorPlay.Application.LogActivity.Commands.LogActivityCreate;

namespace CreatorPlay.Presentation.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class LogActivityController(IMediator mediator) : BaseController
{
	private readonly IMediator _mediator = mediator;

	[HttpPost()]
	[ProducesResponseType(typeof(LogActivityCreateCommandResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateLogActivityAsync([FromBody] LogActivityCreateCommandRequest request)
	{
		request.UserId = JwtUserData().Id;
		var response = await _mediator.Send(request);
		return StatusCode(response.HttpStatusCode, response.GetResultData);
	}
}
