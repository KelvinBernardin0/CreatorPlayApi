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

namespace CreatorPlay.Application.Users.Queries.GetAccessProfile;

public class GetAccessProfileQueryHandler(ILogger<GetAccessProfileQueryHandler> logger, 
										  ICreatorPlayContext _context, 
										  UserManager<ApplicationUser> userManager) : IRequestHandler<GetAccessProfileQueryRequest, ResponseApi<IEnumerable<GetAccessProfileQueryResponse>>>
{
	private readonly ILogger<GetAccessProfileQueryHandler> _logger = logger;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<ResponseApi<IEnumerable<GetAccessProfileQueryResponse>>> Handle(GetAccessProfileQueryRequest request, CancellationToken cancellationToken)
	{
		var response = new ResponseApi<IEnumerable<GetAccessProfileQueryResponse>>();
		try
		{
			var applicationRoles = await _context.ApplicationRole.ToListAsync(cancellationToken);
			if (applicationRoles.Count == 0)
			{
				response.SetError(new ResponseError(TypeError.AccesProfileNotFound, TypeError.AccesProfileNotFound.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
				return response;
			}

			response.SetSuccess(applicationRoles.Select(x => new GetAccessProfileQueryResponse(x)), HttpStatusCode.OK.GetHashCode());
		}
		catch (Exception ex)
		{
			_logger.LogError("{Message}", $"Erro in {nameof(GetAccessProfileQueryHandler)}. Exception: {ex.Message}");
			response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
		}
		return response;
	}
}