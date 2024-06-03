using CreatorPlay.Application.Common.Models.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CreatorPlay.Application.LogActivity.Commands.LogActivityCreate;

public record LogActivityCreateCommandRequest : IRequest<ResponseApi<LogActivityCreateCommandResponse>>
{
	[JsonIgnore]
    public string? UserId { get; set; }
	public int LogLevel { get; set; }
	public string Message { get; set; }
}
