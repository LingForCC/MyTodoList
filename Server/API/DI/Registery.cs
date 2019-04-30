using Core;
using Microsoft.Extensions.DependencyInjection;

namespace API.DI
{
    public class Registery
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
