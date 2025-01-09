using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CreatorPlay.Application.Team.Queries.TemList
{
    public class TeamListQueryHandler(ILogger<TeamListQueryHandler> logger, ICreatorPlayContext context, IMediator mediator) :
        IRequestHandler<TeamListQueryRequest, ResponseApi<List<TeamListQueryResponse>>>
    {
        private readonly ILogger<TeamListQueryHandler> _logger = logger;
        private readonly ICreatorPlayContext _context = context;
        private readonly IMediator _mediator = mediator;

        public async Task<ResponseApi<List<TeamListQueryResponse>>> Handle(TeamListQueryRequest request, CancellationToken cancellationToken)
        {
                 var response = new ResponseApi<List<TeamListQueryResponse>>();
            try
            {


            var teamMembers = await _context.Team
                    .Join(_context.TeamMember, t => t.Id, tm => tm.TeamId, (t, tm) => new { t, tm })
                    .Where(x =>x.tm.Status== Status.Active && x.tm.UserId==request.RequestUserId && x.t.Status== Status.Active  )
                    .GroupBy(x => new { x.t.Id, x.t.Name,x.t.Description,x.t.Creator, x.t.CreatedAt,x.t.DeactivationDate,x.tm.UserId })
                    .Select(g => new TeamListQueryResponse
                    {
                        Id = g.Key.Id,
                        Name = g.Key.Name,
                        Description = g.Key.Description,
                        creator = g.Key.Creator,
                        CreateDate = g.Key.CreatedAt,
                        DeactivationDate =  g.Key.DeactivationDate,
                        LeaderId = _context.TeamMember.Where(x=> x.TeamId==g.Key.Id && x.IsLeader==true).Select(x=> x.UserId).FirstOrDefault() })
                    .ToListAsync(cancellationToken);
          
        
                     response.SetSuccess(teamMembers, HttpStatusCode.OK.GetHashCode());
          }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", $"Erro in {nameof(TeamListQueryHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
                response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
            }
            return response;
        }

    }

}
