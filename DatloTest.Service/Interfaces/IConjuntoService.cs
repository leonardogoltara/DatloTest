using DatloTest.Domain.Models;
using System.Data;

namespace DatloTest.Service.Interfaces
{
    public interface IConjuntoService
    {
        ConjuntoModel AtualizarConjunto(Guid idConjunto, string? nome, DataTable dataTable);
        ConjuntoModel CarregarConjunto(string name, DataTable dataTable);
        dynamic ConsultarConjunto(Guid idConjunto, DataTable? dataTable);
        IList<ConjuntoModel> ListaConjuntos(string? nomeConjunto);
    }
}