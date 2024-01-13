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
        public IEnumerable<dynamic> GetDados(string? collectionName, Dictionary<string, List<string>> filter);
        public IQueryable<ConjuntoModel> GetAll();
        public ConjuntoModel GetById(Guid id);
    }
}
