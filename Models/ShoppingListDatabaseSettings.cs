namespace ShoppingListApi.Models;

public class ShoppingListDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ShoppingListCollectionName { get; set; } = null!;
}