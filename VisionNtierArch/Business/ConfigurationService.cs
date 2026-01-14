using Business.Services.Abstract;
using Business.Services.Concrete;
using Business.Utilities.Profiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddScoped<IProductService,ProductService>();
            return services;
        }
    }
}
