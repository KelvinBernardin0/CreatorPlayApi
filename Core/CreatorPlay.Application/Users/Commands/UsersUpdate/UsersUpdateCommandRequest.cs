using CreatorPlay.Application.Common.Models.Response;
using MediatR;

namespace CreatorPlay.Application.Users.Commands.UsersUpdate;

public class UsersUpdateCommandRequest(string id, string email, string password, string passwordConfirmation) : IRequest<ResponseApi<UsersUpdateCommandResponse>>
{
	public string Id { get; } = id;
	public string Email { get; set; } = email;
	public string Password { get; set; } = password;
	public string PasswordConfirmation { get; set; } = passwordConfirmation;
}
