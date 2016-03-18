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
        public void setVoidTimeTest()
        {
            Single single = new Single();
            single.setTime("");
            
        }

        [TestMethod()]
        public void setNullTimeTest()
        {
            Single single = new Single();
            single.setTime(null);
            
        }

        [TestMethod()]
        public void getTimeTest()
        {
            Single single = new Single();

        }
    }
}