using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.TemplateHistory.Queries.GetTemplateHistory;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using WebApi_VivoValoriza.BouncyCrypto.Asn1.Ocsp;

namespace CreatorPlay.Application.TeamMember.Queries.GetTeamMember;

public class GetTeamMemberQueryHandler(ILogger<GetTeamMemberQueryHandler> logger,
                                          ICreatorPlayContext _context) : IRequestHandler<GetTeamMemberQueryRequest, ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>>
{
    private readonly ILogger<GetTeamMemberQueryHandler> _logger = logger;
    private readonly ICreatorPlayContext _context;

    public async Task<ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>> Handle(GetTeamMemberQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new ResponseApi<IEnumerable<GetTeamMemberQueryResponse>>();
        try
        {
            var teamMembers = await (from t in _context.Team
                                     join tm in _context.TeamMember on t.Id equals tm.TeamId
                                     join usr in _context.ApplicationUser on tm.UserId equals usr.Id
                                     select new
                                     {
                                         TeamName = t.Name,
                                         UserEmail = usr.Email
                                     }).ToListAsync(cancellationToken);
            

            // Mapeamento para a resposta
            response.SetSuccess(teamMembers.Select(x => new GetTeamMemberQueryResponse
            {
                Name = x.TeamName,
                Email = x.UserEmail
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
