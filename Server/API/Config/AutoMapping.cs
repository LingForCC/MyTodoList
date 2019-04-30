using API.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Core;

namespace API.Config
{
    public class AutoMapping
    {
        public static void Setup(IServiceCollection serviceCollection)
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.CreateMap<PostNewTaskRequestModel, Task>().ReverseMap();
            });

            serviceCollection.AddSingleton(Mapper.Instance);
        }
    }
}
