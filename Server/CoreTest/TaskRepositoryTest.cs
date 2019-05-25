using System;
using System.Linq;
using Core;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CoreTest
{
    public class TaskRepositoryTest
    {
        [Fact]
        public async void TestAddTaskAsync()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("TestAddTaskAsync")
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

        [Fact]
        public async void TestAddTaskAsyncThrowException()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("TestAddTaskAsyncThrowException")
                .Options;
            FakeTaskDbContext1 rc = new FakeTaskDbContext1(options);

            TaskRepository tr = new TaskRepository(rc);
            Task task = new Task("abc 123");
            await Assert.ThrowsAsync<RepositoryException>(
                async () => await tr.AddTaskAsync(task)
            );
        }

        [Fact]
        public async void TestFindTaskById() {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("TestFindTaskById")
                .Options;
            TaskDbContext rc = new TaskDbContext(options);

            TaskRepository tr = new TaskRepository(rc);
            Task task = new Task("abc 123");
            string id = task.Id;
            await tr.AddTaskAsync(task);
            Task foundTask = await tr.FindByIdAsync(id);
            Assert.True(foundTask != null && foundTask.Name == task.Name && foundTask.Id == task.Id);
        }

        [Fact]
        public async void TestFindTaskByIdReturnNull() {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("TestFindTaskByIdReturnNull")
                .Options;
            TaskDbContext rc = new TaskDbContext(options);

            TaskRepository tr = new TaskRepository(rc);
            string id = Guid.NewGuid().ToString();
            Task foundTask = await tr.FindByIdAsync(id);
            Assert.True(foundTask == null);
        }

        [Fact]
        public async void TestFindAll() {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            TaskDbContext rc = new TaskDbContext(options);

            TaskRepository tr = new TaskRepository(rc);
            Task task = new Task("abc 123");
            string id1 = task.Id;
            await tr.AddTaskAsync(task);

            task =new Task("abc 456");
            string id2 = task.Id;
            await tr.AddTaskAsync(task);

            var foundTasks = await tr.FindAllAsync();
            Assert.NotNull(foundTasks.SingleOrDefault(t => t.Id == id1));
            Assert.NotNull(foundTasks.SingleOrDefault(t => t.Id == id2));
            Assert.True(foundTasks.Count() == 2);
        }

    }

    public class FakeTaskDbContext1 : TaskDbContext
    {
        public FakeTaskDbContext1(DbContextOptions<TaskDbContext> options) 
            : base(options)
        {
        }

        public override System.Threading.Tasks.Task<int>
            SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            throw new Exception("Fake Error");
        }
    }
}
