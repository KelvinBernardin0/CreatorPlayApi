using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Team.Commands.TeamCreate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreatorPlay.Presentation.API.Controllers;

[Authorize(Roles = "Default_Access,Commercial_Access")]
[Route("api/v1/[controller]")]
[ApiController]
public class TeamController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;
   
    [HttpPost()]
    [ProducesResponseType(typeof(TeamCreateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTeamAsync([FromBody] TeamCreateCommandRequest request)
    {
        request.LeaderId = JwtUserData().Id;
        var response = await _mediator.Send(request);
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
}
