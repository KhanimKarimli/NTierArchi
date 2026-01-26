

namespace DataAccess.EfCore
{
    public class VisionDbContext : IdentityDbContext<AppUser>
    {
        public VisionDbContext(DbContextOptions<VisionDbContext> options) : base(options)
        {
        }
		public DbSet<Product> Products { get; set; }
	}
}
