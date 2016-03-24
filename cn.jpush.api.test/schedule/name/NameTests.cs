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
    public class NameTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void setVoidNameTest()
        {
            Name name = new Name();
            name.setName("");
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void getNullNameTest()
        {
            Name name = new Name();
            name.setName(null);
        }


        [TestMethod()]
        public void getValidNameTest()
        {
            Name name = new Name();
            name.setName("test");
            Assert.AreEqual(name.getName(),"test");
        }
    }
}