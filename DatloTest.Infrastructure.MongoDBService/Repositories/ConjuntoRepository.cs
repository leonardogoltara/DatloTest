using DatloTest.Domain.Models;
using DatloTest.Infrastructure.Repository;
using DatloTest.Infrastructure.Services;
using System.Data;

namespace DatloTest.Infrastructure.MongoDBService.Repositories
{
    public class ConjuntoRepository(IMongoDBService mongoDBService) : IConjuntoRepository
    {
        private readonly string _collectionName = "conjuntos";
        private readonly IMongoDBService _service = mongoDBService;

        public IList<ConjuntoModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(ConjuntoModel conjunto)
        {
            _service.InsertOne(conjunto, _collectionName);
        }

        public void SaveDados(string collectionName, DataTable dataTable)
        {
            _service.SaveDataTableToCollection(dataTable, collectionName);
        }
    }
}
