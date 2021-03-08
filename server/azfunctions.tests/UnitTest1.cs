using System;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace azfunctions.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // ARRANGE

            // ACT
            dynamic data = JObject.Parse("{\"user\":\"joe\"}");

            // ASSERT
            Assert.Equal(data.user.Value, "joe@test.com");
        }
    }
}
