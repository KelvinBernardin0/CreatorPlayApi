using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.History.HistoryCreate
{
    public class HistoryCreateCommandHandler(ILogger<HistoryCreateCommandHandler> logger, ICreatorPlayContext context, IMediator mediator)
        : IRequestHandler<HistoryCreateCommandRequest, ResponseApi<HistoryCreateCommandResponse>>
    {
        private readonly ILogger<HistoryCreateCommandHandler> _logger = logger;
        private readonly ICreatorPlayContext _context = context;
        private readonly IMediator _mediator = mediator;

        public async Task<ResponseApi<HistoryCreateCommandResponse>> Handle(HistoryCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseApi<HistoryCreateCommandResponse>();
            try
            {
                var requestError = ValidateRequest(request);
                if (requestError == null)
                {
                    var newHistory = new Domain.Entities.History();
                    newHistory.AddHistory(request.TeamId, request.UserId, request.Description);
                    await _context.History.AddAsync(newHistory, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    response.SetSuccess(new HistoryCreateCommandResponse("Historico adcionado com sucesso"), HttpStatusCode.Created.GetHashCode());
                }
                else
                    response.SetError(new ResponseError(requestError.Value, requestError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", $"Erro in {nameof(HistoryCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
                response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
            }
            return response;
        }
        private static TypeError? ValidateRequest(HistoryCreateCommandRequest request)
        {
            if (!request.Description.HasValue())
                return TypeError.HistoryDescriptionRequired;

            return null;
        }
    }
}
