namespace CreatorPlay.Application.Users.Queries.GetUsers;

public class GetUsersQueryResponse(Domain.Entities.ApplicationUser user)
{
	public string Id { get; set; } = user.Id;
	public string? UserName { get; set; } = user.UserName;
	public string? Email { get; set; } = user.Email;
}