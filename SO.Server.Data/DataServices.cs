using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SO.Server.Data
{
    public static class DataServices
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<SODbContext>(options => options.UseSqlite(SODbContext.ConnectionString));
            services.AddTransient<IUnitOfWork, UnitOfWork<SODbContext>>();
            return services;
        }

    }
}
