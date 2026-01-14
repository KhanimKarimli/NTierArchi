


namespace DataAccess.Repositories.Concrete
{
    public class EfProductRepository : EfBaseRepository<Product, VisionDbContext>, IProductRepository
    {
        public EfProductRepository(VisionDbContext context) : base(context)
        {
        }
    }
}
