using CreatorPlay.Application.Common.Models.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CreatorPlay.Application.Common.Models.Response
{
	public class ResponseApi<T>
	{
		public bool Success { get; private set; }

		[JsonIgnore]
		public int HttpStatusCode { get; private set; }
		public T Data { get; set; }
		public ResponseError Error { get; private set; }

		public ResponseApi() { }

		public ResponseApi(bool success, int httpStatusCode, T data = default, ResponseError? error = null)
		{
			if (success)
				SetSuccess(data, httpStatusCode);
			else
				SetError(error, httpStatusCode);
		}

		public ResponseApi(T data = default, int? statusCode = null) => SetSuccess(data, statusCode ?? StatusCodes.Status200OK);

		public ResponseApi(ResponseError error, int httpStatusCode) => SetError(error, httpStatusCode);

		public void SetSuccess(T data, int httpStatusCode)
		{
			Data = data;
			HttpStatusCode = httpStatusCode;
			Success = true;
		}

		public void SetSuccess(int httpStatusCode)
		{
			HttpStatusCode = httpStatusCode;
			Success = true;

		}

		public void SetError(ResponseError error, int httpStatusCode)
		{
			Error = error;
			HttpStatusCode = httpStatusCode;
		}

		public void SetError(ResponseError error) => Error = error;

		public void SetStatusCode(int httpStatusCode) => HttpStatusCode = httpStatusCode;

		[JsonIgnore]
		public object GetResultData => Success ? Data : Error;
	}
}
