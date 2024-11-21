using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.TeamMember.Commands.TeamMemberDelete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CreatorPlay.Application.TeamMember.Commands.TeamMemberExit
{
    public class TeamMemberExitCommandHandler(ILogger<TeamMemberExitCommandHandler> logger, ICreatorPlayContext context)
                                                : IRequestHandler<TeamMemberExitCommandRequest, ResponseApi<TeamMemberExitCommandResponse>>
    {
        private readonly ILogger<TeamMemberExitCommandHandler> _logger = logger;
        private readonly ICreatorPlayContext _context = context;

        public async Task<ResponseApi<TeamMemberExitCommandResponse>> Handle(TeamMemberExitCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseApi<TeamMemberExitCommandResponse>();
            try
            {
               
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }
    }
}
