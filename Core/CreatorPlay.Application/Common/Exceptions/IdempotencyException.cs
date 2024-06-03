namespace CreatorPlay.Application.Common.Exceptions;

public class IdempotencyException : BadRequestException
{
	public IdempotencyException(string businessMessage) : base(businessMessage)
	{
	}
}
