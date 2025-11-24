namespace StudioSchedule.Domain.Entities;

public class Studio : BaseEntity
{
    public Studio(string name, string address, string city, string description, string imageUrl, Guid userId)
    {
        Name = name;
        Address = address;
        City = city;
        Description = description;
        ImageUrl = imageUrl;
        UserId = userId;
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Studio() {}
}