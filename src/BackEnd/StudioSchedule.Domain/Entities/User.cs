using StudioSchedule.Domain.DTO;

namespace StudioSchedule.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<Studio>? Studios { get; set; } = [];
    public Role Role { get; set; }
}