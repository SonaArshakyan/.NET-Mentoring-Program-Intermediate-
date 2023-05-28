namespace Catalog_Service.DataAccessLayer;

public sealed class DataManager
{
    private static readonly Lazy<DataManager> dataContext =
        new Lazy<DataManager>(() => new DataManager());

    private static readonly List<Category> categories = new List<Category>()
    {
        new Category() {  Id = 1, Type ="Type1" },
        new Category() {  Id = 2, Type ="Type2" },
        new Category() {  Id = 3, Type ="Type3" },
        new Category() {  Id = 4, Type ="Type1" },
    };

    private static readonly List<Catalog> catalogs = new List<Catalog>()
    {
        new Catalog(){ CatalogId = 1 ,Categories = categories,  CreatedDate = DateTime.Now }
    };

    private static readonly List<CatalogItem> catalogItems = new List<CatalogItem>()
    {
      new CatalogItem{ Id =1, CategoryId =1 , CatalogId = 1, Description ="Item 1 desc." , IsAvailabe = true, Name ="Item 1", Price = 20 },
      new CatalogItem{ Id =2, CategoryId =3,  CatalogId = 1, Description ="Item 2 desc." , IsAvailabe = true, Name ="Item 2", Price = 20 },
      new CatalogItem{ Id =3, CategoryId =2 , CatalogId = 1, Description ="Item 3 desc." , IsAvailabe = true, Name ="Item 3", Price = 20 },
      new CatalogItem{ Id =4, CategoryId =3 , CatalogId = 1, Description ="Item 4 desc." , IsAvailabe = true, Name ="Item 4", Price = 20 },
      new CatalogItem{ Id =5, CategoryId =4 , CatalogId = 1, Description ="Item 5 desc." , IsAvailabe = true, Name ="Item 5", Price = 20 },
      new CatalogItem{ Id =6, CategoryId =3 , CatalogId = 1, Description ="Item 6 desc." , IsAvailabe = true, Name ="Item 6", Price = 20 },
      new CatalogItem{ Id =7, CategoryId =1 , CatalogId = 1, Description ="Item 7 desc." , IsAvailabe = true, Name ="Item 7", Price = 20 },

    };

    public static DataManager Instance { get { return dataContext.Value; } }

    private DataManager()
    {
    }

    public List<Catalog> Catalogs { get { return catalogs; } }
    public List<CatalogItem> CatalogItems { get { return catalogItems; } }
    public List<Category> Categories { get { return categories; } }

}