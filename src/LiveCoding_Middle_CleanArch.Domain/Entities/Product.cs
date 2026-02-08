namespace LiveCoding_Middle_CleanArch.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public float Price { get; set; }
    public float DiscountPercentage { get; set; }
    public float Rating { get; set; }
    public int Stock { get; set; }
    public string[] Tags { get; set; }
    public string Brand { get; set; }
    public string Sku { get; set; }
    public int Weight { get; set; }
    public Dimensions Dimensions { get; set; }
    public string WarrantyInformation { get; set; }
    public string ShippingInformation { get; set; }
    public string AvailabilityStatus { get; set; }
    public Review[] Reviews { get; set; }
    public string ReturnPolicy { get; set; }
    public int MinimumOrderQuantity { get; set; }
    public Meta Meta { get; set; }
    public string[] Images { get; set; }
    public string Thumbnail { get; set; }
}

