namespace LiveCoding_Middle_CleanArch.Application.Common.Models;

public record ProductListModel(
    ProductModel[] Products,
    int Total,
    int Skip,
    int Limit);
