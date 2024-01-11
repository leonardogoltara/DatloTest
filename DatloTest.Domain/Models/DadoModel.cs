namespace DatloTest.Domain.Models
{
    public class DadoModel : ModelBase
    {
        public DadoModel() : base() { }
        public Guid IdConjunto { get; set; }
        public Guid IdColuna { get; set; }
        public string Valor { get; set; }
        public virtual ConjuntoModel Conjunto { get; set; }
        public virtual ColunaModel Coluna { get; set; }
    }
}
