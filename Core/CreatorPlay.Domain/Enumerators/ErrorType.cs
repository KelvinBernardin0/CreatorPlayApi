using System.ComponentModel;

namespace CreatorPlay.Domain.Enumerators;

public enum TypeError
{
    #region VVEAPI: 800 A 899

    [Description("Erro na requisição da API de validação de CNPJ.")]
	ErrorCNPJvalidationAPI = 800,

	#endregion

	#region DEFAULT: 900 a 999

	[Description("Ocorreu um erro interno.")]
	DefaultError = 900,

	[Description("Campo obrigatório.")]
	Required = 901,

	[Description("CNPJ inválido.")]
	InvalidCnpj = 902,

	[Description("CNPJ informado não encontrado.")]
	CNPNotFound = 903,

	[Description("Não foi possível encontrar a Razão Social da Empresa, verifique o CNPJ ou entre em contato com o seu RH.")]
	CompanyNameNotFound = 904,

	[Description("Ocorreu o erro ao tentar enviar o e-mail. Contatar o departamento de suporte técnico.")]
	SendMailFail = 905,

	#endregion

	#region IDENTITY: 1000 a 1099

	[Description("Senhas devem conter ao menos um caracter especial.")]
	PasswordRequiresNonAlphanumeric = 1000,

	[Description("Email já está sendo utilizado.")]
	DuplicateEmail = 1001,

	[Description("A permissão já está sendo utilizada.")]
	DuplicateRoleName = 1002,

	[Description("Login já está sendo utilizado.")]
	DuplicateUserName = 1003,

	[Description("Email inválido.")]
	InvalidEmail = 1004,

	[Description("A permissão é inválida.")]
	InvalidRoleName = 1005,

	[Description("Token inválido.")]
	InvalidToken = 1006,

	[Description("Login é inválido, deve conter apenas letras ou dígitos.")]
	InvalidUserName = 1007,

	[Description("Já existe um usuário com este login.")]
	LoginAlreadyAssociated = 1008,

	[Description("Senha incorreta.")]
	PasswordMismatch = 1009,

	[Description("Senhas devem conter ao menos um digito ('0'-'9').")]
	PasswordRequiresDigit = 1010,

	[Description("Senhas devem conter ao menos um caracter com letra minúscula ('a'-'z').")]
	PasswordRequiresLower = 1011,

	[Description("Senhas devem conter ao menos um caracter com letra maiúscula ('A'-'Z').")]
	PasswordRequiresUpper = 1012,

	[Description("Senhas devem conter ao menos 8 caracteres.")]
	PasswordTooShort = 1013,

	[Description("Usuário já possui uma senha definida.")]
	UserAlreadyHasPassword = 1014,

	[Description("Usuário já possui a permissão.")]
	UserAlreadyInRole = 1015,

	[Description("Lockout não está habilitado para este usuário.")]
	UserLockoutNotEnabled = 1016,

	[Description("Usuário não tem a permissão para esta função.")]
	UserNotInRole = 1017,

    [Description("Falha ao tentar cadastrar a nova senha, tente mais tarde ou entre em contato com o departamento de suporte técnico.")]
    ResetPasswordFail = 1018,

    #endregion

    #region USER: 1100 a 1199

    [Description("Usuário não encontrado.")]

	UserNotFound = 1100,

	[Description("Ocorreu um erro ao tentar alterar o e-mail.")]
	FailUpdateEmail = 1101,

	[Description("Ocorreu um erro ao tentar alterar o telefone.")]
	FailUpdatePhone = 1102,

	[Description("Usuário já existente, isso quer dizer que já existe um usuário com esse número de telefone.")]
	UserAlreadyExists = 1103,

	[Description("Usuário não autorizado.")]
	Unauthorized = 1104,

	[Description("Usuário é obrigatório.")]
	UserRequired = 1105,

	[Description("Email é obrigatório.")]
	EmailRequired = 1106,

	[Description("Senha é obrigatório.")]
	PasswordRequired = 1107,

	[Description("Confirmação de Senha é obrigatório.")]
	ConfirmPasswordRequired = 1108,

	[Description("Número de telefone inválido")]
	InvalidPhone = 1109,

	[Description("Nova Senha devem conter ao menos 8 caracteres.")]
	NewPasswordTooShort = 1110,

	[Description("Nova Senha é obrigatório.")]
	NewPasswordRequired = 1111,

	[Description("As senhas devem ser iguais.")]
	ConfirmPasswordNotEqual = 1112,

	[Description("Senha incorreta.")]
	IncorrectPassword = 1113,

	[Description("Erro ao Reativar Usuario.")]
	ErrorReactivatingUser = 1114,

	[Description("O usuário não está bloqueado ou o período de bloqueio já expirou.")]
	ErroHasExpired = 1115,

	[Description("Id do usuário não encontrado.")]
	InvalidId = 1116,

	[Description("Permissão de usuário não encontrada.")]
	RoleNotFound = 1117,

	[Description("Telefone já existe.")]
	PhoneAlreadyExists = 1118,

	[Description("ClientId e Secret inválidos.")]
	InvalidCredentials = 1119,

	[Description("Perfis de Acesso de usuários não encontrados.")]
	AccesProfileNotFound = 1120,

    [Description("E-mail não encontrado.")]
    EmailNotFound = 1121,

    [Description("Código de recuperação de senha inválido, tente mais tarde ou entre em contato com o departamento de suporte técnico.")]
    CodeForgotPasswordInvalid = 1122,

	#endregion

	#region TEMPLATEHISTORY: 1200 a 1299

	[Description("Nome do template é obrigatório.")]
	TemplateNameRequired = 1200,

	[Description("Template é obrigatório.")]
	TemplateRequired = 1201,

	[Description("Você não tem templates salvos para serem listados.")]
	TemplateNotFound = 1202,
	#endregion

	#region images: 1203 a 1204

	[Description("Nome das imagens é obrigatório.")]
	ImagesNameRequired = 1203,

	[Description("Images é obrigatório.")]
	ImagesRequired = 1204,






	#endregion
}