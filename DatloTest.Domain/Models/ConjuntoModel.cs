namespace DatloTest.Domain.Models
{
    public class ConjuntoModel : ModelBase
    {
        public ConjuntoModel() : base() { }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public virtual IList<ColunaModel> Colunas { get; set; }
        public virtual IList<DadoModel> Dados { get; set; }
    }
}
