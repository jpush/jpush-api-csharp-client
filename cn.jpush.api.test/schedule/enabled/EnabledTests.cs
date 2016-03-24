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
    public class EnabledTests
    {

        [TestMethod()]
        public void setNullEnableTest()
        {
            Enabled enabled = new Enabled();
            enabled.setEnable(true);
            Assert.AreEqual(true,enabled.getEnable());
        }

        [TestMethod()]
        public void setRightEnableTest(bool enable)
        {
            Enabled enabled = new Enabled();
            enabled.setEnable(false);
            Assert.AreEqual(false, enabled.getEnable());
        }

    }
}