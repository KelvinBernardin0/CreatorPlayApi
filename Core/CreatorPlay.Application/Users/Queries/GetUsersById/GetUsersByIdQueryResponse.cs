using CreatorPlay.Domain.Entities;

namespace CreatorPlay.Application.Users.Queries.GetUsersById;

public class GetUsersByIdQueryResponse(ApplicationUser user)
{
	public string Id { get; set; } = user.Id;
	public string? UserName { get; set; } = user.UserName;
	public string? Email { get; set; } = user.Email;
}