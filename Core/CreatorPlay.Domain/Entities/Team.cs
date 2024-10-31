using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Domain.Entities;

public class Team : BaseEntity
{
	public int TeamId { get; set; }
	public string Name { get; set; }
	public string LeaderId { get; set; }
	public Status Status { get; set; } = Status.Active;

	public ICollection<TeamMember> Members { get; set; }
}

