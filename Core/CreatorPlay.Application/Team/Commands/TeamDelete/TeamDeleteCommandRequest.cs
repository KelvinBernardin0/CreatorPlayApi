using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.Team.Commands.TeamDelete
{
    public record TeamDeleteCommandRequest : IRequest<ResponseApi<TeamDeleteCommandResponse>>
    {
        [JsonIgnore]
        public string? LeaderId { get; set; }       

        public TeamDeleteCommandRequest(string leaderId)
        {
            LeaderId = leaderId;            
        }
    }
}
