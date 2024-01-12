using DatloTest.Infrastructure.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;

namespace DatloTest.Infrastructure.MongoDB.Services
{
    public class MongoDBService : IMongoDBService
    {
        private string _connString = "mongodb://localhost:27017";
        private string _databaseName = "datlotestdb";

        public bool InsertOneAsync<T>(T entity, string collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(collectionName);

            collection.InsertOne(entity);

            return true;
        }

        public async Task SaveDataTableToCollection(DataTable dt, string collectionName)
        {
            var client = new MongoClient(_connString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            List<BsonDocument> batch = new List<BsonDocument>();
            foreach (DataRow dr in dt.Rows)
            {
                var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                batch.Add(new BsonDocument(dictionary));
            }

            await collection.InsertManyAsync(batch.AsEnumerable());
        }
    }
}
