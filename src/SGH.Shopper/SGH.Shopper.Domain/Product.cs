namespace SGH.Shopper.Domain;

using System.ComponentModel.DataAnnotations;

public class Product : BaseEntity
{
    // [Required, StringLength(20, MinimumLength = 3)]
    public string Name { get; set; }
    public string Description { get; set; }
    // [StringLength(14, MinimumLength = 14)]
    public string Barcode { get; set; }
    public string Color { get; set; }
    // [Range(1, 1000)]
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal PriceDiscount { get; set;}
    public Size? Size { get; set; }

    public override bool HasContent(string content) => 
        Name.Contains(content, StringComparison.OrdinalIgnoreCase)
        || Description.Contains(content, StringComparison.OrdinalIgnoreCase);
    
}

public enum Size
{
    Small,
    Medium,
    Large,
}
