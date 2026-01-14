
using DataAccess.UnitOfWork.Abstract;

namespace DataAccess
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddDataAccessConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<VisionDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IUnitOfWork,UnitOfWork.Concrete.EfCore.UnitOfWork>();
            return services;
        }
    }
}
