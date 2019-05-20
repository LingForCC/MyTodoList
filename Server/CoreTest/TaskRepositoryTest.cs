using System;
using System.Linq;
using Core;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoreTest
{
    public class TaskRepositoryTest
    {
        [Fact]
        public async void TestAddTaskAsync()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("db_test")
                .Options;
            TaskDbContext rc = new TaskDbContext(options);

            TaskRepository tr = new TaskRepository(rc);
            Task task = new Task("abc 123");
            await tr.AddTaskAsync(task);

            var addedTask = await rc.Set<Task>()
                .Where(t => t.Name == "abc 123")
                .ToListAsync();

            Assert.True(addedTask.Count() == 1);
        }
    }
}
