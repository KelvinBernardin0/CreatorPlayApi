namespace CreatorPlay.Application.Common.Exceptions;

public class BadRequestException : Exception
{
	public BadRequestException(string message) : base(message)
	{
	}

	public BadRequestException(Exception ex, string message) : base(message, ex)
	{
	}
}
