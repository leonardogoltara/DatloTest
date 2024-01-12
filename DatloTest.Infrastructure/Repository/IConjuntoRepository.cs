using DatloTest.Domain.Models;
using System.Data;

namespace DatloTest.Infrastructure.Repository
{
    public interface IConjuntoRepository
    {
        public void Save(ConjuntoModel conjunto);
        public void SaveDados(string collectionName, DataTable dataTable);
        public IList<ConjuntoModel> GetAll();
    }
}
