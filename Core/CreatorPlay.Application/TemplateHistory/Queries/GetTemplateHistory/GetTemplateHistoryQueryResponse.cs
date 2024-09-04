using CreatorPlay.Domain.Enumerators;
using System.Globalization;

namespace CreatorPlay.Application.TemplateHistory.Queries.GetTemplateHistory;

public class GetTemplateHistoryQueryResponse(Domain.Entities.TemplateHistory template)
{
    public int Id { get; set; } = template.Id;
    public string Name { get; set; } = template.Name;
    public string Template { get; set; } = template.Template.Replace("\n","");
    public string CreatedAt { get; set; } = template.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    public TemplateStatus TemplateStatus { get; set; } = template.TemplateStatus;
}
