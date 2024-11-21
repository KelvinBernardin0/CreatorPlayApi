using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.History.HistoryCreate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreatorPlay.Presentation.API.Controllers;

[Authorize(Roles = "Default_Access,Commercial_Access")]
[Route("api/v1/[controller]")]
[ApiController]
public class HistoryController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;
   
    [HttpPost()]
    [ProducesResponseType(typeof(HistoryCreateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHistoryAsync([FromBody] HistoryCreateCommandRequest request)
    {
        request.UserId = JwtUserData().Id;
        var response = await _mediator.Send(request);
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
}
