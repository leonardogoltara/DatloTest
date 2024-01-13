using DatloTest.Domain.Models;
using DatloTest.Infrastructure.Repository;
using DatloTest.Infrastructure.Services;
using MongoDB.Bson;
using MongoDB.Driver.Core.Operations;
using System.Data;

namespace DatloTest.Infrastructure.MongoDBService.Repositories
{
    public class ConjuntoRepository(IMongoDBService mongoDBService) : IConjuntoRepository
    {
        private readonly string _collectionName = "conjuntos";
        private readonly IMongoDBService _service = mongoDBService;

        public IQueryable<ConjuntoModel> GetAll()
        {
            return _service.GetAll<ConjuntoModel>(_collectionName, null);
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
            return _service.GetById<ConjuntoModel>(_collectionName, id);
        }

        public IEnumerable<dynamic> GetDados(string? collectionName, Dictionary<string, List<string>> filter)
        {
            var query = _service.GetAll<BsonDocument>(collectionName, filter)
                .ToList()
                .Select(o => o.ToDynamic(true));

            return query;
        }
    }
}
