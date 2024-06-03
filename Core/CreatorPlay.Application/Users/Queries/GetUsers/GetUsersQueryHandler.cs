using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler(ILogger<GetUsersQueryHandler> _logger, ICreatorPlayContext _context) : IRequestHandler<GetUsersQueryRequest, ResponseApi<IEnumerable<GetUsersQueryResponse>>>
{
	public async Task<ResponseApi<IEnumerable<GetUsersQueryResponse>>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<IEnumerable<GetUsersQueryResponse>>();

		try
		{
			var usuarios = await _context.ApplicationUser.ToListAsync(cancellationToken);
			response.SetSuccess(usuarios.Select(x => new GetUsersQueryResponse(x)), HttpStatusCode.OK.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"Erro in {nameof(GetUsersQueryHandler)}. Request: {request.ToIndentedJson()}");
			response.SetError(new ResponseError(99, "Ocorreu um erro interno."), HttpStatusCode.Unauthorized.GetHashCode());
		}

		return response;
	}
}