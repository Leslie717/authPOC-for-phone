using authAPIpoc.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace authAPIpoc.Database
{
    public interface IDbManager
    {
        IMongoCollection<BsonDocument> GetCollection(string collectionName);
        IMongoCollection<T> GetCollection<T>() where T : Entity;

        T GetEntity<T>(FilterDefinition<T> query) where T : Entity;
        T GetEntity<T>(FilterDefinition<T> query, string collectionName) where T : Entity;
        T GetEntity<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDef) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ, ProjectionDefinition<T, T> projectDef) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, string collectionName) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int skip, int limit) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, SortDefinition<T> sortQ, int skip, int limit) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ, string collectionName) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDef) where T : Entity;

        bool InsertEntity<T>(T entity) where T : Entity;
        bool InsertEntity<T>(List<T> entity) where T : Entity;
        bool InsertEntity<T>(T entity, string collectionName) where T : Entity;

        bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query) where T : Entity;
        bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query, string collectionName) where T : Entity;

        bool DeleteEntity<T>(FilterDefinition<T> query) where T : Entity;
        bool DeleteEntity<T>(FilterDefinition<T> query, string collectionName) where T : Entity;


        bool ReplaceEntity<T>(T data, FilterDefinition<T> query, ReplaceOptions opt) where T : Entity;


        bool ReplaceEntity<T>(T data, FilterDefinition<T> query) where T : Entity;

        bool ReplaceEntity<T>(T data, FilterDefinition<T> query, ReplaceOptions opt, string collectionName) where T : Entity;


        T GetAndReplaceEntity<T>(T data, FilterDefinition<T> query) where T : Entity;
        T GetAndReplaceEntity<T>(T data, FilterDefinition<T> query, string collectionName) where T : Entity;


        bool IsLive(int secondToWait);
    }
}
