using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Domain.Entities;

public class ApiAuthentication : BaseEntity
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string ClientId { get; set; }
	public string Secret { get; set; }
	public string AuthenticationEndpoint { get; set; }
	public Status Status { get; set; } = Status.Active;
}
