namespace Catalog_Service;

public class CatalogItem 
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int CatalogId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsAvailabe { get; set; }
}
