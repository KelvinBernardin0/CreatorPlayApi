using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Images.Commands.ImagesCreate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CreatorPlay.Presentation.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ImagesController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ImagesController> _logger;

		public ImagesController(IMediator mediator, ILogger<ImagesController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[HttpPost("Upload")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseApiError), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UploadImageAsync([FromForm] ImagesCreateCommandRequest request)
		{
			if (request.ImageFile == null || request.ImageFile.Length == 0)
			{
				return BadRequest(new ResponseApiError { Message = "Nenhuma imagem foi anexada." });
			}

			var command = new ImagesCreateCommandRequest
			{
				ImageName = request.ImageFile.FileName,
				ImageFile = request.ImageFile
			};

			var response = await _mediator.Send(command);
			return StatusCode(response.HttpStatusCode, response.GetResultData);
		}
	}
}
