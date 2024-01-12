using DatloTest.Domain.Models;
using DatloTest.Infrastructure.Repository;
using DatloTest.Infrastructure.Services;
using DatloTest.Service.Interfaces;
using System.Data;

namespace DatloTest.Service.Services
{
    public class ConjuntoService(IConjuntoRepository conjuntoRepository) : IConjuntoService
    {
        private readonly IConjuntoRepository _conjuntoRepository = conjuntoRepository;

        public ConjuntoModel AtualizarConjunto(Guid idConjunto, string? nome, DataTable dataTable)
        {
            ConjuntoModel model = _conjuntoRepository.GetById(idConjunto);
            if (model == null)
                return null;

            if (!string.IsNullOrEmpty(model?.CollectionName))
            {
                // Delete collection by name
                _conjuntoRepository.DeleteDados(model.CollectionName);
            }

            // Gera novo guid para nomear a nova collection
            model.CollectionName = Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(nome))
                model.Name = nome;

            // Atualiza o registro Conjunto
            _conjuntoRepository.UpdateOne(model);

            // Salva nova Collection
            _conjuntoRepository.SaveDados(model.CollectionName, dataTable);

            return model;
        }

        public ConjuntoModel CarregarConjunto(string name, DataTable dataTable)
        {
            ConjuntoModel conjunto = new()
            {
                Name = name
            };
            conjunto.CollectionName = conjunto.Id.ToString();

            _conjuntoRepository.InsertOne(conjunto);
            _conjuntoRepository.SaveDados(conjunto.CollectionName, dataTable);

            return conjunto;
        }

        public IList<ConjuntoModel> ListaConjuntos(string? nomeConjunto)
        {
            IQueryable<ConjuntoModel> conjuntos;

            if (!string.IsNullOrEmpty(nomeConjunto))
                conjuntos = _conjuntoRepository.GetAll().Where(o => o.Name.Contains(nomeConjunto, StringComparison.CurrentCultureIgnoreCase));
            else
                conjuntos = _conjuntoRepository.GetAll();

            return conjuntos.ToList();
        }

        public dynamic ConsultarConjunto(Guid idConjunto, DataTable? dataTable)
        {
            var model = _conjuntoRepository.GetById(idConjunto);
            if (model == null)
                return null;

            var filtros = DataTableService.ConvertDataTableToListDictionary(dataTable);

            return _conjuntoRepository.GetDados(model.CollectionName);
        }
    }
}
