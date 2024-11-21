using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Domain.Entities;

public class Team : BaseEntity
{
	public int Id { get; private set; }
	public string? Name { get; private set; }
	public string? LeaderId { get; private set; }
	public Status Status { get; private set; } = Status.Active;

	public ICollection<TeamMember> Members { get; set; }

	public void AddTeam(string name, string leaderId)
	{
		Name = name;
		LeaderId = leaderId;
	}
    public void SetStatusTeam(Status status)
    {
		Status = status;
    }

    
}