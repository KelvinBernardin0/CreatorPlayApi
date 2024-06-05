using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.Users.Queries.GetUsers;

public class GetUsersQueryRequest : IRequest<ResponseApi<IEnumerable<GetUsersQueryResponse>>>
{
	public string Id { get; set; }


}
