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

namespace CreatorPlay.Application.TemplateHistory.Commands.TemplateHistoryCreate;

public class TemplateHistoryCreateCommandHandler(ILogger<TemplateHistoryCreateCommandHandler> logger, ICreatorPlayContext context,
                                                 UserManager<ApplicationUser> userManager) : IRequestHandler<TemplateHistoryCreateCommandRequest, ResponseApi<TemplateHistoryCreateCommandResponse>>
{
    private readonly ILogger<TemplateHistoryCreateCommandHandler> _logger = logger;
    private readonly ICreatorPlayContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<ResponseApi<TemplateHistoryCreateCommandResponse>> Handle(TemplateHistoryCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ResponseApi<TemplateHistoryCreateCommandResponse>();

        try
        {
            var requestError = await ValidateRequest(request);
            if (requestError == null)
            {
                var newTemplate = new Domain.Entities.TemplateHistory
                {
                    AspNetUsersId = request.UserId,
                    Name = request.Name,
					Template = request.Template,
					Options = request.Options,
					TemplateStatus = request.TemplateStatus,
                    CreatedAt = DateTime.Now
                };

                await _context.TemplateHistory.AddAsync(newTemplate, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                response.SetSuccess(new TemplateHistoryCreateCommandResponse("Template salvo com sucesso!"), HttpStatusCode.Created.GetHashCode());
            }
            else
                response.SetError(new ResponseError(requestError.Value, requestError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}", $"Erro in {nameof(TemplateHistoryCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
            response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
        }

        return response;
    }

    private async Task<TypeError?> ValidateRequest(TemplateHistoryCreateCommandRequest request)
    {
        if (!request.Name.HasValue())
            return TypeError.TemplateNameRequired;

        if (!request.Template.HasValue())
            return TypeError.TemplateRequired;

        return null;
    }
}
