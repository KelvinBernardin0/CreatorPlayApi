using Microsoft.AspNetCore.Identity;

namespace CreatorPlay.Domain.Entities;

public class ApplicationUser : IdentityUser
{
	public DateTime CreatedAt { get; set; }
	public DateTime? ModifiedAt { get; set; }

    public ICollection<TemplateHistory> TemplateHistories { get; set; }
	public ICollection<TeamMember> Teams { get; set; }
	public ICollection<History> Histories { get; set; }
}
