using Microsoft.AspNetCore.Identity;

namespace CreatorPlay.Domain.Entities;

public class ApplicationUser : IdentityUser
{
	public DateTime CreatedAt { get; set; }
	public DateTime? ModifiedAt { get; set; }

    public ICollection<TemplateHistory> TemplateHistorys { get; set; }
}
