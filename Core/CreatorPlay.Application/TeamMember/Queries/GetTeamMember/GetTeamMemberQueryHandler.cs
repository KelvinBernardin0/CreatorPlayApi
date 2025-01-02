using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using WebApi_VivoValoriza.BouncyCrypto.Math.EC.Rfc7748;

namespace CreatorPlay.Application.TeamMember.Queries.GetTeamMember;

public class GetTeamMemberQueryHandler(ILogger<GetTeamMemberQueryHandler> logger,
                                          ICreatorPlayContext _context) : IRequestHandler<GetTeamMemberQueryRequest, ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>>
{
    private readonly ILogger<GetTeamMemberQueryHandler> _logger = logger;    

    public async Task<ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>> Handle(GetTeamMemberQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>();
        try
        {
            var teamMembers = await _context.Team
                    .Join(_context.TeamMember, t => t.Id, tm => tm.TeamId, (t, tm) => new { t, tm })
                    .Join(_context.ApplicationUser, combined => combined.tm.UserId, usr => usr.Id, (combined, usr) => new { combined.t, combined.tm, usr })
                    .Where(x =>x.tm.TeamId == request.TeamId)
                    .GroupBy(x => new { x.t.Id, x.t.Name })
                    .Select(g => new
                    {
                        TeamId = g.Key.Id,
                        TeamName = g.Key.Name,
                         TeamMembers = g.Select(x => new User{ Id=x.usr.Id,Email=x.usr.Email}).OrderBy( x=> x.Email).ToList()
                    })
                    .ToListAsync(cancellationToken);

            
            response.SetSuccess(teamMembers.Select(x => new GetTeamMemberQueryResponse
            {
                Id=x.TeamId,
                Name = x.TeamName,
                Members = x.TeamMembers
            }), HttpStatusCode.OK.GetHashCode());
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}", $"Erro em {nameof(GetTeamMemberQueryHandler)}. Exception: {ex.Message}");
            response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
        }

        return response;
    }


}
