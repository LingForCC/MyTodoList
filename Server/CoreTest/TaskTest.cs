using Xunit;
using Core;

namespace CoreTest
{
    public class TaskTest
    {

        #region New Task with Valid Parameter

        [Fact]
        public void TestNewTaskWithValidName()
        {
            string name = "Buy Books123";
            Task task = new Task(name);
            Assert.Equal(task.Name, name);
        }

        #endregion

        #region New Task with invalid Name

        [Fact]
        public void TestNewTaskWithInvalidCharacterInName()
        {
            Assert.Throws<TaskException>(() => new Task("Buy Books134!"));
        }

        [Fact]
        public void TestNewTaskWithSpaceInName()
        {
            Assert.Throws<TaskException>(() => new Task(" "));
        }

        [Fact]
        public void TestNewTaskWithEmptyStringAsName()
        {
            Assert.Throws<TaskException>(() => new Task(""));
        }

        [Fact]
        public void TestNewTaskWithNullAsName()
        {
            Assert.Throws<TaskException>(() => new Task(null));
        }

        #endregion
    }
}
