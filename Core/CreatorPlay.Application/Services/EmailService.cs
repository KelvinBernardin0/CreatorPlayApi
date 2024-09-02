using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Application.Common.Models.SimplifiqueAPI;

namespace CreatorPlay.Application.Services;

public class EmailService(HttpClient httpClient) : EmailServiceBase(httpClient), IEmailService
{
    public async Task<ResponseApiSimplifique> SendMailAsync(EmailModel request)
        => await SendRequestAsync<ResponseApiSimplifique>(payload: request);
}
