using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.Users.Commands.UsersUpdate;

public class UsersUpdateCommandRequest : IRequest<ResponseApi<UsersUpdateCommandResponse>>
{
	public string Id { get; set; }
	public string? PhoneNumber { get; set; }
}
