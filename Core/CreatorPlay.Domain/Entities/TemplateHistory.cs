using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Domain.Entities;

public class TemplateHistory : BaseEntity
{
    public int Id { get; set; }
    public string AspNetUsersId { get; set; }
    public string Name { get; set; }
	public string Template { get; set; }
	public string Options { get; set; }
	public TemplateStatus TemplateStatus { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
}
