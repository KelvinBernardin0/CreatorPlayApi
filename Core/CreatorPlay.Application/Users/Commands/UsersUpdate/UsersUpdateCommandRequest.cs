using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.Users.Commands.UsersUpdate;

public class UsersUpdateCommandRequest : IRequest<ResponseApi<UsersUpdateCommandResponse>>
{
	[JsonIgnore]
	public string? Id { get; set; }
	public string? Email { get; set; }
	public required string Password { get; set; }
	public required string PasswordConfirmation { get; set; }
}