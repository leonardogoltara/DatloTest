using DatloTest.Domain.Models;
using System.Data;

namespace DatloTest.Infrastructure.Repository
{
    public interface IConjuntoRepository
    {
        public void UpdateOne(ConjuntoModel conjunto);
        public void InsertOne(ConjuntoModel conjunto);
        public void SaveDados(string? collectionName, DataTable dataTable);
        public void DeleteDados(string? collectionName);
        public dynamic GetDados(string? collectionName);
        public IQueryable<ConjuntoModel> GetAll();
        public ConjuntoModel GetById(Guid id);
    }
}
