using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.TemplateHistory.Queries.GetTemplateHistory;

public class GetTemplateHistoryQueryHandler(ILogger<GetTemplateHistoryQueryHandler> logger, 
										  ICreatorPlayContext _context, 
										  UserManager<ApplicationUser> userManager) : IRequestHandler<GetTemplateHistoryQueryRequest, ResponseApi<IEnumerable<GetTemplateHistoryQueryResponse>>>
{
	private readonly ILogger<GetTemplateHistoryQueryHandler> _logger = logger;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<ResponseApi<IEnumerable<GetTemplateHistoryQueryResponse>>> Handle(GetTemplateHistoryQueryRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<IEnumerable<GetTemplateHistoryQueryResponse>>();
		try
		{
			var templates = await _context.TemplateHistory.Where(x=>x.AspNetUsersId == request.UserId).ToListAsync();
			if (templates.Count == 0)
			{
				response.SetSuccess(new List<GetTemplateHistoryQueryResponse>(), HttpStatusCode.OK.GetHashCode());
				return response;
			}

			response.SetSuccess(templates.Select(x => new GetTemplateHistoryQueryResponse(x)), HttpStatusCode.OK.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(GetTemplateHistoryQueryHandler)}. Exception: {ex.Message}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}
		return response;
	}
}