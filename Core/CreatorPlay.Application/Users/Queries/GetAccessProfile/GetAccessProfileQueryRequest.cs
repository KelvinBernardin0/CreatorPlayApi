using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.Users.Queries.GetAccessProfile;

public record GetAccessProfileQueryRequest : IRequest<ResponseApi<IEnumerable<GetAccessProfileQueryResponse>>>;
