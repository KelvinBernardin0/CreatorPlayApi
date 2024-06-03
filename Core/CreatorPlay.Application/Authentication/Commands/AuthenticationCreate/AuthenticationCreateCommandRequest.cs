using MediatR;
using CreatorPlay.Application.Common.Models.Response;

namespace CreatorPlay.Application.Authentication.Commands.AuthenticationCreate;

public record AuthenticationCreateCommandRequest(string? Email, string? Password) : IRequest<ResponseApi<AuthenticationCreateCommandResponse>>;
