namespace LiveCoding_Middle_CleanArch.Application.Common.Models;

public record ProductModel(
    int Id,
    string Title,
    string Description,
    string Category,
    float Price,
    float DiscountPercentage,
    float Rating,
    int Stock,
    string[] Tags,
    string Brand,
    string Sku,
    int Weight,
    DimensionsModel Dimensions,
    string WarrantyInformation,
    string ShippingInformation,
    string AvailabilityStatus,
    ReviewModel[] Reviews,
    string ReturnPolicy,
    int MinimumOrderQuantity,
    MetaModel Meta,
    string[] Images,
    string Thumbnail
    );
