using Core;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Config
{
    public class DI
    {
        public static void Register(IServiceCollection services)
        {
            Config.AutoMapping.Setup(services);

            // TODO:
            // change to real one in the future
            services.AddScoped<IUnitOfWork, InMemoryUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(InMemoryRepository<>));

            services.AddScoped<ITaskService, TaskService>();
        }
    }
}
