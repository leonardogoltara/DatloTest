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

        public bool InsertOne<T>(T entity, string collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(collectionName);

            collection.InsertOne(entity);

            return true;
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

        public async Task SaveDataTableToCollection(DataTable dt, string collectionName)
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

        public IQueryable<T> GetAll<T>(string collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(collectionName);

            return collection.AsQueryable();
        }
    }
}
