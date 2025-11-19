using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Domain.DTO;

public class UserResponse
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
}