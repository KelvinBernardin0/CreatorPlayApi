using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Domain.Entities;

public class Team : BaseEntity
{
	public int Id { get; private set; }
	public string? Name { get; private set; }
	public string? LeaderId { get; private set; }
	public string Creator { get; set; }
	public string Description { get; set; }
	public DateTime DeactivationDate { get; set; }
	public bool Active { get; set; }
	public Status Status { get; private set; } = Status.Active;

	public ICollection<TeamMember> Members { get; set; }
	public Team(string name, string leaderId,string description,string creator)
	{
		Name = name;
		LeaderId = leaderId;
		Description=description;
		Creator=creator;
		Active=true;
		DeactivationDate= DateTime.Parse("1800/01/01");

	}


    public void SetStatusTeam(Status status)
    {
		Status = status;
    }
}