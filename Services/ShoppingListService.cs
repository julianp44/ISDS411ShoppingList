using ShoppingListApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ShoppingListApi.Services;

public class ShoppingListService
{
    private readonly IMongoCollection<ShoppingList> _shoppinglistCollection;

    public ShoppingListService(
        IOptions<ShoppingListDatabaseSettings> ShoppingListDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            ShoppingListDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            ShoppingListDatabaseSettings.Value.DatabaseName);

        _shoppinglistCollection = mongoDatabase.GetCollection<ShoppingList>(
            ShoppingListDatabaseSettings.Value.ShoppingListsCollectionName);
    }

    public async Task<List<ShoppingList>> GetAsync() =>
        await _shoppinglistCollection.Find(_ => true).ToListAsync(); // Returns all documents in the collection matching the provided search criteria.

    public async Task<ShoppingList?> GetAsync(string id) =>
        await _shoppinglistCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); // Returns all documents in the collection matching the provided search criteria.

    public async Task CreateAsync(ShoppingList newShoppingList) =>
        await _shoppinglistCollection.InsertOneAsync(newShoppingList); // Inserts the provided object as a new document in the collection.

    public async Task UpdateAsync(string id, ShoppingList updatedShoppingList) =>
        await _shoppinglistCollection.ReplaceOneAsync(x => x.Id == id, updatedShoppingList); // Replaces the single document matching the provided search criteria with the provided object.

    public async Task RemoveAsync(string id) =>
        await _shoppinglistCollection.DeleteOneAsync(x => x.Id == id); //Deletes a single document matching the provided search criteria.
}