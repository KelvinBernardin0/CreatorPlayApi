namespace CreatorPlay.Domain.Entities;

public abstract class BaseEntity
{
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public DateTime? ModifiedAt { get; set; }
}
