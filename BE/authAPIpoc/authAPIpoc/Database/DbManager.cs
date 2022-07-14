using authAPIpoc.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace authAPIpoc.Database
{
    public class DbManager : IDbManager
    {

        private readonly IMongoClient ___client;
        private readonly IMongoDatabase ___db;
        public DbManager(string ConnectionString, string DbName)
        {
            try
            {
                ___client = new MongoClient(ConnectionString);
                ___db = ___client.GetDatabase(DbName);
            }
            catch
            { throw; }
        }


        public bool IsLive(int secondToWait = 1)
        {
            //bool isMongoLive = ___db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            // return isMongoLive;

            //var isAlive = ProbeForMongoDbConnection();
            //Console.WriteLine("Connection to mongodb://localhost:27017 was " + (isAlive ? "successful!" : "NOT successful!"));

            if (secondToWait <= 0)
                throw new ArgumentOutOfRangeException("secondToWait", secondToWait, "Must be at least 1 second");

            return ___db.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}").Wait(secondToWait * 1000);

        }
        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            return ___db.GetCollection<BsonDocument>(collectionName);
        }

        private bool ProbeForMongoDbConnection()
        {
            var probeTask =
                    Task.Run(() =>
                    {
                        var isAlive = false;
                        //var client = new MongoDB.Driver.MongoClient(connectionString);

                        for (var k = 0; k < 6; k++)
                        {
                            ___client.GetDatabase("admin");
                            var server = ___client.Cluster.Description.Servers;
                            //  isAlive = (server != null &&
                            // server.HeartbeatException == null &&
                            // server.State == MongoDB.Driver.Core.Servers.ServerState.Connected);
                            if (isAlive)
                            {
                                break;
                            }
                            System.Threading.Thread.Sleep(300);
                        }
                        return isAlive;
                    });
            probeTask.Wait();
            return probeTask.Result;
        }

        /// <summary>
        /// This function used to return the collection respective to the Generic object type
        /// passed to the function. The collection can be used later for queries
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IMongoCollection<T></returns>
        public IMongoCollection<T> GetCollection<T>() where T : Entity
        {
            IMongoCollection<T> _col = null;
            try
            {
                _col = ___db.GetCollection<T>(typeof(T).Name);
            }
            catch
            { throw; }

            return _col;
        }
        /// <summary>
        /// This function is used to return single row of the generic object passed to this function
        /// The single row will be returned from the mongo db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>T</returns>
        public T GetEntity<T>(FilterDefinition<T> query) where T : Entity
        {
            T rtnObj = default(T);
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnObj = ___col.Find<T>(query).SingleOrDefault<T>();
            }
            catch
            { throw; }
            return rtnObj;
        }
        /// <summary>
        /// This function is used to return list of rows of the generic object passed to this function
        /// The list of rows will be returned from the mongo db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnList = ___col.Find<T>(query).ToList();
            }
            catch
            { throw; }
            return rtnList;
        }

        /// <summary>
        /// This method is used for projecting required fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="projectionDef"></param>
        /// <returns></returns>
        public T GetEntity<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDef) where T : Entity
        {
            T rtnObj = default(T);
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnObj = ___col.Find<T>(query)
                    .Project(projectionDef)
                    .SingleOrDefault<T>();
            }
            catch
            { throw; }
            return rtnObj;
        }

        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, ProjectionDefinition<T, T> projectionDef) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnList = ___col.Find<T>(query)
                    .Project(projectionDef)
                    .ToList();
            }
            catch
            { throw; }
            return rtnList;
        }
        /// <summary>
        /// This function is used to return single row of the generic object passed to this function
        /// The single row will be returned from the mongo db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>T</returns>
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnList = ___col.Find<T>(query)
                    .Sort(sortQ)
                      .Skip(offset)
                       .Limit(pageSize).ToList();
            }
            catch
            { throw; }
            return rtnList;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, SortDefinition<T> sortQ, int skip, int limit) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnList = ___col.Find<T>(query)
                    .Sort(sortQ)
                      .Skip(skip)
                       .Limit(limit).ToList();
            }
            catch
            { throw; }
            return rtnList;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int skip, int limit) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnList = ___col.Find<T>(query)

                      .Skip(skip)
                       .Limit(limit).ToList();
            }
            catch
            { throw; }
            return rtnList;
        }

        public T GetEntity<T>(FilterDefinition<T> query, string collectionName) where T : Entity
        {
            T rtnObj = default(T);
            try
            {
                var ___col = ___db.GetCollection<T>(collectionName);
                rtnObj = ___col.Find<T>(query).SingleOrDefault<T>();
            }
            catch
            { throw; }
            return rtnObj;
        }
        /// <summary>
        /// This function is used to return list of rows of the generic object passed to this function
        /// The list of rows will be returned from the mongo db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, string collectionName) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                var ___col = ___db.GetCollection<T>(collectionName);
                rtnList = ___col.Find<T>(query).ToList();
            }
            catch
            { throw; }
            return rtnList;
        }
        /// <summary>
        /// This function is used to return list of rows of the generic object passed to this function
        /// The list of rows with only required fields will be returned from the mongo db 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ, ProjectionDefinition<T, T> projectDef) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                rtnList = ___col.Find<T>(query)
                     .Project(projectDef)
                    .Sort(sortQ)
                      .Skip(offset)
                       .Limit(pageSize)
                      .ToList();
            }
            catch
            { throw; }
            return rtnList;
        }
        public IEnumerable<T> GetEntityList<T>(FilterDefinition<T> query, int offset, int pageSize, SortDefinition<T> sortQ, string collectionName) where T : Entity
        {
            IEnumerable<T> rtnList = null;
            try
            {
                // var ___col = ___db.GetCollection<T>(typeof(T).Name);
                var ___col = ___db.GetCollection<T>(collectionName);
                rtnList = ___col.Find<T>(query)
                    .Sort(sortQ)
                      .Skip(offset)
                       .Limit(pageSize).ToList();
                // ___col = ___db.GetCollection<T>(collectionName);

            }
            catch
            { throw; }
            return rtnList;
        }

        /// <summary>
        /// This function is used to insert a row to mongo through the generic object passed to the functions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public bool InsertEntity<T>(T entity) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                ___col.InsertOne(entity);
                rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }
        public bool InsertEntity<T>(List<T> entity) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                ___col.InsertMany(entity);
                rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }
        /// <summary>
        /// This function is used to insert a row to mongo through the generic object passed to the functions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public bool InsertEntity<T>(T entity, string collectionName) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(collectionName);
                ___col.InsertOne(entity);
                rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }

        /// <summary>
        /// This function is used to update mongodb rows based on the generic object passed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateInfo"></param>
        /// <param name="query"></param>
        /// <returns>bool</returns>
        public bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query) where T : Entity
        {

            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                var status = ___col.UpdateOne(query, updateInfo);
                if (status.MatchedCount > 0)
                    rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }
        /// <summary>
        /// This function is used to delete mongodb rows based on the generic object passed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>bool</returns>
        public bool DeleteEntity<T>(FilterDefinition<T> query) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                var result = ___col.DeleteOne(query);
                if (result.DeletedCount > 0)
                    rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }
        public bool DeleteEntity<T>(FilterDefinition<T> query, string collectionName) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(collectionName);
                var result = ___col.DeleteOne(query);
                if (result.DeletedCount > 0)
                    rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }

        public bool ReplaceEntity<T>(T data, FilterDefinition<T> query) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);
                ReplaceOneResult result = ___col.ReplaceOne(query, data);

                if (result.ModifiedCount > 0)
                    rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }
        public bool ReplaceEntity<T>(T data, FilterDefinition<T> query, ReplaceOptions opt, string collectionName) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(collectionName);

                ReplaceOneResult result = ___col.ReplaceOne(query, data, opt);


                if (result.ModifiedCount > 0)
                    rtnVal = true;
                else if (result.UpsertedId != string.Empty)
                    rtnVal = true;

            }
            catch
            { throw; }
            return rtnVal;
        }


        public bool ReplaceEntity<T>(T data, FilterDefinition<T> query, ReplaceOptions opt) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);

                ReplaceOneResult result = ___col.ReplaceOne(query, data, opt);


                if (result.ModifiedCount > 0)
                    rtnVal = true;
                else if (result.UpsertedId != string.Empty)
                    rtnVal = true;

            }
            catch
            { throw; }
            return rtnVal;
        }
        public T GetAndReplaceEntity<T>(T data, FilterDefinition<T> query) where T : Entity
        {
            T rtnVal = null;


            try
            {
                var ___col = ___db.GetCollection<T>(typeof(T).Name);


                rtnVal = ___col.FindOneAndReplace(query, data);

                //if (result.ModifiedCount > 0)
                //    rtnVal = true;
                //else if (result.UpsertedId != string.Empty)



                //if (result.ModifiedCount > 0)
                //    rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }

        public bool UpdateEntity<T>(UpdateDefinition<T> updateInfo, FilterDefinition<T> query, string collectionName) where T : Entity
        {
            bool rtnVal = false;
            try
            {
                var ___col = ___db.GetCollection<T>(collectionName);
                var status = ___col.UpdateOne(query, updateInfo);
                if (status.MatchedCount > 0)
                    rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }


        public T GetAndReplaceEntity<T>(T data, FilterDefinition<T> query, string collectionName) where T : Entity
        {
            T rtnVal = null;

            try
            {
                var ___col = ___db.GetCollection<T>(collectionName);
                rtnVal = ___col.FindOneAndReplace(query, data);

                //if (result.ModifiedCount > 0)
                //    rtnVal = true;
            }
            catch
            { throw; }
            return rtnVal;
        }


    }
}
