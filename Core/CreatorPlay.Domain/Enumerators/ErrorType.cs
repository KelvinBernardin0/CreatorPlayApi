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

	#endregion

	#region USUARIO: 1100 a 1199

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

	#endregion

	#region SETOR: 1200 a 1299

	[Description("Setor já existe.")]
	SectorAlreadyExists = 1200,

	[Description("Setor é obrigatório.")]
	SectorRequired = 1201,

	[Description("Setor não encontrado.")]
	SectorNotFound = 1202,

	[Description("Setor CNAE não encontrado.")]
	SectorCnaeNotFound = 1203,

	[Description("Setor CNAE já foi adicionado.")]
	SectorCnaeAlreadyExists = 1204,

	[Description("Não é possível excluir o Setor, pois há alguma Oferta que o utiliza, remova todas os registros do setor das ofertas e depois exclua-o novamente.")]
	LinkedOfferExists = 1205,

	#endregion

	#region DE-PARA: 1300 a 1399

	[Description("DeParaPerfil já existe.")]
	FromToProfileAlreadyExists = 1300,

	[Description("DeParaPerfil não encontrado.")]
	FromToProfileNotFound = 1301,

	[Description("Perfil não encontrado.")]
	ProfileNotFound = 1302,

	#endregion

	#region CATEGORIA: 1400 a 1499

	[Description("Categoria já existe.")]
	CategoryAlreadyExists = 1400,

	[Description("Categoria é obrigatório.")]
	CategoryRequired = 1401,

	[Description("Categoria não encontrada!")]
	CategoryNotFound = 1402,

	#endregion

	#region PARCEIRO: 1500 a 1599

	[Description("Parceiro não encontrado!")]
	PartnerNotFound = 1500,

	[Description("Pixel identificador não encontrado!")]
	IdentityPixelNotFound = 1501,

	[Description("Parceiro já existe!")]
	PartnerAlready = 1502,

	#endregion

	#region OFERTA: 1600 a 1699

	[Description("Oferta não encontrada!")]
	OfferNotFound = 1600,

	[Description("Atualização de dados e interesses.")]
	UserDataUpdate = 1601,

	#endregion

	#region GESTOR-RH: 1700 a 1799

	[Description("Não foi possível encontrar o Gestor.")]
	GestorNotFound = 1700,

	[Description("Não foi possível encontrar o Arquivo.")]
	InvalidFile = 1701,

	[Description("Colaborador não encontrado.")]
	ColaboradorNotFound = 1702,

	[Description("Seu CNPJ já possui um cadastro.")]
	UserGestorAlreadyExists = 1703,

	[Description("Já existe um colaborador cadastrado com essa chave.")]
	KeyAlreadyExists = 1704,

	#endregion

	#region SSO: 1800 a 1899

	[Description("JWT: Client não encontrado no token.")]
	ClientNaoEncontradoNoTokenMVE = 1800,

	[Description("JWT: Client não encontrado nas configurações do Vivo Valoriza.")]
	ClientMVENaoEncontradoNoVivoValoriza = 1801,

	[Description("Sesão expirada.")]
	SessaoExpirada = 1802,

	[Description("Falha ao criar o usuário na base de dados do VivoValoriza. Por favor, tente mais tarde ou entre em contato com o departamento de suporte técnico.")]
	FalhaNaCriacaoDeUsuario = 1803,

	[Description("Não foi possível autenticar o usuário. Por favor, tente mais tarde ou entre em contato com o departamento de suporte técnico.")]
	FalhaGetDadosGestor = 1804,

	[Description("Não foi possível autenticar o usuário. Cnpj informado na requisição inválido.")]
	CnpjGestorInvalido = 1805

	#endregion
}