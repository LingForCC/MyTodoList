using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace APITest
{
    public class TaskTest
    {
        [Fact]
        public async void TestAddTaskWithValidName()
        {
            using (var client = new TestClientProvider().Client)
            {
                var param = JsonConvert.SerializeObject(new { Name = "abc 123" });
                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/task", contentPost);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var respContentString = await response.Content.ReadAsStringAsync();
                dynamic respContent = JsonConvert.DeserializeObject(respContentString);
                Assert.Equal("abc 123", (string)(respContent.name));
            }
        }

        [Fact]
        public async void TestAddTaskWithInvalidCharacterAsName()
        {
            using (var client = new TestClientProvider().Client)
            {
                var param = Newtonsoft.Json.JsonConvert.SerializeObject(new { Name = "**" });
                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/task", contentPost);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                var respContentString = await response.Content.ReadAsStringAsync();
                dynamic respContent = JsonConvert.DeserializeObject(respContentString);
                Assert.Equal("TSC-101", (string)(respContent.errorCode));
            }
        }

        [Fact]
        public async void TestViewTask()
        {
            using (var client = new TestClientProvider().Client)
            {
                var param = JsonConvert.SerializeObject(new { Name = "abc 123" });
                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
                await client.PostAsync("/api/task", contentPost);

                var response = await client.GetAsync("/api/task");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var respContentString = await response.Content.ReadAsStringAsync();
                dynamic respContent = JsonConvert.DeserializeObject(respContentString);
                Assert.Equal("abc 123", (string)(respContent[0].name));

            }
        }



        [Fact]
        public async void TestDeleteNonExistingTaskReturnsNotFoundStatus()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/task/non-existing-task-id");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

    }
}
