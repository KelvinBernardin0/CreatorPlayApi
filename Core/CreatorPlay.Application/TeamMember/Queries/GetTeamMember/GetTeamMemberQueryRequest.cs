using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using WebApi_VivoValoriza.BouncyCrypto.Asn1.Misc;

namespace CreatorPlay.Application.TeamMember.Queries.GetTeamMember;

public class GetTeamMemberQueryRequest : IRequest<ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>>
{
    public string? UserId { get; set; }
    public 	int TeamId { get; set; }
}
