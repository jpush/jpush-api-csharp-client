using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace cn.jpush.api.schedule.Tests
{
    [TestClass()]
    public class SingleTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void setVoidTimeTest()
        {
            Single single = new Single();
            single.setTime("");
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void setNullTimeTest()
        {
            Single single = new Single();
            single.setTime(null);
            
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void setWrongTimeTest()
        {
            Single single = new Single();
            single.setTime("2016-0514:05:00");
            
        }

        [TestMethod()]
        public void setRightTimeTest()
        {
            Single single = new Single();
            single.setTime("2016-05-25 14:05:00");
            Assert.AreEqual("2016-05-25 14:05:00", single.getTime());
        }
    }
}