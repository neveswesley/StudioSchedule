namespace StudioSchedule.Domain.Entities;

public class Room : BaseEntity
{
    public Guid StudioId { get; set; }
    public Studio Studio { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourPrice { get; set; }
    public int OpenHour { get; set; }
    public int CloseHour { get; set; }
    public string Description { get; set; } = string.Empty;
}