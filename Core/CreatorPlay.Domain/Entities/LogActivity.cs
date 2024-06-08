namespace CreatorPlay.Domain.Entities;

public class LogActivity
{
	public int Id { get; set; }
	public int LogLevel { get; set; }
	public string User { get; set; }
	public string TypeUser { get; set; }
	public DateTime CreatedAt { get; set; }
	public string Message { get; set; }

	public LogActivity(int logLevel, string user, string typeUser, string message)
	{
		LogLevel = logLevel;
		User = user;
		TypeUser = typeUser;
		Message = message;
		CreatedAt = DateTime.Now;
	}
}
