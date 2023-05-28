using Catalog_Service.DataAccessLayer;
using Catalog_Service.Models;

namespace Catalog_Service.Services;

public class CatalogService : ICatalogService
{
    public Task<List<CatalogItem>> GetCatalogItemsAsync(int categoryId, PaginationFilter filter)
    {
        var dataManager = DataManager.Instance;
        return Task.FromResult(dataManager.CatalogItems
                                          .Where(c => c.CategoryId == categoryId)
                                          .Skip((filter.PageNumber - 1) * filter.PageSize)
                                          .Take(filter.PageSize).ToList());
    }               

    public Task<List<Category>> GetCategoryListAsync()
    {
        var dataManager = DataManager.Instance;
        return Task.FromResult(dataManager.Categories);
    }
}
