using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.Team.Commands.TeamDelete
{
    public record TeamDeleteCommandRequest : IRequest<ResponseApi<TeamDeleteCommandResponse>>
    {
        [JsonIgnore]
        public int TeamId { get; set; }  
        public string RequestUserId { get; set; }     

        public TeamDeleteCommandRequest(int teamId)
        {
            TeamId = teamId;            
        }
    }
}
