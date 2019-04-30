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
                Id = 1,
                Name = "test task",
            };

            // TODO:
            // Use moq
            ITaskRepository repo = new TaskRepository();

            repo.Add(task);

            Assert.Equal(repo.FindById(task.Id).Name, task.Name);
        }

    }
}
