using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.Users.Commands.UsersUpdate;

public class UsersUpdateCommandRequest(string id, string phoneNumber) : IRequest<ResponseApi<UsersUpdateCommandResponse>>
{
	public string Id { get; } = id;
	public string? PhoneNumber { get; } = phoneNumber;
}
