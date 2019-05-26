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

            TaskDbContext rc = TestConfigs.GetTaskDbContext("TestAddNewTask");

            //to remove
            Mock <IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
            mockUnitOfWork.Setup(m => m.CompleteAsync())
                .ReturnsAsync(() => 1);

            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            IRepository<Task> repository = mockRepository.Object;
            TaskService ts = new TaskService(unitOfWork, repository);

            //When
            await ts.CreateTask(toAddTaskName);

            //Then
            mockRepository.Verify(it => it.AddAsync(It.IsAny<Task>()), Times.Once);
            mockUnitOfWork.Verify(it => it.CompleteAsync(), Times.Once);
        }


        [Fact]
        public async void TestAddNewTaskWithInvalidName()
        {
            TaskDbContext rc = TestConfigs.GetTaskDbContext("TestAddNewTaskWithInvalidName");
            var tr = new Mock<IRepository<Task>>().Object;

            string toAddTaskName = "*23";
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;

            TaskService ts = new TaskService(unitOfWork, tr);
            await Assert.ThrowsAsync<TaskServiceCreationException>(async () => await ts.CreateTask(toAddTaskName));
        }

        #endregion

        #region Get Task in TaskService

        [Fact]
        public async void TestGetTasks()
        {
            //Given
            TaskDbContext rc = TestConfigs.GetTaskDbContext("TestGetTasks");

            Task task1 = new Task("abc 123");
            Task task2 = new Task("abc 456");

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;

            List<Task> taskRepository = new List<Task>();
            taskRepository.Add(task1);
            taskRepository.Add(task2);

            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
   
            mockRepository.Setup(m => m.FindAllAsync())
                .ReturnsAsync(() => 
                { 
                    return taskRepository; 
                });

            IRepository<Task> repository = mockRepository.Object;

            TaskService ts = new TaskService(unitOfWork, repository);

            IEnumerable<Task> tasks = await ts.GetTasksAsync();
            Assert.Equal(2, tasks.Count());
            Assert.Contains(tasks, task => task.Name == "abc 123");
            Assert.Contains(tasks, task => task.Name == "abc 456");
        }

        [Fact]
        public async void TestGetTaskById() {

            string toAddTaskName = "abc 123";
            Task task = new Task(toAddTaskName);

            TaskDbContext rc = TestConfigs.GetTaskDbContext("TestGetTaskById");

            //to remove
            Mock <IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();

            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
            mockRepository.Setup(m => m.FindByIdAsync(task.Id))
                .ReturnsAsync((string id) => task);

            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            IRepository<Task> repository = mockRepository.Object;
            TaskService ts = new TaskService(unitOfWork, repository);

            var foundTask = await ts.GetTaskByIdAsync(task.Id);
            Assert.True(foundTask != null && foundTask.Id == task.Id);
        }

        [Fact]
        public async void TestGetTaskByIdThrowExceptionWhenIdIsNullOrEmpty()
        {
            Mock <IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
 
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            IRepository<Task> repository = mockRepository.Object;
            TaskService ts = new TaskService(unitOfWork, repository);

            await Assert.ThrowsAsync<TaskServiceQueryException>(async () => await ts.GetTaskByIdAsync(null));
            await Assert.ThrowsAsync<TaskServiceQueryException>(async () => await ts.GetTaskByIdAsync(string.Empty));
        }

        #endregion
     }
}
