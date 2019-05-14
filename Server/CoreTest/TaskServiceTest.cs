using Xunit;
using Core;
using Core.Services;
using System.Linq;
using Core.Services.Exceptions;
using System.Collections.Generic;
using Moq;
using Core.Repositories;

namespace CoreTest
{
    public class TaskServiceTest
    {

        #region Create Task in TaskService

        [Fact]
        public void TestAddNewTask()
        {
            string toAddTaskName = "abc 123";
            Task task = new Task(toAddTaskName);

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();

            List<Task> taskRepository = new List<Task>();
            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
            mockRepository.Setup(m => m.Add(It.IsAny<Task>()))
                .Callback((Task t) => 
                { 
                    taskRepository.Add(t); 
                });
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            IRepository<Task> repository = mockRepository.Object;
            TaskService ts = new TaskService(unitOfWork, repository);
            ts.CreateTask(toAddTaskName);

            Assert.Single(taskRepository, t => t.Name == toAddTaskName);
        }


        [Fact]
        public void TestAddNewTaskWithInvalidName()
        {
            string toAddTaskName = "*23";
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            Mock<IRepository<Task>> mockRepository = new Mock<IRepository<Task>>();
            IRepository<Task> repository = mockRepository.Object;

            TaskService ts = new TaskService(unitOfWork, repository);
            Assert.Throws<TaskServiceCreationException>(() => ts.CreateTask(toAddTaskName));
        }

        #endregion

        #region Get Task in TaskService

        [Fact]
        public void TestGetTasks()
        {
            //Given
            string toAddTaskName1 = "abc 123";
            string toAddTaskName2 = "abc 456";
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

            TaskService ts = new TaskService(unitOfWork, repository);
            ts.CreateTask(toAddTaskName1);
            ts.CreateTask(toAddTaskName2);

            IEnumerable<Task> tasks = ts.GetTasks();
            Assert.Equal(2, tasks.Count());
            Assert.Contains(tasks, task => task.Name == toAddTaskName1);
            Assert.Contains(tasks, task => task.Name == toAddTaskName2);
        }

        #endregion


    }
}
