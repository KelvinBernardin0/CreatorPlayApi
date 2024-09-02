using CreatorPlay.Application.Common.Models.SimplifiqueAPI;
using CreatorPlay.Common;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CreatorPlay.Application.Services;

public class EmailServiceBase
{
	private readonly HttpClient _httpClient;

	public EmailServiceBase(HttpClient httpClient)
	{
		_httpClient = httpClient;
		_httpClient.BaseAddress = new Uri(Configuration.Simplifique_BaseUrl);
	}

	public async Task<TResponse> SendRequestAsync<TResponse>(object payload)
	{
		var request = CreateHttpRequest(payload);
		var response = await _httpClient.SendAsync(request);
		var responseString = await response.Content.ReadAsStringAsync();

		if (response.StatusCode is HttpStatusCode.Unauthorized 
		 || response.StatusCode is  HttpStatusCode.Forbidden 
		 || response.StatusCode is HttpStatusCode.BadRequest
		 || response.StatusCode >= HttpStatusCode.InternalServerError)
		{
			throw new Exception(responseString);
		}

        return responseString.FromJson<TResponse>();
	}

	public static HttpRequestMessage CreateHttpRequest(object payload)
	{
        var emailConfiguration = new EmailConfiguration();
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        
		var httpRequest = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(EnsureStartsWithHttp($"{Configuration.Simplifique_BaseUrl}/{emailConfiguration.ServiceName}?token={emailConfiguration.Token}")),
            Content = stringContent
        };

        return httpRequest;
    }

    private static string EnsureStartsWithHttp(string url) => url.StartsWith("http") ? url : $"https://{url}";
}