using CreatorPlay.Application.Common.Models.SimplifiqueAPI;

namespace CreatorPlay.Application.Common.Interfaces;

public interface IEmailService
{
    Task<ResponseApiSimplifique> SendMailAsync(EmailModel request);
}
