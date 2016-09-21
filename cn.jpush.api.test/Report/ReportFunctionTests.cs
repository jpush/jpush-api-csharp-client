using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.report;
using cn.jpush.api.common;

namespace cn.jpush.api.test.Report
{
    [TestClass]
    public class ReportFunctionTests:BaseTest
    {
        private JPushClient jpushClient;
        public ReportFunctionTests()
        {
            jpushClient = new JPushClient(APP_KEY, MASTER_SECRET);
        }
        
        [TestMethod]
        public void getMessagesTest()
        {
            MessagesResult result = jpushClient.getReportMessages("1613113584");
            Assert.IsTrue(result.isResultOK());
            Assert.IsTrue(result.messages.Count > 0);
         }
        [TestMethod]
         public void getMessagesTest2()
        {
            MessagesResult result = jpushClient.getReportMessages("1613113584,   ,1229760629,  ");
            Assert.IsTrue(result.isResultOK());
            Assert.IsTrue(result.messages.Count > 0);
        }
        [TestMethod]
        [ExpectedException(typeof(APIRequestException))]
        public void getUserTest2() 
        {
            UsersResult result = jpushClient.getReportUsers(TimeUnit.DAY, "2016-05-10", 5);
            Assert.IsTrue(result.isResultOK());
            Assert.IsTrue(result.items.Count > 0);
        }
        [TestMethod]
        [ExpectedException(typeof(APIRequestException))]
        public void getUserTest3()  
        {
          UsersResult result = jpushClient.getReportUsers(TimeUnit.HOUR, "2016-05-10 06", 10);
          Assert.IsTrue(result.isResultOK());
          Assert.IsTrue(result.items.Count > 0);
        }
    }
}
