using Microsoft.Extensions.DependencyInjection;
using SO.Data.Repositories;

namespace SO.Data
{
    public static class DataServices
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<SODbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork<SODbContext>>();

            return services;
        }
    }
}
