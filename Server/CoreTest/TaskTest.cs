using Xunit;
using Core;

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
        public void TestAddTaskSuccesfully()
        {
            Task task = new Task()
            {
                Id = "00101001",
                Name = "test task",
            };

            // TODO:
            // Use moq
            ITaskRepository repo = new TaskRepository();

            repo.Add(task);

            Assert.Equal(repo.FindById(task.Id).Name, task.Name);
        }

        [Fact]
        public void TestAddTaskWithInvalidEmptyName()
        {
            Task task = new Task()
            {
                Id = "00101001",
                Name = " ",
            };

            // TODO:
            // Use moq
            ITaskRepository repo = new TaskRepository();

            Assert.Throws<TaskException>(() => repo.Add(task));
        }

        [Fact]
        public void TestAddTaskWithInvalidCharacterName()
        {
            Task task = new Task()
            {
                Id = "00101001",
                Name = "Buy Books !",
            };

            // TODO:
            // Use moq
            ITaskRepository repo = new TaskRepository();

            Assert.Throws<TaskException>(() => repo.Add(task));
        }
    }
}
