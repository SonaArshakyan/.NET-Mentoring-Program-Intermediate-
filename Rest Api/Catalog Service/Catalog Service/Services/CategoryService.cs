using Catalog_Service.DataAccessLayer;

namespace Catalog_Service.Services;

public class CategoryService : ICategoryService
{
    public Task<Category> CreateCategoryAsync(Category category)
    {
        var dataManager = DataManager.Instance;
        if (category.Id == 0)
        {
            var id = dataManager.Categories.Last().Id;
            category.Id = ++id;
        }
        dataManager.Categories.Add(category);
        return Task.FromResult(dataManager.Categories.Last());
    }

    public Task DeleteCategoryAsync(int categoryId)
    {
        var dataManager = DataManager.Instance;
        var category = dataManager.Categories.FirstOrDefault(c => c.Id == categoryId);
        dataManager.Categories.Remove(category);
        return Task.CompletedTask;
    }

    public Task UpdateCategoryAsync(Category category)
    {
        var dataManager = DataManager.Instance;
        var listCategory = dataManager.Categories.First(c => c.Id == category.Id);
        listCategory.Type = category.Type;
        return Task.CompletedTask;
    }
}
