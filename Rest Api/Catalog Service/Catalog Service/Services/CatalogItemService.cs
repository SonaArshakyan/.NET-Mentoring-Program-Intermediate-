using Catalog_Service.DataAccessLayer;

namespace Catalog_Service.Services;

public class CatalogItemService : ICatalogItemService
{
    public Task<CatalogItem> CreateItem(CatalogItem item)
    {
        var dataManager = DataManager.Instance;
        if (item.Id == 0)
        {
            var id = dataManager.CatalogItems.Last().Id;
            item.Id = ++id;
        } 
        dataManager.CatalogItems.Add(item);
        return Task.FromResult(dataManager.CatalogItems.Last());
    }

    public Task DeleteItemAsync(int id)
    {
        var dataManager = DataManager.Instance;
        var item = dataManager.CatalogItems.FirstOrDefault(c => c.Id == id);
        dataManager.CatalogItems.Remove(item);
        return Task.CompletedTask;
    }

    public Task<CatalogItem> GetItemAsync(int id)
    {
        var dataManager = DataManager.Instance;
        var item = dataManager.CatalogItems.First(c => c.Id == id);
        return Task.FromResult(item);
    }

    public Task UpdateItemAsync(CatalogItem item)
    {
        var dataManager = DataManager.Instance;
        var listItem = dataManager.CatalogItems.First(c => c.Id == item.Id);
        listItem.IsAvailabe = item.IsAvailabe;
        listItem.Name = item.Name;
        listItem.Price = item.Price;
        listItem.Description = item.Description;
        listItem.CatalogId = item.CatalogId;
        listItem.CatalogId = item.CatalogId;
        return Task.CompletedTask;
    }
}
