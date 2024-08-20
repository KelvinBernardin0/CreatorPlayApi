
using CreatorPlay.Common;
using CreatorPlay.Domain.Entities;
using CreatorPlay.Domain.Enumerators;

namespace CreatorPlay.Application.Users.Queries.GetAccessProfile;

public class GetAccessProfileQueryResponse(ApplicationRole role)
{
	public string Id { get; set; } = role.Id;
	public string? AccessProfile { get; set; } = role.Name == Roles.Default_Access.ToString()
														    ? Roles.Default_Access.GetDescription()
														    : Roles.Commercial_Access.GetDescription();
}
