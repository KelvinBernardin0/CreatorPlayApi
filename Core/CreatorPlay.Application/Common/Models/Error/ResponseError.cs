using CreatorPlay.Common;
using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Application.Common.Models.Error;

public class ResponseError
{
	public int Code { get; set; }
	public string Message { get; set; }
    public IEnumerable<ResponseErrorItem> Errors { get; set; }
	
	public ResponseError() { }

	public ResponseError(int code, string message)
	{
		Code = code;
		Message = message;
	}

	public ResponseError(TypeError code, object value)
	{
		Code = code.GetHashCode();
		Message = code.GetDescription();
	}

	public ResponseError(TypeError code, string? message)
	{
		Code = code.GetHashCode();
		Message = message;
	}
}
