using CreatorPlay.Common;
using System.Globalization;

namespace CreatorPlay.Application.TemplateHistory.Queries.GetTemplateHistory;

public class GetTemplateHistoryQueryResponse(Domain.Entities.TemplateHistory template)
{
    public int Id { get; set; } = template.Id;
    public string Name { get; set; } = template.Name;
    public string Template { get; set; } = template.Template.Replace("\n","");
    public string CreatedAt { get; set; } = template.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    public string TemplateStatus { get; set; } = template.TemplateStatus == Domain.Enumerators.TemplateStatus.Draft
                                                                          ? Domain.Enumerators.TemplateStatus.Draft.GetDescription()
                                                                          : Domain.Enumerators.TemplateStatus.Completed.GetDescription();
}
