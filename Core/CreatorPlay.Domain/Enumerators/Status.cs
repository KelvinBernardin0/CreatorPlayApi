using System.ComponentModel;

namespace CreatorPlay.Domain.Enumerators;

public enum Status
{
    [Description("Inativo")]
    Inactive = 0,
    [Description("Ativo")]
    Active = 1
}
