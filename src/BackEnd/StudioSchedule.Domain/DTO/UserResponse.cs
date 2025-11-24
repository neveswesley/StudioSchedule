using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Domain.DTO;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public List<StudioResponse>? Studios { get; set; }
}