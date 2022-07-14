using authAPIpoc.Database;
using authAPIpoc.Models;
using MongoDB.Driver;

namespace authAPIpoc.DbService
{
    public class ServiceBase : IServiceBase
    {
        private readonly IDbManager dbManager;


        public ServiceBase(IDbManager dbM)
        {
            dbManager = dbM;
        }

        public IDbManager GetDbManager()
        {
            return dbManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, string collectionName) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query, collectionName);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDef) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query, projectionDef);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offSet, int pageSize, SortDefinition<T> sortQ) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query, offSet, pageSize, sortQ);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offSet, int pageSize, SortDefinition<T> sortQ, string collectionName) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query, offSet, pageSize, sortQ, collectionName);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ, ProjectionDefinition<T, T> projectDef) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query, offset, pageSize, sortQ, projectDef);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int skip, int limit) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query, skip, limit);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, SortDefinition<T> sortQ, int skip, int limit) where T : Entity
        {
            IEnumerable<T> rtnObj = null;
            try
            {
                rtnObj = dbManager.GetEntityList(query, sortQ, skip, limit);
            }
            catch
            { throw; }
            return rtnObj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public T GetEntity<T>(FilterDefinition<T> query) where T : Entity
        {
            T rtnObj = default(T);
            try
            {
                rtnObj = dbManager.GetEntity(query);
            }
            catch
            { throw; }
            return rtnObj;
        }

        public T GetEntity<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDefinition) where T : Entity
        {
            T rtnObj = default(T);
            try
            {
                rtnObj = dbManager.GetEntity(query, projectionDefinition);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public T GetEntity<T>(FilterDefinition<T> query, string collectionName) where T : Entity
        {
            T rtnObj = default(T);
            try
            {
                rtnObj = dbManager.GetEntity(query, collectionName);
            }
            catch
            { throw; }
            return rtnObj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertEntity<T>(T entity) where T : Entity
        {
            bool rtnObj = false;
            try
            {
                rtnObj = dbManager.InsertEntity(entity);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public bool InsertEntity<T>(T entity, string collectionName) where T : Entity
        {
            bool rtnObj = false;
            try
            {
                rtnObj = dbManager.InsertEntity(entity, collectionName);
            }
            catch
            { throw; }
            return rtnObj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateInfo"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query) where T : Entity
        {
            bool rtnObj = false;
            try
            {
                rtnObj = dbManager.UpdateEntity(updateInfo, query);
            }
            catch
            { throw; }
            return rtnObj;
        }
        public bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query, string collectionName) where T : Entity
        {
            bool rtnObj = false;
            try
            {
                rtnObj = dbManager.UpdateEntity(updateInfo, query, collectionName);
            }
            catch
            { throw; }
            return rtnObj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool DeleteEntity<T>(FilterDefinition<T> query) where T : Entity
        {
            bool rtnObj = false;
            //UpdateDefinition<Entity> updateEntity = null;
            //FilterDefinition<Entity> filter = null;
            try
            {
                //filter =(FilterDefinition<Entity>) query;
                //updateEntity = Builders<Entity>.Update.Set(enty => enty.isPurged, true); //Soft delete  
                //rtnObj = dbManager.UpdateEntity<Entity>(updateEntity,filter);
                //query = Builders<Entity>.Filter.Where(ud => ud.Identity == userId); //Delete
                //query = Builders<Entity>.Filter.Where(ls => ls.Identity ==);
                rtnObj = dbManager.DeleteEntity(query);

            }
            catch
            { throw; }
            return rtnObj;
        }
        public bool DeleteEntity<T>(FilterDefinition<T> query, string collectionName) where T : Entity
        {
            bool rtnObj = false;
            try
            {
                rtnObj = dbManager.DeleteEntity(query, collectionName);

            }
            catch
            { throw; }
            return rtnObj;
        }
        public bool ReplaceEntity<T>(FilterDefinition<T> query, T entity) where T : Entity
        {
            bool rtnObj = false;
            try
            {
                rtnObj = dbManager.ReplaceEntity(entity, query);

            }
            catch
            { throw; }
            return rtnObj;
        }

        public bool ReplaceEntity<T>(T data, FilterDefinition<T> query, ReplaceOptions opt, string collectionName) where T : Entity
        {
            bool rtnVal = false;

            try
            {
                rtnVal = dbManager.ReplaceEntity(data, query, opt, collectionName);

            }
            catch
            { throw; }
            return rtnVal;
        }
    }
}
