using System;
using System.Linq;
using System.Threading;
using Core;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using Xunit;

namespace CoreTest
{
    public class TaskRepositoryTest
    {
        [Fact]
        public async void TestAddTaskAsync()
        {
            // Arrange
            var task = new Task("abc 123");

            var dbSetMock = new Mock<DbSet<Task>>();

            var contextMock = new Mock<TaskDbContext>(TestConfigs.GetDbOptions("TestAddTaskAsync"));
            contextMock.Setup(it => it.Set<Task>())
                .Returns(() => dbSetMock.Object);

            var context = contextMock.Object;

            dbSetMock.Setup(m => m.AddAsync(It.IsAny<Task>(), CancellationToken.None))
                .ReturnsAsync(() => context.Entry<Task>(task));

            // Act
            var repository = new EFBaseRepository<Task>(context);
            await repository.AddAsync(task);

            //Assert
            dbSetMock.Verify(x => x.AddAsync(It.Is<Task>(y => y == task), CancellationToken.None));
        }

        [Fact]
        public async void TestFindTaskById() {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("TestFindTaskById")
                .Options;
            TaskDbContext rc = new TaskDbContext(options);

            var tr = new EFBaseRepository<Task>(rc);
            Task task = new Task("abc 123");
            string id = task.Id;
            await tr.AddAsync(task);
            await rc.SaveChangesAsync();
            Task foundTask = await tr.FindByIdAsync(id);
            Assert.True(foundTask != null && foundTask.Name == task.Name && foundTask.Id == task.Id);
        }

        [Fact]
        public async void TestFindTaskByIdReturnNull() {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("TestFindTaskByIdReturnNull")
                .Options;
            TaskDbContext rc = new TaskDbContext(options);

            var tr = new EFBaseRepository<Task>(rc);
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

            var tr = new EFBaseRepository<Task>(rc);
            Task task = new Task("abc 123");
            string id1 = task.Id;
            await tr.AddAsync(task);

            task =new Task("abc 456");
            string id2 = task.Id;
            await tr.AddAsync(task);

            await rc.SaveChangesAsync();

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
