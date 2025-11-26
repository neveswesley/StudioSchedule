namespace StudioSchedule.Domain.DTO;

public class RoomResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourPrice { get; set; }
    public int OpenHour { get; set; }
    public int CloseHour { get; set; }
    public string Description { get; set; } = string.Empty;
}