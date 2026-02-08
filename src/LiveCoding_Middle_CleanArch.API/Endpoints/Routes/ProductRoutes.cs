namespace LiveCoding_Middle_CleanArch.API.Endpoints.Routes;

public static class ProductRoutes
{
    private const string Base = "api/products";
    private const string ById = $"{{id:int}}";

    public const string GetAll = Base;
    public const string GetById = $"{Base}/{ById}";
    public const string Update = $"{Base}/{ById}";
    public const string Delete = $"{Base}/{ById}";

    public const string Fetch = $"{Base}/fetch";
}
