using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBTodoApi.Models;

public class Todo
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool Completa { get; set; }
}
