using API.ErrorCode;
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

            services.AddSingleton<IErrorCodeGeneratorManager>(GetErrorCodeGeneratorManager());

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
        }

        private static IErrorCodeGeneratorManager GetErrorCodeGeneratorManager()
        {
            var errorCodeGeneratorManager = new ErrorCodeGeneratorManager();

            errorCodeGeneratorManager.RegisterErrorCodeGenerator(new TaskServiceCreationErrorCodeGenerator());

            return errorCodeGeneratorManager;
        }
    }
}
