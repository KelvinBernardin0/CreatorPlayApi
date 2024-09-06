using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Users.Commands.UsersCreate;
using CreatorPlay.Application.Users.Commands.UsersForgotPassword;
using CreatorPlay.Application.Users.Commands.UsersResetPassword;
using CreatorPlay.Application.Users.Commands.UsersUpdate;
using CreatorPlay.Application.Users.Queries.GetAccessProfile;
using CreatorPlay.Application.Users.Queries.GetUsersById;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CreatorPlay.Presentation.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController(IMediator mediator, ILogger<UsersController> logger) : BaseController
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UsersController> _logger = logger;

    [HttpGet("id/{id}")]
    [ProducesResponseType(typeof(GetUsersByIdQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsersByIdAsync([FromRoute] string id)
    {
        var response = await _mediator.Send(new GetUsersByIdQueryRequest(id));
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

    [HttpPut()]
    [ProducesResponseType(typeof(UsersUpdateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUsuariosAsync([FromBody] UsersUpdateCommandRequest request)
    {
        var response = new ResponseApi<UsersUpdateCommandResponse>();

        try
        {
            request.Id = JwtUserData().Id;
            response = await _mediator.Send(request);
            return StatusCode(response.HttpStatusCode, response.GetResultData);
        }
        catch (Exception ex)
        {
            _logger.LogError("{message}", ex.ToJson());
            response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
            return StatusCode(response.HttpStatusCode, response.GetResultData);
        }
    }

    [HttpGet("access-profile")]
    [ProducesResponseType(typeof(GetAccessProfileQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAccessProfileAsync()
    {
        var response = await _mediator.Send(new GetAccessProfileQueryRequest());
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(UsersForgotPasswordCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] UsersForgotPasswordCommandRequest request)
    {
        var response = await _mediator.Send(new UsersForgotPasswordCommandRequest(request.Email));
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(UsersResetPasswordCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] UsersResetPasswordCommandRequest request)
    {
        var response = await _mediator.Send(new UsersResetPasswordCommandRequest(request.Email, request.Code, request.Password));
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
}
