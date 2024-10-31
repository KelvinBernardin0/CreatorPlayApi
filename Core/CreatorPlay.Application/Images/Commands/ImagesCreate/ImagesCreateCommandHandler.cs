using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Images.Commands.ImagesCreate;
using CreatorPlay.Application.TemplateHistory.Commands.TemplateHistoryCreate;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CreatorPlay.Application.TemplateHistory.Commands.ImagesCreate
{
	public class ImagesCreateCommandHandler : IRequestHandler<ImagesCreateCommandRequest, ResponseApi<ImagesCreateCommandResponse>>
	{
		private readonly ILogger<ImagesCreateCommandHandler> _logger;

		public ImagesCreateCommandHandler(ILogger<ImagesCreateCommandHandler> logger)
		{
			_logger = logger;
		}

		public async Task<ResponseApi<ImagesCreateCommandResponse>> Handle(ImagesCreateCommandRequest request, CancellationToken cancellationToken)
		{
			var response = new ResponseApi<ImagesCreateCommandResponse>();

			try
			{
				var requestError = await ValidateRequest(request);
				if (requestError == null)
				{
					// Salvar a imagem no diretório especificado
					var imagePath = SaveFile(request.ImageFile, request.ImageName); https://vivoid.vivo.com.br/creatorPlay/

					response.SetSuccess(new ImagesCreateCommandResponse("Imagem salva com sucesso!", imagePath, request.ImageName), HttpStatusCode.Created.GetHashCode());
				}
				else
				{
					response.SetError(new ResponseError(requestError.Value, requestError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("{Message}", $"Erro em {nameof(ImagesCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
				response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
			}

			return response;
		}

		// Método para salvar a imagem
		private string SaveFile(IFormFile imageFile, string imageName)
		{

			//var folderPath = @"E:\App\VivoID\vivoid_v2\front\deploy\creatorPlay";  // Caminho fixo para salvar as imagens
			var folderPath = @"C:\Users\kelvin.bernardino\Desktop\VVE";  // Caminho fixo para salvar as imagens
			var filePath = Path.Combine(folderPath, imageName);

			// Garantir que o diretório existe
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				imageFile.CopyTo(stream);
			}

			return filePath;
		}

		// Método para validar o request
		private async Task<TypeError?> ValidateRequest(ImagesCreateCommandRequest request)
		{
			if (!request.ImageName.HasValue())
				return TypeError.ImagesNameRequired;

			if (request.ImageFile == null || request.ImageFile.Length == 0)
				return TypeError.ImagesRequired;

			return null;
		}
	}
}
