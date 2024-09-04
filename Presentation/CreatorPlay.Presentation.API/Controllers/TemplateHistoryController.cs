using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.TemplateHistory.Commands.TemplateHistoryCreate;
using CreatorPlay.Application.TemplateHistory.Queries.GetTemplateHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreatorPlay.Presentation.API.Controllers;

[Authorize(Roles = "Default_Access,Commercial_Access")]
[Route("api/v1/[controller]")]
[ApiController]
public class TemplateHistoryController(IMediator mediator, ILogger<TemplateHistoryController> logger) : BaseController
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<TemplateHistoryController> _logger = logger;
   
    [HttpPost()]
    [ProducesResponseType(typeof(TemplateHistoryCreateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTemplateHistoryAsync([FromBody] TemplateHistoryCreateCommandRequest request)
    {
        request.UserId = JwtUserData().Id;
        var response = await _mediator.Send(request);
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(GetTemplateHistoryQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTemplateHistoryAsync()
    {
        var response = await _mediator.Send(new GetTemplateHistoryQueryRequest(JwtUserData().Id));
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
}
