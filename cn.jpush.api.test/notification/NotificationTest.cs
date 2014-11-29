using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;

namespace cn.jpush.api.test.notification
{
    [TestClass]
    public class NotificationTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testEmpty()
        {
            Notification notification = new Notification().Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testAndroidAlertEmpty()
        {
            Notification notification = new Notification();
            notification.AndroidNotification =new AndroidNotification().setTitle("title");
            notification.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIosAlertEmpty()
        {
            Notification notification = new Notification();
            notification.IosNotification =new IosNotification().autoBadge();
            notification.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testWinphoneAlertEmpty()
        {
            Notification notification = new Notification();
            notification.WinphoneNotification = new WinphoneNotification().setTitle("title");
            notification.Check();
        }
    }
}
