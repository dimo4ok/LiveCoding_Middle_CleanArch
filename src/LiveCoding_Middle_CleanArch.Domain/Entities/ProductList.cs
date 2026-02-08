namespace LiveCoding_Middle_CleanArch.Domain.Entities;

public class ProductList
{
    public Product[] Products { get; set; }
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}
