namespace CreatorPlay.Domain.Entities;

public class History : BaseEntity
{
	public int Id { get; set; }
	public string UserId { get; set; }
	public int? TeamId { get; set; }
	public string Description { get; set; }
}
