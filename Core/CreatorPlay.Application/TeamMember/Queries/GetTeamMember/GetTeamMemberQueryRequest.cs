using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.TeamMember.Queries.GetTeamMember;

public class GetTeamMemberQueryRequest : IRequest<ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>>
{
    public int UserId { get; set; }
}
