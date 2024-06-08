using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.Users.Queries.GetUsersById;

public record GetUsersByIdQueryRequest(string Id) : IRequest<ResponseApi<IEnumerable<GetUsersByIdQueryResponse>>>;
