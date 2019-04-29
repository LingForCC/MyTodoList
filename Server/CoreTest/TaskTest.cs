using System;
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
    }
}
