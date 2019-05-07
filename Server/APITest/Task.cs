using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace APITest
{
    public class UnitTest1
    {
        [Fact]
        public async void TestAddTaskWithInvalidCharacterAsName()
        {
            using (var client = new TestClientProvider().Client)
            {
                var param = Newtonsoft.Json.JsonConvert.SerializeObject(new { Name = "**" });
                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/task", contentPost);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

    }
}
