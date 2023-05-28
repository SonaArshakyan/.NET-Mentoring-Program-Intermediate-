namespace Catalog_Service;
public class Catalog
{
    public int CatalogId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public List<Category> Categories { get; set; }
    List<CatalogItem> Items { get; set; }
}