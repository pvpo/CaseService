using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaseService.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
           int res = 5+3;
           Assert.AreEqual(res, 8);
        }
    }
}
