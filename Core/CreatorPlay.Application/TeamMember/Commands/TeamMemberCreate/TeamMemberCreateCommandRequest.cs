using MediatR;
using CreatorPlay.Application.Common.Models.Response;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.TeamMember.Commands.TeamMemberCreate;

public record TeamMemberCreateCommandRequest : IRequest<ResponseApi<TeamMemberCreateCommandResponse>>
{
	[JsonIgnore]
	public string? UserId { get; set; }
	[JsonIgnore]
	public bool IsLeader { get; set; }
	public int TeamId { get; set; }
	public string? UserEmail { get; set; }
	[JsonIgnore]
	public string? RequestUserId  { get; set; }

	public TeamMemberCreateCommandRequest(string? userId, int teamId, bool isLeader = default,  string? userEmail = default,string requestUserId = default )
	{
		UserId = userId;
		TeamId = teamId;
		IsLeader = isLeader;
		UserEmail = userEmail;
		RequestUserId=requestUserId;
	}
}
