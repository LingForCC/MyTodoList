using Xunit;
using Core;
using Core.Services;
using System.Linq;
using Core.Services.Exceptions;

namespace CoreTest
{
    public class TaskServiceTest
    {

        #region Create Task in TaskService

        [Fact]
        public void TestAddNewTask()
        {
            string toAddTaskName = "abc 123";
            IUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            TaskService ts = new TaskService(unitOfWork, 
                unitOfWork.GetRepository<Task>());
            Task newTask = ts.CreateTask(toAddTaskName);
            Assert.Equal(newTask.Name, toAddTaskName);

            var taskNum = unitOfWork.GetRepository<Task>()
                .Find(t => t.Name == newTask.Name).Count();
            Assert.Equal(1, taskNum);
        }


        [Fact]
        public void TestAddNewTaskWithInvalidName()
        {
            string toAddTaskName = "*23";
            IUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            TaskService ts = new TaskService(unitOfWork, 
                unitOfWork.GetRepository<Task>());
            Assert.Throws<TaskServiceCreationException>(() => ts.CreateTask(toAddTaskName));
        }

        #endregion

    }
}
