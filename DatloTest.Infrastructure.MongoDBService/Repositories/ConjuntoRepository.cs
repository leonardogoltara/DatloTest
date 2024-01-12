using DatloTest.Domain.Models;
using DatloTest.Infrastructure.Repository;
using DatloTest.Infrastructure.Services;
using MongoDB.Bson;
using System.Data;

namespace DatloTest.Infrastructure.MongoDBService.Repositories
{
    public class ConjuntoRepository(IMongoDBService mongoDBService) : IConjuntoRepository
    {
        private readonly string _collectionName = "conjuntos";
        private readonly IMongoDBService _service = mongoDBService;

        public IQueryable<ConjuntoModel> GetAll()
        {
            return _service.GetAll<ConjuntoModel>(_collectionName);
        }

        public void InsertOne(ConjuntoModel conjunto)
        {
            _service.InsertOne(conjunto, _collectionName);
        }

        public void UpdateOne(ConjuntoModel conjunto)
        {
            _service.UpdateOne(conjunto, conjunto.Id, _collectionName);
        }

        public void SaveDados(string? collectionName, DataTable dataTable)
        {
            _service.SaveDataTableToCollection(dataTable, collectionName);
        }

        public void DeleteDados(string? collectionName)
        {
            _service.DeleteCollection(collectionName);
        }

        public ConjuntoModel GetById(Guid id)
        {
            return _service?.GetAll<ConjuntoModel>(_collectionName)?
                .FirstOrDefault(o => o.Id == id);
        }

        public dynamic GetDados(string? collectionName)
        {
            return _service.GetAll<BsonDocument>(_collectionName);
        }
    }
}
