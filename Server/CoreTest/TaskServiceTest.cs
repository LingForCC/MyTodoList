using Xunit;
using Core;
using Core.Services;
using System.Linq;
using Core.Services.Exceptions;
using System.Collections.Generic;
using Moq;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreTest
{
    public class TaskServiceTest
    {

        #region Create Task in TaskService

        [Fact]
        public async void TestAddNewTask()
        {
            string toAddTaskName = "abc 123";
            Task task = new Task(toAddTaskName);

            TaskDbContext rc = GetTaskDbContext("TestAddNewTask");
            TaskRepository tr = GetTaskRepository(rc);

            //to remove
            Mock <IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            List<Task> taskRepository = new List<Task>();
            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
            mockRepository.Setup(m => m.Add(It.IsAny<Task>()))
                .Callback((Task t) => 
                { 
                    taskRepository.Add(t); 
                });
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            IRepository<Task> repository = mockRepository.Object;
            TaskService ts = new TaskService(unitOfWork, repository, tr);

            //When
            await ts.CreateTask(toAddTaskName);

            //Then
            var addedTask = await rc.Set<Task>()
                .Where(t => t.Name == "abc 123")
                .ToListAsync();

            Assert.True(addedTask.Count() == 1);
        }


        [Fact]
        public async void TestAddNewTaskWithInvalidName()
        {
            TaskDbContext rc = GetTaskDbContext("TestAddNewTaskWithInvalidName");
            TaskRepository tr = GetTaskRepository(rc);

            string toAddTaskName = "*23";
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
            IRepository<Task> repository = mockRepository.Object;

            TaskService ts = new TaskService(unitOfWork, repository, tr);
            await Assert.ThrowsAsync<TaskServiceCreationException>(async () => await ts.CreateTask(toAddTaskName));
        }

        #endregion

        #region Get Task in TaskService

        [Fact]
        public async void TestGetTasks()
        {
            //Given
            TaskDbContext rc = GetTaskDbContext("TestGetTasks");
            TaskRepository tr = GetTaskRepository(rc);

            Task task1 = new Task("abc 123");
            Task task2 = new Task("abc 456");
            await tr.AddTaskAsync(task1);
            await tr.AddTaskAsync(task2);

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;

            List<Task> taskRepository = new List<Task>();
            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
            mockRepository.Setup(m => m.Add(It.IsAny<Task>()))
                .Callback((Task t) =>
                {
                    taskRepository.Add(t);
                });
            mockRepository.Setup(m => m.FindAll())
                .Returns(() => 
                { 
                    return taskRepository; 
                });

            IRepository<Task> repository = mockRepository.Object;
            repository.Add(task1);
            repository.Add(task2);

            TaskService ts = new TaskService(unitOfWork, repository, tr);

            IEnumerable<Task> tasks = ts.GetTasks();
            Assert.Equal(2, tasks.Count());
            Assert.Contains(tasks, task => task.Name == "abc 123");
            Assert.Contains(tasks, task => task.Name == "abc 456");
        }

        #endregion

        private TaskDbContext GetTaskDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("db_test")
                .Options;
            return new TaskDbContext(options);
        }

        private TaskRepository GetTaskRepository(TaskDbContext rc)
        {
            return new TaskRepository(rc);
        }

     }
}
