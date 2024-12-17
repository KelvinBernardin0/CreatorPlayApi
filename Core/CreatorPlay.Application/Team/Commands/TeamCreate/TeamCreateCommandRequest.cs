using MediatR;
using CreatorPlay.Application.Common.Models.Response;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.Team.Commands.TeamCreate;

public record TeamCreateCommandRequest : IRequest<ResponseApi<TeamCreateCommandResponse>>
{

    public string? LeaderId { get; set; }
	public required string Name { get; set; }
	[JsonIgnore]
	public string? Creator { get; set; }
	public string Description { get; set; }
}

