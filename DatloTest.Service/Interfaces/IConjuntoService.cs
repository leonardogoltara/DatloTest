using DatloTest.Domain.Models;
using System.Data;

namespace DatloTest.Service.Interfaces
{
    public interface IConjuntoService
    {
        ConjuntoModel AtualizarConjunto(Guid idConjunto, DataTable dataTable);
        ConjuntoModel CarregarConjunto(string name, DataTable dataTable);
        dynamic ConsultarConjunto(Guid idConjunto);
        IList<ConjuntoModel> ListaConjuntos(string nomeConjunto);
    }
}