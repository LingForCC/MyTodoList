using Xunit;
using Core;
using Core.Services;

namespace CoreTest
{
    public class TaskTest
    {
        [Fact]
        public void TestAddDedpendencyWithNull()
        {
            Task task = new Task();
            Assert.Throws<TaskException>(() => task.AddDependency(null));
        }

        [Fact]
        public void TestAddTaskWithInvalidCharacterName()
        {
            // Arrange
            Task task = new Task()
            {
                Id = "00101001",
                Name = "Buy Books!",
            };

            TaskService taskService = new TaskService(null, null);

            // Assert
            Assert.Throws<TaskException>(() => taskService.ValidateTaskName(task.Name));
        }

        [Fact]
        public void TestAddTaskWithEmptyName()
        {
            // Arrange
            Task task = new Task();

            TaskService taskService = new TaskService(null, null);

            // Assert
            Assert.Throws<TaskException>(() => taskService.ValidateTaskName(task.Name));
        }

    }
}
