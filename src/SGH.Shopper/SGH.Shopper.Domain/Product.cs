namespace SGH.Shopper.Domain;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Barcode { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal PriceDiscount { get; set;}
    public Size? Size { get; set; }

}

public enum Size
{
    Small,
    Medium,
    Large,
}
