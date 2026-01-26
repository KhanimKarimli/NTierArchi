


namespace DataAccess.UnitOfWork.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VisionDbContext _context;
        private readonly IProductRepository _productRepository;

        public UnitOfWork(VisionDbContext context)
        {
            _context=context;
        }

        public IProductRepository ProductRepository => _productRepository ?? new EfProductRepository(_context);

        public async Task<int> SaveAsync()
        {
          return await _context.SaveChangesAsync();
        }
    }
}
