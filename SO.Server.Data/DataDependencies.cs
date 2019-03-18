using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SO.Server.Data
{
    public static class DataDependencies
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<SODbContext>(options => options.UseSqlite(SODbContext.DbName));

            return services;
        }

    }
}
