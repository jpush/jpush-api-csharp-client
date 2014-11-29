using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.notification;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace cn.jpush.api.test.notification
{
    [TestClass]
    public class IosNotificationTest
    {
         JsonSerializerSettings jSetting = new JsonSerializerSettings();

         public IosNotificationTest()
        {
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
        }
        [TestMethod]
        public void testEmpty()
        {
            IosNotification ios = new IosNotification();

            JObject json = new JObject();
            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("+1"));

            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));

        }
        [TestMethod]
        public void testQuickAlert()
        {
            IosNotification ios =new IosNotification().setAlert("aaa");
            JObject json = new JObject();
           
            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("+1"));
            json.Add("alert", JToken.FromObject("aaa"));

            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));

        }
        [TestMethod]
        public void testBadge_0()
        {
            IosNotification ios = new IosNotification().setBadge(0);
            JObject json = new JObject();
            
            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("0"));

            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));

        }
        [TestMethod]
        public void testBadge_auto()
        {
            IosNotification ios = new IosNotification().autoBadge();
            JObject json = new JObject();

            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("+1"));

            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
        }
         [TestMethod]
        public void testBadge_plus_2()
        {
            IosNotification ios = new IosNotification().incrBadge(2);
            JObject json = new JObject();
            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("+2"));
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
           
        }
        [TestMethod]
         public void testBadge_plus_0()
         {
             IosNotification ios = new IosNotification().incrBadge(0);

             JObject json = new JObject();
             json.Add("sound", JToken.FromObject(""));
             json.Add("badge", JToken.FromObject("+0"));
             Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
           
         }
        [TestMethod]
        public void testBadge_minus_2()
        {
            IosNotification ios = new IosNotification().incrBadge(-2);
            JObject json = new JObject();
            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("-2"));
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
           
        }
        [TestMethod]
        public void testSound()
        {
            IosNotification ios = new IosNotification().setSound("sound");
            JObject json = new JObject();
            json.Add("sound", JToken.FromObject("sound"));
            json.Add("badge", JToken.FromObject("+1"));
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
        }
        [TestMethod]
        public void testSoundDisabled()
        {
            IosNotification ios = new IosNotification().setSound("sound").disableSound().setAlert("alert");
            JObject json = new JObject();
            json.Add("badge", JToken.FromObject("+1"));
            json.Add("alert", JToken.FromObject("alert"));
           
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
          
        }
        [TestMethod]
        public void testBadgeDisabled()
        {
            IosNotification ios = new IosNotification().disableBadge().setAlert("alert");

            JObject json = new JObject();
            json.Add("sound", JToken.FromObject(""));
            json.Add("alert", JToken.FromObject("alert"));
          
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
        }
        [TestMethod]
        public void testExtra()
        {
            IosNotification ios = new IosNotification().AddExtra("key", "value").AddExtra("key2", true);


            JObject json = new JObject();
            JObject extra = new JObject();
            extra.Add("key",  JToken.FromObject("value"));
            extra.Add("key2", JToken.FromObject(true));
           
            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("+1"));
            json.Add("extras", extra);
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));
        }
        [TestMethod]
         public void testCategory()
        {
    	    IosNotification ios = new IosNotification().setCategory("java");
    	
            JObject json = new JObject();
           
            json.Add("sound", JToken.FromObject(""));
            json.Add("badge", JToken.FromObject("+1"));
            json.Add("category", JToken.FromObject("java"));

            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(ios, jSetting));

        }


    }
}
