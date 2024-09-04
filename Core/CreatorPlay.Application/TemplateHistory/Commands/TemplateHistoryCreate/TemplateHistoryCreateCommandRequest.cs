using MediatR;
using CreatorPlay.Application.Common.Models.Response;
using System.Text.Json.Serialization;
using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Application.TemplateHistory.Commands.TemplateHistoryCreate;

public class TemplateHistoryCreateCommandRequest : IRequest<ResponseApi<TemplateHistoryCreateCommandResponse>>
{
    [JsonIgnore]
    public string? UserId { get; set; }
	public string Name { get; set; }
	public string Template { get; set; }
    public TemplateStatus TemplateStatus { get; set; }
}