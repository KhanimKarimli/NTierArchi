namespace Entities.Concrete
{
    public class Product:BaseEntity,IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
