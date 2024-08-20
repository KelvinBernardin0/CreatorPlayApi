using MediatR;
using CreatorPlay.Application.Common.Models.Response;

namespace CreatorPlay.Application.Users.Commands.UsersCreate;

public class UsersCreateCommandRequest : IRequest<ResponseApi<UsersCreateCommandResponse>>
{
	public string Email { get; set; }
	public string Password { get; set; }
	public string PasswordConfirmation { get; set; }
	public string RoleId { get; set; }
}