using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.notification;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace cn.jpush.api.test.notification
{
    [TestClass]
    public class AndroidNotificationTest
    {
        JsonSerializerSettings jSetting = new JsonSerializerSettings();
        public AndroidNotificationTest()
        {
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            jSetting.NullValueHandling = NullValueHandling.Ignore;
        }
        [TestMethod]
        public void testNoParams()
        {
            AndroidNotification an = new AndroidNotification();
            JObject jobject = new JObject();
           
          
            var jsonString = JsonConvert.SerializeObject(an, jSetting);
            var jsonObject = jobject.ToString(Formatting.None);
            
            Assert.AreEqual(jsonString, jsonObject);
        }
        [TestMethod]
        public void testQuickAlert()
        {
            AndroidNotification an =new AndroidNotification().setAlert("alert");

            JObject json = new JObject();
            json.Add("alert",JToken.FromObject("alert"));
            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(an, jSetting));
        }
         [TestMethod]
        public void testTitle()
         {
             AndroidNotification an = new AndroidNotification().setTitle("title");
             JObject json = new JObject();
             json.Add("title", JToken.FromObject("title"));
             Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(an, jSetting));
         }
        [TestMethod]
        public void testExtra()
         {
             AndroidNotification an = new AndroidNotification().AddExtra("key1", "value1").AddExtra("key2", 222);

             JObject json = new JObject();
             JObject extra = new JObject();
             extra.Add("key1", JToken.FromObject("value1"));
             extra.Add("key2", JToken.FromObject(222));
             json.Add("extras", extra);

             Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(an, jSetting));
         }
         [TestMethod]
        public void testExtra_nullvalue()
        {
            String value2 = "value2";
            value2 = null;
            AndroidNotification an = new AndroidNotification().AddExtra("key2", value2).AddExtra("key1", "value1");

            JObject json = new JObject();
            JObject extra = new JObject();
            extra.Add("key1", JToken.FromObject("value1"));
            json.Add("extras", extra);

            Assert.AreEqual(json.ToString(Formatting.None), JsonConvert.SerializeObject(an, jSetting));
        }
    }
}
