using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.notification;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace cn.jpush.api.test.notification
{
    [TestClass]
    public class WinphoneNotificationTest
    {
        JsonSerializerSettings jSetting = new JsonSerializerSettings();
        public WinphoneNotificationTest()
        {
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
        }
        [TestMethod]
        public void testEmpty()
        {
            WinphoneNotification winphone = new WinphoneNotification();
           
            JObject json = new JObject();

            Assert.AreEqual(new JObject().ToString(Formatting.None), JsonConvert.SerializeObject(winphone, jSetting));
        }
        [TestMethod]
        public void testQuickAlert()
        {
            WinphoneNotification winphone = new WinphoneNotification().setAlert("aaa");
            JObject json = new JObject();
            json.Add("alert", JToken.FromObject("aaa"));
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(winphone, jSetting));
        }
        [TestMethod]
        public void testTitle()
        {
            WinphoneNotification winphone = new WinphoneNotification().setAlert("aaa").setTitle("title");
            JObject json = new JObject();
            json.Add("title", JToken.FromObject("title"));
            json.Add("alert", JToken.FromObject("aaa"));
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(winphone, jSetting));
        }
        [TestMethod]
        public void testExtra()
        {
            WinphoneNotification winphone = new WinphoneNotification().AddExtra("key", "value").AddExtra("key2", 222);
            JObject json = new JObject();
            JObject extra = new JObject();
            extra.Add("key", JToken.FromObject("value"));
            extra.Add("key2", JToken.FromObject(222));
            json.Add("extras", extra);
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(winphone, jSetting));
        }
    }
}
