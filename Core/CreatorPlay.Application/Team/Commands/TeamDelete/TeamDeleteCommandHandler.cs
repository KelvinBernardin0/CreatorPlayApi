using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Team.Commands.TeamCreate;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Team.Commands.TeamDelete
{
    public class TeamDeleteCommandHandler(ILogger<TeamDeleteCommandHandler> logger, ICreatorPlayContext context, IMediator mediator) :
        IRequestHandler<TeamDeleteCommandRequest, ResponseApi<TeamDeleteCommandResponse>>
    {
        private readonly ILogger<TeamDeleteCommandHandler> _logger = logger;
        private readonly ICreatorPlayContext _context = context;
        private readonly IMediator _mediator = mediator;

        public async Task<ResponseApi<TeamDeleteCommandResponse>> Handle(TeamDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseApi<TeamDeleteCommandResponse>();
            try
            {
                
                var team = await _context.Team.FirstOrDefaultAsync(x => x.LeaderId == request.LeaderId, cancellationToken);
                if (team == null)
                {
                    response.SetError(new ResponseError(TypeError.DefaultError, "Lider não encontrada"), HttpStatusCode.BadRequest.GetHashCode());
                    return response;
                }

                team.SetStatusTeam(Status.Inactive);

                _context.Team.Update(team);
                await _context.SaveChangesAsync(cancellationToken);

                response.SetSuccess(new TeamDeleteCommandResponse("Equipe Deletado com sucesso!"), HttpStatusCode.OK.GetHashCode());
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", $"Erro in {nameof(TeamCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
                response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
            }
            return response;
        }

    }

}
