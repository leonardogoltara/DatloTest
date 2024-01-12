using DatloTest.Domain.Models;
using DatloTest.Infrastructure.Repository;
using DatloTest.Service.Interfaces;
using System.Data;

namespace DatloTest.Service.Services
{
    public class ConjuntoService(IConjuntoRepository conjuntoRepository) : IConjuntoService
    {
        private readonly IConjuntoRepository _conjuntoRepository = conjuntoRepository;

        public ConjuntoModel AtualizarConjunto(Guid idConjunto, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public ConjuntoModel CarregarConjunto(string name, DataTable dataTable)
        {
            ConjuntoModel conjunto = new()
            {
                Name = name
            };
            conjunto.CollectionName = conjunto.Id.ToString();

            _conjuntoRepository.Save(conjunto);
            _conjuntoRepository.SaveDados(conjunto.CollectionName, dataTable);

            return conjunto;
        }

        public dynamic ConsultarConjunto(Guid idConjunto)
        {
            throw new NotImplementedException();
        }

        public IList<ConjuntoModel> ListaConjuntos(string nomeConjunto)
        {
            throw new NotImplementedException();
        }
    }
}
