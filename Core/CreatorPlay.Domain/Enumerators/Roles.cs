using System.ComponentModel;

namespace CreatorPlay.Domain.Enumerators;

public enum Roles
{
	[Description("Acesso Padrão")]
	Default_Access = 1,
	[Description("Acesso Comercial")]
	Commercial_Access = 2
}