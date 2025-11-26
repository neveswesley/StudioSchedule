using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Domain.DTO;

public class StudioResponse
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<RoomResponse> Rooms { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}