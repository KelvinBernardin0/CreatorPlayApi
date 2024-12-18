using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.TeamMember.Commands.TeamMemberCreate;
using CreatorPlay.Application.TeamMember.Commands.TeamMemberDelete;
using CreatorPlay.Application.TeamMember.Queries.GetTeamMember;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreatorPlay.Presentation.API.Controllers;

//[Authorize(Roles = "Default_Access,Commercial_Access")]
[Route("api/v1/[controller]")]
[ApiController]
public class TeamMemberController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

	[HttpPost()]
	[ProducesResponseType(typeof(TeamMemberCreateCommandResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateTeamMemberAsync([FromBody] TeamMemberCreateCommandRequest request)
	{
         request.RequestUserId= JwtUserData().Id;
		var response = await _mediator.Send(request);
		return StatusCode(response.HttpStatusCode, response.GetResultData);
	}

    [HttpDelete()]
    [ProducesResponseType(typeof(TeamMemberDeleteCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTeamMemberAsync([FromBody] TeamMemberDeleteCommandRequest request)
    {
        request.RequestUserId = JwtUserData().Id;
        var response = await _mediator.Send(request);
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(GetTeamMemberQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTeamMember()
    {        
        var response = await _mediator.Send(new GetTeamMemberQueryRequest());
        return StatusCode(response.HttpStatusCode, response.GetResultData);
    }
}
