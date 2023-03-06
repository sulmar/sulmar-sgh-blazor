namespace SGH.Shopper.Domain;

public abstract class BaseEntity : Base
{
    public int Id { get; set; }

    public abstract bool HasContent(string content);
}
