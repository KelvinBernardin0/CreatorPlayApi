using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Domain.Entities;

public class TeamMember : BaseEntity
{
	public int TeamMemberId { get; set; }
	public int TeamId { get; set; }
	public string UserId { get; set; }
	public bool IsLeader { get; set; }
	public Status Status { get; set; } = Status.Active;
}
