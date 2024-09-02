using MediatR;
using CreatorPlay.Application.Common.Models.Response;

namespace CreatorPlay.Application.Users.Commands.UsersForgotPassword;

public record UsersForgotPasswordCommandRequest(string Email) : IRequest<ResponseApi<UsersForgotPasswordCommandResponse>>;
