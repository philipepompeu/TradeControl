using Microsoft.EntityFrameworkCore;
using TradeControl.Domain.Repository;
using TradeControl.Services;

namespace TradeControl.Configurations
{
    public static class RepositoryDependency
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TradeControlDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")) );

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<ITradeOperationRepository, TradeOperationRepository>();

            services.AddScoped<IAssetsService, AssetsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITradeOperationService, TradeOperationService>();
            services.AddScoped<IFileDocumentRepository, FileDocumentRepository>();

            return services;
        }
    }
}
