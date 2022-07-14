using authAPIpoc.Database;
using authAPIpoc.Models;
using MongoDB.Driver;

namespace authAPIpoc.DbService
{
    public interface IServiceBase
    {
        IDbManager GetDbManager();
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, string collectionName) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDef) where T : Entity;
        T GetEntity<T>(FilterDefinition<T> query) where T : Entity;
        T GetEntity<T>(FilterDefinition<T> query, string collectionName) where T : Entity;
        T GetEntity<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDefinition) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offSet, int pageSize, SortDefinition<T> sortQ) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offSet, int pageSize, SortDefinition<T> sortQ, string collectionName) where T : Entity;

        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, SortDefinition<T> sortQ, int skip, int limit) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ, ProjectionDefinition<T, T> projectDef) where T : Entity;
        IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int skip, int limit) where T : Entity;
        bool InsertEntity<T>(T entity) where T : Entity;
        bool InsertEntity<T>(T entity, string collectionName) where T : Entity;
        bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query, string collectionName) where T : Entity;
        bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query) where T : Entity;
        bool DeleteEntity<T>(FilterDefinition<T> query) where T : Entity;
        bool ReplaceEntity<T>(FilterDefinition<T> query, T entity) where T : Entity;
    }
}
