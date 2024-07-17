using Domain.Interface;
using Infrastructure.Job;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddHttpClient<FetchShowsJob>();
            services.AddSingleton<FetchShowsJob>();
            services.AddHostedService(provider => provider.GetRequiredService<FetchShowsJob>());
            return services;
        }
    }
}
