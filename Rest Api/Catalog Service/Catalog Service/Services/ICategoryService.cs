namespace Catalog_Service.Services;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int categoryId);
}
