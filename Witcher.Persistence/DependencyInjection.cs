using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;

namespace Witcher.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<WitcherDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IWitcherDbContext>(provider => provider.GetService<WitcherDbContext>());
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            return services;
        }
    }
}
