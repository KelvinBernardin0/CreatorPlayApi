namespace CreatorPlay.Domain.Entities;

public class History : BaseEntity
{  
    public int Id { get; private set; }
	public string UserId { get; private set; }
	public int TeamId { get; private set; }
	public string Description { get; private set; }
    public void AddHistory(int teamId, string userId, string description)
    {
        UserId = userId;
        TeamId = teamId;
        Description = description;
    }    

}
