using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.TemplateHistory.Queries.GetTemplateHistory;

public class GetTemplateHistoryQueryRequest(string userId) : IRequest<ResponseApi<IEnumerable<GetTemplateHistoryQueryResponse>>>
{
    public string UserId { get; set; } = userId;
}
