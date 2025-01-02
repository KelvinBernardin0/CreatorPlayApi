using CreatorPlay.Domain.Entities;

namespace CreatorPlay.Application.TeamMember.Queries.GetTeamMember;

public class GetTeamMemberQueryResponse()
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public List<User> Members { get; set; }
    

}

