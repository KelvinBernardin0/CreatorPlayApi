using MediatR;
using CreatorPlay.Application.Common.Models.Response;

namespace CreatorPlay.Application.Users.Commands.UsersResetPassword;

public class UsersResetPasswordCommandRequest(string email, string code, string password) : IRequest<ResponseApi<UsersResetPasswordCommandResponse>>
{
    public string Email { get; set; } = email;
    public string Code { get; set; } = code;
    public string Password { get; set; } = password;
}