using Book4H2Ten.EntityFrameWorkCore;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Book4H2Ten.Host.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Book4H2TenDbContext");

            services.AddDbContext<Book4H2TenDbContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            /*services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));*/
            return services;
        }
    }
}
