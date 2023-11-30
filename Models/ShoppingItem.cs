using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ShoppingListApi.Models;

public class ShoppingItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string ShoppingItemName { get; set; } = null!;

    public decimal Price { get; set; }

    public bool Selected { get; set; }

    public string Description { get; set; } = null!;
}
