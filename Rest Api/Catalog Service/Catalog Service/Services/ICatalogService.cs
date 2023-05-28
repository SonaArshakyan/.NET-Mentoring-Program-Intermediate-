using Catalog_Service.Models;

namespace Catalog_Service.Services;

public interface ICatalogService
{
    Task<List<Category>> GetCategoryListAsync();
    Task<List<CatalogItem>> GetCatalogItemsAsync(int categoryId, PaginationFilter filter);
}
