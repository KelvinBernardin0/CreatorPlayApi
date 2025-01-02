using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.Team.Queries.TemList
{
    public record TeamListQueryRequest : IRequest<ResponseApi<List<TeamListQueryResponse>>>
    {
        [JsonIgnore]
        public string RequestUserId { get; set; }     

        public TeamListQueryRequest(string requestUserId)
        {
             RequestUserId=requestUserId;
        }
    }
}
