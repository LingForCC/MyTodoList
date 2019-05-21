using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace APITest
{
    public class TestClientProvider
    {
        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<API.Startup>());
            Client = server.CreateClient();
        }
    }
}
