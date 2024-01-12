using DatloTest.Domain.Models;
using DatloTest.Infrastructure.Repository;
using DatloTest.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatloTest.Infrastructure.MongoDB.Repositories
{
    public class ConjuntoRepository : IConjuntoRepository
    {
        private string _collectionName = "conjuntos";
        private IMongoDBService _service;
        public ConjuntoRepository(IMongoDBService mongoDBService)
        {
            _service = mongoDBService;
        }

        public void Save(ConjuntoModel conjunto)
        {
            _service.InsertOneAsync(conjunto, _collectionName);
        }

        public void SaveDados(string collectionName, DataTable dataTable)
        {
            _service.SaveDataTableToCollection(dataTable, collectionName);
        }
    }
}
