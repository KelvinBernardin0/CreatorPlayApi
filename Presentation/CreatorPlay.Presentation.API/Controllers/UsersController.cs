using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CreatorPlay.Application.Users.Commands.UsersUpdate;
using CreatorPlay.Application.Users.Queries.GetUsers;
using CreatorPlay.Application.Users.Commands.UsersCreate;
using Microsoft.AspNetCore.Authorization;

namespace CreatorPlay.Presentation.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator;

	[HttpGet()]
	[ProducesResponseType(typeof(GetUsersQueryResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> GetUsuariosAsync()
	{
		var response = await _mediator.Send(new GetUsersQueryRequest());
		return StatusCode(response.HttpStatusCode, response.GetResultData);
	}
	
	[HttpPost()]
	[ProducesResponseType(typeof(UsersCreateCommandResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateUsersAsync([FromBody] UsersCreateCommandRequest request)
	{
		var response = await _mediator.Send(request);
		return StatusCode(response.HttpStatusCode, response.GetResultData);
	}

	[Authorize(Roles = "Default_Access")]
	[HttpPut()]
	[ProducesResponseType(typeof(UsersUpdateCommandResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> UpdateUsuariosAsync([FromBody] UsersUpdateCommandRequest request)
	{
		var response = await _mediator.Send(request);
		return StatusCode(response.HttpStatusCode, response.GetResultData);
	}
}
