namespace CreatorPlay.Domain.Entities;

public class GlobalConfiguration : BaseEntity
{
	public int Id { get; set; }
	public string EmailLogin { get; set; }
	public string EmailPassword { get; set; }
}
