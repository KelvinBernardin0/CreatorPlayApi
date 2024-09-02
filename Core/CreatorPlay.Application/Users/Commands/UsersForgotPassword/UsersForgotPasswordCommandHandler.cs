using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.Error;
using CreatorPlay.Application.Common.Models.Response;
using CreatorPlay.Application.Common.Models.SimplifiqueAPI;
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
using System.Text.Encodings.Web;

namespace CreatorPlay.Application.Users.Commands.UsersForgotPassword;

public class UsersForgotPasswordCommandHandler(ILogger<UsersForgotPasswordCommandHandler> logger,
                                       ICreatorPlayContext context,
                                       UserManager<ApplicationUser> userManager,
                                       IEmailService emailService) : IRequestHandler<UsersForgotPasswordCommandRequest, ResponseApi<UsersForgotPasswordCommandResponse>>
{
    private readonly ILogger<UsersForgotPasswordCommandHandler> _logger = logger;
    private readonly ICreatorPlayContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailService _emailService = emailService;

    public async Task<ResponseApi<UsersForgotPasswordCommandResponse>> Handle(UsersForgotPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ResponseApi<UsersForgotPasswordCommandResponse>();

        try
        {
            var requestError = await ValidateRequest(request, cancellationToken);
            if (requestError != null)
            {
                _logger.LogError("{Message}", $"Erro in {nameof(UsersForgotPasswordCommandHandler)}. Request: {request} - Response: {requestError.GetDescription()}");
                response.SetError(new ResponseError(requestError.Value, requestError.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var codeForgotPassword = await _userManager.GeneratePasswordResetTokenAsync(user);
                codeForgotPassword = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(codeForgotPassword));
                var emailBody = CreateBodyEmail(request.Email, HtmlEncoder.Default.Encode(codeForgotPassword));

                var config = await _context.GlobalConfiguration.FirstOrDefaultAsync(cancellationToken);

                var emailConfigurations = new EmailConfiguration();
                var emailModel = new EmailModel
                {
                    CorpoEmail = emailBody,
                    EmailCredentials = new EmailCredentials(emailConfigurations.DisplayName, emailConfigurations.EmailAdrees, config.EmailLogin, EncodingClass.DecodeFrom64(config.EmailPassword))
                };

                emailModel.AddEmailTo(request.Email);
                var emailServiceResponse = await _emailService.SendMailAsync(emailModel);
               
                if (emailServiceResponse.Erro)
                {
                    _logger.LogError("{Message}", $"Erro in {nameof(UsersForgotPasswordCommandHandler)}. Request: {request} - Response: {emailServiceResponse.ToJson()}");
                    response.SetError(new ResponseError(TypeError.SendMailFail, TypeError.SendMailFail.GetDescription()), HttpStatusCode.BadRequest.GetHashCode());
                }
                else
                    response.SetSuccess(new UsersForgotPasswordCommandResponse("E-mail para recuperação de senha, enviado com sucesso!"), HttpStatusCode.OK.GetHashCode());
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}", $"Erro in {nameof(UsersForgotPasswordCommandHandler)}. Request: {request} - Exception: {ex.ToJson()}");
            response.SetError(new ResponseError(TypeError.DefaultError, TypeError.DefaultError.GetDescription()), HttpStatusCode.InternalServerError.GetHashCode());
        }

        return response;
    }

    private static string CreateBodyEmail(string email, string codeForgotPassword)
    {
        string corpoEmail =
            "<!DOCTYPE html>                                                                                                                                                                                                                                                                                                                             " +
            "<html>                                                                                                                                                                                                                                                                                                                                      " +
            "<head>                                                                                                                                                                                                                                                                                                                                      " +
            "<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />                                                                                                                                                                                                                                                                       " +
            "<meta http-equiv='X-UA-Compatible' content='IE=edge'>                                                                                                                                                                                                                                                                                       " +
            "<meta name='viewport' content='width=device-width' />                                                                                                                                                                                                                                                                                       " +
            "<title>Vivo</title>                                                                                                                                                                                                                                                                                                                         " +
            "<style type='text/css'>                                                                                                                                                                                                                                                                                                                     " +
            "table                                                                                                                                                                                                                                                                                                                                       " +
            "{                                                                                                                                                                                                                                                                                                                                           " +
            "    margin: 0px auto !important;                                                                                                                                                                                                                                                                                                            " +
            "    border-collapse: collapse;                                                                                                                                                                                                                                                                                                              " +
            "    mso-table-lspace: 0pt;                                                                                                                                                                                                                                                                                                                  " +
            "    mso-table-rspace: 0pt;                                                                                                                                                                                                                                                                                                                  " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "table td,  table tr                                                                                                                                                                                                                                                                                                                         " +
            "{                                                                                                                                                                                                                                                                                                                                           " +
            "    border-collapse: collapse;                                                                                                                                                                                                                                                                                                              " +
            "    mso-table-lspace: 0pt;                                                                                                                                                                                                                                                                                                                  " +
            "    mso-table-rspace: 0pt;                                                                                                                                                                                                                                                                                                                  " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            " @media screen and (max-width: 600px) {                                                                                                                                                                                                                                                                                                     " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "table.deviceWidth                                                                                                                                                                                                                                                                                                                           " +
            "{                                                                                                                                                                                                                                                                                                                                           " +
            "    width: 100% !important;                                                                                                                                                                                                                                                                                                                 " +
            "    min-width: 100% !important;                                                                                                                                                                                                                                                                                                             " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "table.deviceWidth td                                                                                                                                                                                                                                                                                                                        " +
            "{                                                                                                                                                                                                                                                                                                                                           " +
            "    display: block !important;                                                                                                                                                                                                                                                                                                              " +
            "    clear: both !important;                                                                                                                                                                                                                                                                                                                 " +
            "    width: 100% !important;                                                                                                                                                                                                                                                                                                                 " +
            "    text-align: center !important;                                                                                                                                                                                                                                                                                                          " +
            "    padding: 0px !important;                                                                                                                                                                                                                                                                                                                " +
            "    min-width: 100% !important;                                                                                                                                                                                                                                                                                                             " +
            "    margin: 0 auto !important;                                                                                                                                                                                                                                                                                                              " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "table.tableType                                                                                                                                                                                                                                                                                                                             " +
            "{                                                                                                                                                                                                                                                                                                                                           " +
            "    min-width:95%;                                                                                                                                                                                                                                                                                                                          " +
            "    width: 95% !important;                                                                                                                                                                                                                                                                                                                  " +
            "    margin: 0 auto !important;                                                                                                                                                                                                                                                                                                              " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "table.tableType td                                                                                                                                                                                                                                                                                                                          " +
            "{                                                                                                                                                                                                                                                                                                                                           " +
            "    display: table-cell !important;                                                                                                                                                                                                                                                                                                         " +
            "    min-width: auto !important;                                                                                                                                                                                                                                                                                                             " +
            "    width: auto !important;                                                                                                                                                                                                                                                                                                                 " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "img.imageMobile                                                                                                                                                                                                                                                                                                                             " +
            "{                                                                                                                                                                                                                                                                                                                                           " +
            "    width: 100% !important;                                                                                                                                                                                                                                                                                                                 " +
            "    height: auto !important;                                                                                                                                                                                                                                                                                                                " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "*.hideThis { display: none !important; }                                                                                                                                                                                                                                                                                                    " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "*.to90 { width: 93%; }                                                                                                                                                                                                                                                                                                                      " +
            "}                                                                                                                                                                                                                                                                                                                                           " +
            "</style>                                                                                                                                                                                                                                                                                                                                    " +
            "</head>                                                                                                                                                                                                                                                                                                                                     " +
            "<body>                                                                                                                                                                                                                                                                                                                                      " +
            "<div style='display:none;font-size:1px;color:#333333;line-height:1px;max-height:0px;max-width:0px;opacity:0;overflow:hidden;'>                                                                                                                                                                                                              " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "</div>                                                                                                                                                                                                                                                                                                                                      " +
            "                                                                                                                                                                                                                                                                                                                                            " +
            "<table width='600' border='0' align='center' cellpadding='0' cellspacing='0' class='deviceWidth' bgcolor='#ffffff'>                                                                                                                                                                                                                         " +
            "  	<tr>                                                                                                                                                                                                                                                                                                                                     " +
            "    	<td valign='top' style='line-height:0px; font-size:0px; margin:0px;'>                                                                                                                                                                                                                                                                " +
            //"			<table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'>                                                                                                                                                                                                                                                   " +
            //"				<tr>                                                                                                                                                                                                                                                                                                                         " +
            //"					<td align='center' style='line-height:0px; font-size:0px; margin:0px;'><img src='https://vivovalorizaempresas.vivo.com.br/images/banner_voucher.png' width='600' height='293' alt='Benefícios exclusivos na QueroQuitar, só para cliente Vivo Valoriza Empresas.' class='imageMobile'></td>                              " +
            //"				</tr>                                                                                                                                                                                                                                                                                                                        " +
            //"			</table>                                                                                                                                                                                                                                                                                                                         " +
            "           <br><br><br><br>                                                                                                                                                                                                                                                                                                                 " +
            "			<table width='90%' border='0' cellspacing='0' cellpadding='0' align='center' >                                                                                                                                                                                                                                                   " +
            "				<tr>                                                                                                                                                                                                                                                                                                                         " +
            "					<td align='center' style='font-family: Trebuchet MS, Verdana, Geneva, sans-serif; font-size:24px; line-height:18px; color:#666666;-ms-text-size-adjust:none;-webkit-text-size-adjust:none; text-align:left!important;mso-line-height-rule: exactly;'>                                                                    " +
            "						<span style='font-size:18px; color:#660099'>Olá, " + email + " </span>                                                                                                                                                                                                                                               " +
            "						<br><br>                                                                                                                                                                                                                                                                                                             " +
            "						<span style='font-size:16px; color:#666666'>                                                                                                                                                                                                                                                                         " +
            "                           <a href = '" + $"{Configuration.CreatorPlay_URL}?code={codeForgotPassword}" + "'>Clique aqui </a> para cadastrar uma nova senha de acesso ao CreatorPlay.                                                                                                                                                        " +
            "						</span>                                                                                                                                                                                                                                                                                                              " +
            "					</td>                                                                                                                                                                                                                                                                                                                    " +
            "			  	</tr>                                                                                                                                                                                                                                                                                                                        " +
            "			</table>                                                                                                                                                                                                                                                                                                                         " +
            "		</td>                                                                                                                                                                                                                                                                                                                                " +
            "	</tr>                                                                                                                                                                                                                                                                                                                                    " +
            "</table>                                                                                                                                                                                                                                                                                                                                    " +
            "</body>                                                                                                                                                                                                                                                                                                                                     " +
            "</html>";

        return corpoEmail;

    }

    private async Task<TypeError?> ValidateRequest(UsersForgotPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (!request.Email.HasValue())
            return TypeError.EmailRequired;

        if (!Extensions.IsValidEmail(request.Email))
            return TypeError.InvalidEmail;

        var emailExists = await _context.ApplicationUser.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (!emailExists)
            return TypeError.EmailNotFound;

        return null;
    }
}
