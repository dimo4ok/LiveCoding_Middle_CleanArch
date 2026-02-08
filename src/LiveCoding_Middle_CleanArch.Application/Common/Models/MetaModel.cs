namespace LiveCoding_Middle_CleanArch.Application.Common.Models;

public record MetaModel(
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Barcode,
    string QrCode
    );