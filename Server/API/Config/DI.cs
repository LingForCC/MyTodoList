using Core;
using Microsoft.Extensions.DependencyInjection;

namespace API.Config
{
    public class DI
    {
        public static void Register(IServiceCollection services)
        {
            Config.AutoMapping.Setup(services);

            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
