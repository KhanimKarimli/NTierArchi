using Business.Services.Abstract;
using Business.Services.Concrete;
using Business.Utilities.Profiles;
using Core.Entities.Concrete.Auth;
using DataAccess.EfCore;
using DataAccess.UnitOfWork.Concrete.EfCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Business
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddScoped<IProductService,ProductService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<VisionDbContext>()
				.AddDefaultTokenProviders();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(opt =>
			{
				var tokenOption = configuration.GetSection("TokenOptions").Get<TokenOption>();
				opt.TokenValidationParameters=new TokenValidationParameters
				{
					ValidateIssuer=true,
					ValidateAudience=true,
					ValidateLifetime=true,
					ValidIssuer=tokenOption.Issuer,
					ValidAudience=tokenOption.Audience,
					ValidateIssuerSigningKey=true,
					IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey)),
					ClockSkew=TimeSpan.Zero
				};
			});
			return services;
        }
    }
}
