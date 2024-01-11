namespace DatloTest.Domain.Models
{
    public abstract class ModelBase
    {
        public ModelBase()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
