using System.ComponentModel;

namespace CreatorPlay.Domain.Enumerators;

public enum TemplateStatus
{
    [Description("Rascunho")]
    Draft = 0,
    [Description("Concluído")]
    Completed = 1
}
