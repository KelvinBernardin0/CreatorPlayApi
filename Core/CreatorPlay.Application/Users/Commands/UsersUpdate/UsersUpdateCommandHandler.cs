using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Users.Commands.UsersUpdate;

public class UsersUpdateCommandHandler(ILogger<UsersUpdateCommandHandler> _logger, ICreatorPlayContext _context) : IRequestHandler<UsersUpdateCommandRequest, ResponseApi<UsersUpdateCommandResponse>>
{
	public async Task<ResponseApi<UsersUpdateCommandResponse>> Handle(UsersUpdateCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<UsersUpdateCommandResponse>();

		try
		{
			var user = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
			if (user != null)
			{
				user.PhoneNumber = request.PhoneNumber;

				_context.ApplicationUser.Update(user);
				await _context.SaveChangesAsync(cancellationToken);

				response.SetSuccess(new UsersUpdateCommandResponse(user.Id), HttpStatusCode.OK.GetHashCode());
			}
			else
			{
				response.SetError(new ResponseError(99, $"Usuário não encontrado. ID: {request.Id}"));
				return response;
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"Erro in {nameof(UsersUpdateCommandHandler)}. Request: {request.ToIndentedJson()}");
			response.SetError(new ResponseError(99, "Ocorreu um erro interno."), HttpStatusCode.Unauthorized.GetHashCode());
		}

		return response;
	}
}