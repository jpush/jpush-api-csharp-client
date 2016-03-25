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
    public class TriggerPayloadTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void TriggerPayloadSingleTimeNullTest()
        {
            Single single = new Single();
            single = null;
            TriggerPayload singlepayload = new TriggerPayload(single);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadSingleTimeEmptyTest()
        {
            TriggerPayload single = new TriggerPayload("");
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadSingleTimeInvalidTest()
        {
            TriggerPayload single = new TriggerPayload("2016-4-25 14:05:00");
        }

        [TestMethod()]
        public void TriggerPayloadSingleTimeValidTest()
        {
            TriggerPayload single = new TriggerPayload("2016-04-25 14:05:00");
            Assert.IsInstanceOfType(single,typeof(TriggerPayload));
        }

        [TestMethod()]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void TriggerPayloadPeriodicalNullTest()
        {
            Periodical periodical = new Periodical();
            periodical = null;
            TriggerPayload periodicalpayload = new TriggerPayload(periodical);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadPeriodicalEmptyTest()
        {
            String[] point = { };
            TriggerPayload periodicalpayload = new TriggerPayload("", "", "", "",1, point);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadPeriodicalStartInvalidTest()
        {
            String[] point = {"01"};
            TriggerPayload periodicalpayload = new TriggerPayload("2016-09-1712:00:00", "2016-10-17 12:00:00", "12:00:00", "Month", 1, point);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadPeriodicalEndInvalidTest()
        {
            String[] point = { "01" };
            TriggerPayload periodicalpayload = new TriggerPayload("2016-09-17 12:00:00", "2016-10-1712:00:00", "12:00:00", "Month", 1, point);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadPeriodicalTimeInvalidTest()
        {
            String[] point = { "01" };
            TriggerPayload periodicalpayload = new TriggerPayload("2016-09-17 12:00:00", "2016-10-17 12:00:00", "112:00:00", "Month", 1, point);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadPeriodicalTime_unitInvalidTest()
        {
            String[] point = { "01" };
            TriggerPayload periodicalpayload = new TriggerPayload("2016-09-17 12:00:00", "2016-10-17 12:00:00", "12:00:00", "Months", 1, point);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TriggerPayloadPeriodicalFrequencyInvalidTest()
        {
            String[] point = { "01" };
            TriggerPayload periodicalpayload = new TriggerPayload("2016-09-17 12:00:00", "2016-10-17 12:00:00", "12:00:00", "Month", 101, point);
        }


        [TestMethod()]
        
        public void TriggerPayloadPeriodicalValidTest()
        {
            String[] point = { "01" };
            TriggerPayload periodicalpayload = new TriggerPayload("2016-09-17 12:00:00", "2016-10-17 12:00:00", "12:00:00", "Month", 1, point);
            Assert.IsInstanceOfType(periodicalpayload,typeof(TriggerPayload));
        }

        [TestMethod()]
        public void setTimeTest()
        {

        }

        [TestMethod()]
        public void getTimeTest()
        {

        }

        [TestMethod()]
        public void setStartTest()
        {

        }

        [TestMethod()]
        public void getStartTest()
        {

        }

        [TestMethod()]
        public void setEndTest()
        {

        }

        [TestMethod()]
        public void getEndTest()
        {

        }

        [TestMethod()]
        public void setTime_unitTest()
        {

        }

        [TestMethod()]
        public void getTime_unitTest()
        {

        }

        [TestMethod()]
        public void setFrequencyTest()
        {

        }

        [TestMethod()]
        public void getFrequencyTest()
        {

        }

        [TestMethod()]
        public void setPointTest()
        {

        }

        [TestMethod()]
        public void getPointTest()
        {

        }

        [TestMethod()]
        public void ToJsonTest()
        {

        }
    }
}