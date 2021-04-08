using System;
using Logic;
using PizzaBox.Repository;
using Repository.Contexts;
using Xunit;

namespace Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            EventFunctionsContext context = new EventFunctionsContext();
            TestRepository testRepo = new TestRepository(context);
            TestLogic test = new TestLogic(testRepo);
            string testString = test.test();

            Assert.Equal("test", testString);
        }
    }
}
