using Xunit;
using Core;
using Core.Exceptions;

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
            Assert.Throws<InvalidNameTaskException>(() => new Task("Buy Books134!"));
        }

        [Fact]
        public void TestNewTaskWithSpaceInName()
        {
            Assert.Throws<InvalidNameTaskException>(() => new Task(" "));
        }

        [Fact]
        public void TestNewTaskWithEmptyStringAsName()
        {
            Assert.Throws<InvalidNameTaskException>(() => new Task(""));
        }

        [Fact]
        public void TestNewTaskWithNullAsName()
        {
            Assert.Throws<InvalidNameTaskException>(() => new Task(null));
        }

        #endregion
    }
}
