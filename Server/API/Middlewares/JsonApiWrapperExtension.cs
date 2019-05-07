using Microsoft.AspNetCore.Builder;

namespace API.Middlewares
{
    public static class JsonApiWrapperExtension
    {
        public static IApplicationBuilder UseJsonApiWrapper(this IApplicationBuilder app, string version, string name)
        {
            return app.UseMiddleware<JsonApiWrapper>(version, name);
        }
    }
}
