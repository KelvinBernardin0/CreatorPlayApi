using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Team.Commands.TeamCreate;
using CreatorPlay.Application.Team.Commands.TeamDelete;
using CreatorPlay.Application.Team.Queries.TemList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreatorPlay.Presentation.API.Controllers;

[Authorize(Roles = "Default_Access,Commercial_Access")]
[Route("api/v1/[controller]/")]
[ApiController]
public class TeamController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;
   
    [HttpPost()]
    [ProducesResponseType(typeof(TeamCreateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTeamAsync([FromBody] TeamCreateCommandRequest request)
    {
      
        request.Creator= JwtUserData().Id;
        var response = await _mediator.Send(request);
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
    [Route("{TeamId:int}")]
    [HttpDelete()]
    [ProducesResponseType(typeof(TeamDeleteCommandRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTeamAsync(int TeamId)
    {
      TeamDeleteCommandRequest request=  new TeamDeleteCommandRequest (TeamId);
        request.RequestUserId= JwtUserData().Id;
        var response = await _mediator.Send(request);
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
    [HttpGet()]
    [ProducesResponseType(typeof(TeamDeleteCommandRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> getTeamlistAsync(int TeamId)
    {
      TeamListQueryRequest request=  new TeamListQueryRequest( JwtUserData().Id);
    
        var response = await _mediator.Send(request);
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
}
