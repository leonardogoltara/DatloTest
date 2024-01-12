namespace DatloTest.Domain.Models
{
    public class ConjuntoModel : ModelBase
    {
        public ConjuntoModel() : base()
        {
            Name = string.Empty;
            CollectionName = string.Empty;
        }

        public string Name { get; set; }
        public string CollectionName { get; set; }
    }
}
