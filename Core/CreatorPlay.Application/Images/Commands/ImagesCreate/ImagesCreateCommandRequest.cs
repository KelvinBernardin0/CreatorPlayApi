using MediatR;
using CreatorPlay.Application.Common.Models.Response;
using System.Text.Json.Serialization;
using CreatorPlay.Domain.Enumerators;
using CreatorPlay.Application.TemplateHistory.Commands.TemplateHistoryCreate;
using Microsoft.AspNetCore.Http;

namespace CreatorPlay.Application.Images.Commands.ImagesCreate;

public class ImagesCreateCommandRequest : IRequest<ResponseApi<ImagesCreateCommandResponse>>
{
	public string ImageName { get; set; }
	public IFormFile ImageFile { get; set; }

}
