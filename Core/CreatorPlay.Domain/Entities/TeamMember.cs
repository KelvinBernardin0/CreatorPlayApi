using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Domain.Entities;

public class TeamMember : BaseEntity
{
	public int Id { get; private set; }
	public int TeamId { get; private set; }
	public string UserId { get; private set; }
	public bool IsLeader { get; private set; }
	public Status Status { get; private set; } = Status.Active;

	public void AddTeamMember(int teamId, string userId, bool isLeader)
	{
		TeamId = teamId;
		UserId = userId;
		IsLeader = isLeader;
	}

	public void AddTeamMember(int teamId, string userId)
	{
		TeamId = teamId;
		UserId = userId;
		IsLeader = false;
	}
}
