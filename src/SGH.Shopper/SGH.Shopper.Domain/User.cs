namespace SGH.Shopper.Domain;

public class User : BaseEntity
{    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
    public Company Company { get; set; }
}
