using Application.Common.Interfaces;
using Domain.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
      public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
      {
            // Register your infrastructure services here
            // For example:
            // services.AddScoped<IYourRepository, YourRepository>();
            // services.AddDbContext<YourDbContext>(options =>
            //     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));services.AddDbContext<AppDBContext>(options =>
            services.AddDbContext<AppDBContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            return services;
      }
}
