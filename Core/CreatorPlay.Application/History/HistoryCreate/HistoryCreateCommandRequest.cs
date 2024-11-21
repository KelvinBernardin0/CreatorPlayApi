using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.History.HistoryCreate
{
    public class HistoryCreateCommandRequest : IRequest<ResponseApi<HistoryCreateCommandResponse>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public int TeamId { get; set; }
        public string? Description { get; set; }
    }
}
