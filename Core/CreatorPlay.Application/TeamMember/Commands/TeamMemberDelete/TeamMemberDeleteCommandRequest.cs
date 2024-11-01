using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.TeamMember.Commands.TeamMemberCreate;
using CreatorPlay.Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.TeamMember.Commands.TeamMemberDelete
{
    public record TeamMemberDeleteCommandRequest : IRequest<ResponseApi<TeamMemberDeleteCommandResponse>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public int TeamId { get; set; }

        public TeamMemberDeleteCommandRequest(string? userId, int teamId )
        {
            UserId = userId;
            TeamId = teamId;
        }
    }
   
}
