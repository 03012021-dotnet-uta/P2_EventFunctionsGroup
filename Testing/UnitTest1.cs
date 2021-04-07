using System;
using Logic;
using Xunit;

namespace Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 test = new Class1();
            string testString = test.test();

            Assert.Equal("test", testString);
        }
    }
}
