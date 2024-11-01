using MediatR;
using CreatorPlay.Application.Common.Models.Response;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.Team.Commands.TeamCreate;

public class TeamCreateCommandRequest : IRequest<ResponseApi<TeamCreateCommandResponse>>
{
	[JsonIgnore]
    public string? LeaderId { get; set; }
	public required string Name { get; set; }
}
