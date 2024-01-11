namespace DatloTest.Domain.Models
{
    public class ColunaModel : ModelBase
    {
        public ColunaModel() : base() { }
        public Guid IdConjunto { get; set; }
        public string Descricao { get; set; }
        public TipoColuna Tipo { get; set; }
    }
}
