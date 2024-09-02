using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace CreatorPlay.Application.Users.Commands.UsersResetPassword;

public class UsersResetPasswordCommandHandler(ILogger<UsersResetPasswordCommandHandler> logger,
                                               ICreatorPlayContext context,
                                               UserManager<ApplicationUser> userManager,
                                               IEmailService emailService) : IRequestHandler<UsersResetPasswordCommandRequest, ResponseApi<UsersResetPasswordCommandResponse>>
{
    private readonly ILogger<UsersResetPasswordCommandHandler> _logger = logger;
    private readonly ICreatorPlayContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailService _emailService = emailService;

    public async Task<ResponseApi<UsersResetPasswordCommandResponse>> Handle(UsersResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ResponseApi<UsersResetPasswordCommandResponse>();

        try
        {
            var requestError = await ValidateRequest(request, cancellationToken);
            if (requestError != null)
            {
                _logger.LogError("{Message}", $"Erro in {nameof(UsersResetPasswordCommandHandler)}. Request: {request} - Response: {requestError.GetDescription()}");
                response.SetError(new ResponseError(requestError.Value, requestError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                var resultResetPassword = await userManager.ResetPasswordAsync(user, code, request.Password);

                if (!resultResetPassword.Succeeded)
                {
                    _logger.LogError("{Message}", $"Erro in {nameof(UsersResetPasswordCommandHandler)}. Request: {request} - Response: {resultResetPassword.Errors.FirstOrDefault()}");
                    response.SetError(new ResponseError(TypeError.ResetPasswordFail, TypeError.ResetPasswordFail.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
                }
                else
                    response.SetSuccess(new UsersResetPasswordCommandResponse("Senha cadastrada com sucesso!"), HttpStatusCode.OK.GetHashCode());
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}", $"Erro in {nameof(UsersResetPasswordCommandHandler)}. Request: {request} - Exception: {ex.ToJson()}");
            response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
        }
        return response;
    }

    private async Task<TypeError?> ValidateRequest(UsersResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (!request.Email.HasValue())
            return TypeError.EmailRequired;

        if (!Extensions.IsValidEmail(request.Email))
            return TypeError.InvalidEmail;

        var emailExists = await _context.ApplicationUser.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (!emailExists)
            return TypeError.EmailNotFound;

        if (!request.Code.HasValue())
            return TypeError.CodeForgotPasswordInvalid;

        return null;
    }
}
