using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.TeamMember.Commands.TeamMemberExit
{
    public record TeamMemberExitCommandRequest : IRequest<ResponseApi<TeamMemberExitCommandResponse>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        //public int TeamId { get; set; }

        public TeamMemberExitCommandRequest(string? userId)
        {
            UserId = userId;
            //TeamId = teamId;
        }
    }
}
