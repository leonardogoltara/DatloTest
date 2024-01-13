using DatloTest.Infrastructure.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;

namespace DatloTest.Infrastructure.MongoDBService.Services
{
    public class MongoDBService : IMongoDBService
    {
        private readonly string _connString = "mongodb://localhost:27017";
        private readonly string _databaseName = "datlotestdb";

        public bool InsertOne<T>(T entity, string? collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(collectionName);

            collection.InsertOne(entity);

            return true;
        }

        public bool UpdateOne<T>(T entity, Guid key, string? collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(collectionName);

            var filter = Builders<T>.Filter.Eq("Id", key);

            var result = collection.ReplaceOne(filter, entity, new UpdateOptions() { IsUpsert = true });

            return result.ModifiedCount > 0; ;
        }

        public async Task SaveDataTableToCollectionOld(DataTable dt, string collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            List<BsonDocument> batch = [];
            foreach (DataRow dr in dt.Rows)
            {
                var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                batch.Add(new BsonDocument(dictionary));
            }

            await collection.InsertManyAsync(batch.AsEnumerable());
        }

        public async Task SaveDataTableToCollection(DataTable dt, string? collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            try
            {
                List<BsonDocument> batch = [];
                var dicList = DataTableService.ConvertDataTableToListDictionary(dt);
                if (dicList.Count != 0)
                {
                    dicList.ForEach(dic => batch.Add(new BsonDocument(dic)));
                }

                await collection.InsertManyAsync(batch.AsEnumerable());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetById<T>(string? collectionName, Guid id)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(collectionName);

            var filter = Builders<T>.Filter.Eq("Id", id);

            var result = collection.Find(filter).ToList();

            return result.FirstOrDefault();
        }

        public IQueryable<T> GetAll<T>(string? collectionName, Dictionary<string, List<string>> filters)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(collectionName);

            if (filters != null && filters.Any())
            {

                //var filter = Builders<T>.Filter.Eq("Parameters.Value", 11111);
                var filter = Builders<T>.Filter.AnyIn(filters.First().Key, filters.First().Value.ToArray());
                //string firstKey = filters.First().Key;
                //filters.Remove(firstKey);

                //foreach (var filterCol in filters)
                //{
                //    filter = filter.AnyIn(filterCol.Key, filterCol.Value.ToArray());
                //}

                var result = collection.Find(filter).ToList();
                return result.AsQueryable();
            }

            return collection.AsQueryable();
        }

        public bool DeleteCollection(string? collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);

            database.DropCollection(collectionName);

            return true;
        }
    }
}
