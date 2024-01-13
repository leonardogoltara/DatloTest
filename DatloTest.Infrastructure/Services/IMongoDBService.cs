using System.Data;

namespace DatloTest.Infrastructure.Services
{
    public interface IMongoDBService
    {
        bool InsertOne<T>(T entity, string? collectionName);
        bool UpdateOne<T>(T entity, Guid key, string? collectionName);
        IQueryable<T> GetAll<T>(string? collectionName, Dictionary<string, List<string>> filters);
        T GetById<T>(string? collectionName, Guid id);
        Task SaveDataTableToCollection(DataTable dt, string? collectionName);
        bool DeleteCollection(string? collectionName);
    }
}