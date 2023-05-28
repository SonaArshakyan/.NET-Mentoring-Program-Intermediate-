namespace Catalog_Service.Services;

public interface ICatalogItemService
{
    Task<CatalogItem> GetItemAsync(int id);
    Task DeleteItemAsync(int id);
    Task<CatalogItem> CreateItem(CatalogItem item);
    Task UpdateItemAsync(CatalogItem item);
}
