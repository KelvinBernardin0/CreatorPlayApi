using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;


namespace CreatorPlay.Application.TeamMember.Commands.TeamMemberDelete
{
    public class TeamMemberDeleteCommandHandler(ILogger<TeamMemberDeleteCommandHandler> logger, ICreatorPlayContext context)
                                                : IRequestHandler<TeamMemberDeleteCommandRequest, ResponseApi<TeamMemberDeleteCommandResponse>>
    {
        private readonly ILogger<TeamMemberDeleteCommandHandler> _logger = logger;
        private readonly ICreatorPlayContext _context = context;
        

        public async Task<ResponseApi<TeamMemberDeleteCommandResponse>> Handle(TeamMemberDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseApi<TeamMemberDeleteCommandResponse>();
            
            try
            {   
                var teamMember = await _context.TeamMember.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.TeamId == request.TeamId, cancellationToken);                

                if (teamMember == null) 
                {
                    response.SetError(new ResponseError(TypeError.DefaultError, "Equipe não encontrada"), HttpStatusCode.BadRequest.GetHashCode());
                    return response;
                }
                teamMember.SetStatusTeamMember(Status.Inactive);

                _context.TeamMember.Update(teamMember);
                await _context.SaveChangesAsync(cancellationToken);

                response.SetSuccess(new TeamMemberDeleteCommandResponse("Usuário Deletado com sucesso!"), HttpStatusCode.OK.GetHashCode());
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", $"Erro in {nameof(TeamMemberDeleteCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
                response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
            }

            return response;
        }
    }
}
