using System.ComponentModel;

namespace CreatorPlay.Domain.Enumerators;

public enum TypeError
{
    #region VVEAPI: 800 A 899

    [Description("Erro na requisi��o da API de valida��o de CNPJ.")]
	ErrorCNPJvalidationAPI = 800,

	#endregion

	#region DEFAULT: 900 a 999

	[Description("Ocorreu um erro interno.")]
	DefaultError = 900,

	[Description("Campo obrigat�rio.")]
	Required = 901,

	[Description("CNPJ inv�lido.")]
	InvalidCnpj = 902,

	[Description("CNPJ informado n�o encontrado.")]
	CNPNotFound = 903,

	[Description("N�o foi poss�vel encontrar a Raz�o Social da Empresa, verifique o CNPJ ou entre em contato com o seu RH.")]
	CompanyNameNotFound = 904,

	[Description("Ocorreu o erro ao tentar enviar o e-mail. Contatar o departamento de suporte t�cnico.")]
	SendMailFail = 905,

	#endregion

	#region IDENTITY: 1000 a 1099

	[Description("Senhas devem conter ao menos um caracter especial.")]
	PasswordRequiresNonAlphanumeric = 1000,

	[Description("Email j� est� sendo utilizado.")]
	DuplicateEmail = 1001,

	[Description("A permiss�o j� est� sendo utilizada.")]
	DuplicateRoleName = 1002,

	[Description("Login j� est� sendo utilizado.")]
	DuplicateUserName = 1003,

	[Description("Email inv�lido.")]
	InvalidEmail = 1004,

	[Description("A permiss�o � inv�lida.")]
	InvalidRoleName = 1005,

	[Description("Token inv�lido.")]
	InvalidToken = 1006,

	[Description("Login � inv�lido, deve conter apenas letras ou d�gitos.")]
	InvalidUserName = 1007,

	[Description("J� existe um usu�rio com este login.")]
	LoginAlreadyAssociated = 1008,

	[Description("Senha incorreta.")]
	PasswordMismatch = 1009,

	[Description("Senhas devem conter ao menos um digito ('0'-'9').")]
	PasswordRequiresDigit = 1010,

	[Description("Senhas devem conter ao menos um caracter com letra min�scula ('a'-'z').")]
	PasswordRequiresLower = 1011,

	[Description("Senhas devem conter ao menos um caracter com letra mai�scula ('A'-'Z').")]
	PasswordRequiresUpper = 1012,

	[Description("Senhas devem conter ao menos 8 caracteres.")]
	PasswordTooShort = 1013,

	[Description("Usu�rio j� possui uma senha definida.")]
	UserAlreadyHasPassword = 1014,

	[Description("Usu�rio j� possui a permiss�o.")]
	UserAlreadyInRole = 1015,

	[Description("Lockout n�o est� habilitado para este usu�rio.")]
	UserLockoutNotEnabled = 1016,

	[Description("Usu�rio n�o tem a permiss�o para esta fun��o.")]
	UserNotInRole = 1017,

    [Description("Falha ao tentar cadastrar a nova senha, tente mais tarde ou entre em contato com o departamento de suporte t�cnico.")]
    ResetPasswordFail = 1018,

    #endregion

    #region USER: 1100 a 1199

    [Description("Usu�rio n�o encontrado.")]

	UserNotFound = 1100,

	[Description("Ocorreu um erro ao tentar alterar o e-mail.")]
	FailUpdateEmail = 1101,

	[Description("Ocorreu um erro ao tentar alterar o telefone.")]
	FailUpdatePhone = 1102,

	[Description("Usu�rio j� existente, isso quer dizer que j� existe um usu�rio com esse n�mero de telefone.")]
	UserAlreadyExists = 1103,

	[Description("Usu�rio n�o autorizado.")]
	Unauthorized = 1104,

	[Description("Usu�rio � obrigat�rio.")]
	UserRequired = 1105,

	[Description("Email � obrigat�rio.")]
	EmailRequired = 1106,

	[Description("Senha � obrigat�rio.")]
	PasswordRequired = 1107,

	[Description("Confirma��o de Senha � obrigat�rio.")]
	ConfirmPasswordRequired = 1108,

	[Description("N�mero de telefone inv�lido")]
	InvalidPhone = 1109,

	[Description("Nova Senha devem conter ao menos 8 caracteres.")]
	NewPasswordTooShort = 1110,

	[Description("Nova Senha � obrigat�rio.")]
	NewPasswordRequired = 1111,

	[Description("As senhas devem ser iguais.")]
	ConfirmPasswordNotEqual = 1112,

	[Description("Senha incorreta.")]
	IncorrectPassword = 1113,

	[Description("Erro ao Reativar Usuario.")]
	ErrorReactivatingUser = 1114,

	[Description("O usu�rio n�o est� bloqueado ou o per�odo de bloqueio j� expirou.")]
	ErroHasExpired = 1115,

	[Description("Id do usu�rio n�o encontrado.")]
	InvalidId = 1116,

	[Description("Permiss�o de usu�rio n�o encontrada.")]
	RoleNotFound = 1117,

	[Description("Telefone j� existe.")]
	PhoneAlreadyExists = 1118,

	[Description("ClientId e Secret inv�lidos.")]
	InvalidCredentials = 1119,

	[Description("Perfis de Acesso de usu�rios n�o encontrados.")]
	AccesProfileNotFound = 1120,

    [Description("E-mail n�o encontrado.")]
    EmailNotFound = 1121,

    [Description("C�digo de recupera��o de senha inv�lido, tente mais tarde ou entre em contato com o departamento de suporte t�cnico.")]
    CodeForgotPasswordInvalid = 1122,

	#endregion

	#region TEMPLATEHISTORY: 1200 a 1299

	[Description("Nome do template � obrigat�rio.")]
	TemplateNameRequired = 1200,

	[Description("Template � obrigat�rio.")]
	TemplateRequired = 1201,

	[Description("Voc� n�o tem templates salvos para serem listados.")]
	TemplateNotFound = 1202,
	#endregion

	#region IMAGES: 1300 a 1399

	[Description("Nome das imagens � obrigat�rio.")]
	ImagesNameRequired = 1300,

	[Description("Images � obrigat�rio.")]
	ImagesRequired = 1301,

	#endregion

	#region TEAM: 1400 a 1499

	[Description("Nome da equipe � obrigat�rio.")]
	TeamNameRequired = 1400,    

    [Description("Id do l�der da equipe � obrigat�rio.")]
	TeamLeaderIdRequired = 1401,

    [Description("Já existe uma equipe com esse nome.")]
    TeamAlreadyExistsInTeam = 1402,
	
	[Description("Id do criador da equipe � obrigat�rio.")]
	TeamCreatorRequired = 1403,
	[Description("Apenas o Lider pode remover um membro da equipe.")]
    OnlyLeaderCanDeleteTeam = 1404,


    #endregion

    #region TEAMMEMBER: 1500 a 1599

    [Description("Usu�rio j� est� cadastrado na equipe.")]
	MemberAlreadyExistsInTeam = 1500,
	[Description("A equipe já possui um lider.")]
	LeaderAlreadyExistsInTeam = 1501,

	[Description("Apenas o lider da equipe pode executar essa ação.")]
	NotTeamLeader = 1502,


    #endregion

    #region History: 1600 a 1699

    [Description("Descri��o � obrigat�rio.")]
    HistoryDescriptionRequired = 1600,

    #endregion
	#region DeleteTeamMember: 1700 a 1799

    [Description("Apenas o Lider pode remover um membro da equipe.")]
    OnlyLeaderCanRemoveTeamMember = 1700,

    #endregion
}