using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Domain.Entities;

namespace LiveCoding_Middle_CleanArch.Application.Common.Extensions;

public static class EntityExtensions
{
    public static Product ToEntity(this UpdateProductModel model, int id) =>
         new()
         {
             Id = id,
             Title = model.Title,
             Description = model.Description,
             Category = model.Category,
             Price = model.Price,
             DiscountPercentage = model.DiscountPercentage,
             Rating = model.Rating,
             Stock = model.Stock,
             Tags = model.Tags,
             Brand = model.Brand,
             Sku = model.Sku,
             Weight = model.Weight,
             Dimensions = model.Dimensions.ToEntity(),
             WarrantyInformation = model.WarrantyInformation,
             ShippingInformation = model.ShippingInformation,
             AvailabilityStatus = model.AvailabilityStatus,
             Reviews = model.Reviews.Select(r => r.ToEntity()).ToArray(),
             ReturnPolicy = model.ReturnPolicy,
             MinimumOrderQuantity = model.MinimumOrderQuantity,
             Meta = model.Meta.ToEntity(),
             Images = model.Images,
             Thumbnail = model.Thumbnail
         };

    public static Dimensions ToEntity(this DimensionsModel model) =>
        new()
        {
            Width = model.Width,
            Height = model.Height,
            Depth = model.Depth
        };

    public static Review ToEntity(this ReviewModel model) =>
        new()
        {
            Rating = model.Rating,
            Comment = model.Comment,
            Date = model.Date,
            ReviewerName = model.ReviewerName,
            ReviewerEmail = model.ReviewerEmail
        };

    public static Meta ToEntity(this MetaModel model) =>
        new()
        {
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Barcode = model.Barcode,
            QrCode = model.QrCode
        };


    public static ProductListModel ToModel(this ProductList productList) =>
        new(
            productList.Products.Select(p => p.ToModel()).ToArray(),
            productList.Total,
            productList.Skip,
            productList.Limit
        );

    public static ProductModel ToModel(this Product product) =>
        new(
            product.Id,
            product.Title,
            product.Description,
            product.Category,
            product.Price,
            product.DiscountPercentage,
            product.Rating,
            product.Stock,
            product.Tags,
            product.Brand,
            product.Sku,
            product.Weight,
            product.Dimensions.ToModel(),
            product.WarrantyInformation,
            product.ShippingInformation,
            product.AvailabilityStatus,
            product.Reviews.Select(r => r.ToModel()).ToArray(),
            product.ReturnPolicy,
            product.MinimumOrderQuantity,
            product.Meta.ToModel(),
            product.Images,
            product.Thumbnail
        );

    public static DimensionsModel ToModel(this Dimensions dimensions) =>
        new(
            dimensions.Width,
            dimensions.Height,
            dimensions.Depth
        );

    public static ReviewModel ToModel(this Review review) =>
        new(
            review.Rating,
            review.Comment,
            review.Date,
            review.ReviewerName,
            review.ReviewerEmail
        );

    public static MetaModel ToModel(this Meta meta) =>
        new(
            meta.CreatedAt,
            meta.UpdatedAt,
            meta.Barcode,
            meta.QrCode
        );
}
