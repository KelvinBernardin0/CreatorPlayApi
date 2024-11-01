using Azure;
using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Team.Commands.TeamCreate;
using CreatorPlay.Application.TeamMember.Commands.TeamMemberCreate;
using CreatorPlay.Application.Users.Commands.UsersUpdate;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;


namespace CreatorPlay.Application.TeamMember.Commands.TeamMemberDelete
{
    public class TeamMemberDeleteCommandHandler(ILogger<TeamMemberDeleteCommandHandler> logger, ICreatorPlayContext context,
                                                 UserManager<ApplicationUser> userManager) : IRequestHandler<TeamMemberDeleteCommandRequest, ResponseApi<TeamMemberDeleteCommandResponse>>
    {
        private readonly ILogger<TeamMemberDeleteCommandHandler> _logger = logger;
        private readonly ICreatorPlayContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<ResponseApi<TeamMemberDeleteCommandResponse>> Handle(TeamMemberDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseApi<TeamMemberDeleteCommandResponse>();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                //user.Email = request.Email;
                user.ModifiedAt = DateTime.Now;

                var changePassword = await _userManager.ChangePasswordAsync(user, request.Password, request.PasswordConfirmation);

                if (!changePassword.Succeeded)
                {
                    var typeErrors = Extensions.GetEnumValues<TypeError>();
                    foreach (var typeError in typeErrors)
                    {
                        if (changePassword.Errors.Any(x => x.Code == typeError.ToString()))
                        {
                            response.SetError(new ResponseError(typeError, typeError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
                            return response;
                        }
                    }
                }

                _context.ApplicationUser.Update(user);
                await _context.SaveChangesAsync(cancellationToken);

                response.SetSuccess(new UsersUpdateCommandResponse($"Usuário {user.Email} atualizado com sucesso!"), HttpStatusCode.OK.GetHashCode());
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", $"Erro in {nameof(TeamMemberCreateCommandHandler)}. Request: {request.ToJson()} - Exception: {ex.ToJson()}");
                response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
            }
        }
    }
}
