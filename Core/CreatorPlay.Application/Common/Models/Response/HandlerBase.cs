namespace CreatorPlay.Application.Common.Models.Response;

public class HandlerBase<TResponse>
{
    public HandlerBase() => Response = new ResponseApi<TResponse>();
    public ResponseApi<TResponse> Response { get; private set; }
}
