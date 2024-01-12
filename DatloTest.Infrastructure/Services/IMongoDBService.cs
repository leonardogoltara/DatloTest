using System.Data;

namespace DatloTest.Infrastructure.Services
{
    public interface IMongoDBService
    {
        bool InsertOne<T>(T entity, string collectionName);
        IQueryable<T> GetAll<T>(string collectionName);
        Task SaveDataTableToCollection(DataTable dt, string collectionName);
    }
}