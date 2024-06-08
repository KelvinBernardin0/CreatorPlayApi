using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Users.Queries.GetUsersById;

public class GetUsersByIdQueryHandler(ILogger<GetUsersByIdQueryHandler> _logger, ICreatorPlayContext _context) : IRequestHandler<GetUsersByIdQueryRequest, ResponseApi<IEnumerable<GetUsersByIdQueryResponse>>>
{
	public async Task<ResponseApi<IEnumerable<GetUsersByIdQueryResponse>>> Handle(GetUsersByIdQueryRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<IEnumerable<GetUsersByIdQueryResponse>>();

		try
		{
			var users = await _context.ApplicationUser.Where(x => x.Id == request.Id).ToListAsync(cancellationToken);
			response.SetSuccess(users.Select(x => new GetUsersByIdQueryResponse(x)), HttpStatusCode.OK.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(GetUsersByIdQueryHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}
		return response;
	}
}