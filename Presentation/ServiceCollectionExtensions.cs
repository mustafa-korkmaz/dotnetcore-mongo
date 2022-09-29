using Application.Services.Order;
using Application.Services.Product;
using Infrastructure.Configuration;
using Infrastructure.Persistence.MongoDb;

namespace Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfigSections(
           this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbConfig>(
                config.GetSection("MongoDbConfig"));
        }

        public static void AddMappings(this IServiceCollection services)
        {
            //ViewModels to DTOs mappings
            services.AddAutoMapper(typeof(MappingProfile));

            //DTOs to Domain entities mappings
            services.AddAutoMapper(typeof(Application.MappingProfile));

            //Domain entities to mongoDb collections mappings
            MongoDbPersistence.Configure();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}


