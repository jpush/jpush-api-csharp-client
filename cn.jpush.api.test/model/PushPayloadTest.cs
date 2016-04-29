using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using cn.jpush.api.common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using cn.jpush.api.push.notification;

namespace cn.jpush.api.test.model
{
    [TestClass]
    public class PushPayloadTest:BaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegal_OnlyAudience()
        {
            PushPayload pushPayliad = new PushPayload();
            pushPayliad.audience = Audience.all();
            pushPayliad.Check();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegal_OnlyPlatform()
        {
            Platform platform = Platform.all();
            PushPayload pushPayliad = new PushPayload();
            pushPayliad.platform = platform;
            pushPayliad.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegal_PlatformAudience()
        {
            Platform platform = Platform.all();
            Audience audience = Audience.all();
           
            PushPayload pushPayliad = new PushPayload();
            pushPayliad.platform = platform;
            pushPayliad.audience = audience;

            pushPayliad.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegal_NoAudience()
        {
            Platform platform = Platform.all();
            Notification notifcation = new Notification().setAlert("alert");

            PushPayload pushPayliad = new PushPayload();

            pushPayliad.platform = platform;
            pushPayliad.notification = notifcation;

            pushPayliad.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegal_NoPlatform()
        {

            Notification notifcation = new Notification().setAlert("alert");

            PushPayload pushPayliad = new PushPayload();
            pushPayliad.audience = Audience.all(); ;
            pushPayliad.notification = notifcation;

            pushPayliad.Check();
        }
        [TestMethod]
        public void testNotification()
        {
            int number = ServiceHelper.generateSendno();
           
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all(); 
            payload.options = new Options() { sendno = number };
            payload.notification = new Notification().setAlert("alert");
            payload.Check();

            JObject json = new JObject();
            json.Add("platform", JToken.FromObject("all"));
            json.Add("audience", JToken.FromObject("all"));
           

            JObject noti = new JObject();
            noti.Add("alert", JToken.FromObject("alert"));
            json.Add("notification", noti);

            JObject options = new JObject();
            options.Add("sendno", JToken.FromObject(number));
            options.Add("apns_production", JToken.FromObject(false));
            json.Add("options", options);

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(payload, jSetting);
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);
        }
        [TestMethod]
        public void testMessage()
        {
            int number = ServiceHelper.generateSendno();
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.options = new Options() { sendno = number };
            payload.message = Message.content("message");
            payload.Check();

            JObject json = new JObject();
            json.Add("platform", JToken.FromObject("all"));
            json.Add("audience", JToken.FromObject("all"));
           

            JObject msg = new JObject();
            msg.Add("msg_content", JToken.FromObject("message"));
            json.Add("message", msg);

            JObject options = new JObject();
            options.Add("sendno", JToken.FromObject(number));
            options.Add("apns_production", JToken.FromObject(false));
            json.Add("options", options);

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(payload, jSetting) ;
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);
        }
        [TestMethod]
        public void testGlobalExceed() 
         {
             PushPayload payload = new PushPayload();
             payload.platform = Platform.all();
             payload.audience = Audience.all();
             payload.message = Message.content(LONG_TEXT_2);
             payload.Check();

             Debug.WriteLine("Size: " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_2).Length);
             Assert.IsTrue(payload.IsGlobalExceedLength(), "Should exceed - " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_2).Length);          
        }
        [TestMethod]
        public void testIosExceed() 
        {
             PushPayload payload = new PushPayload();
             payload.platform = Platform.all();
             payload.audience = Audience.all();
             payload.notification = new Notification().setAlert(LONG_TEXT_1);

             payload.Check();
             Debug.WriteLine("Size: " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_1).Length);
             Assert.IsTrue(payload.IsIosExceedLength(), "Should exceed - " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_1).Length); 
       }
        [TestMethod]
        public void testIosExceed2() 
         {
             PushPayload payload = new PushPayload();
             payload.platform = Platform.all();
             payload.audience = Audience.all();
             payload.notification = new Notification();
             payload.notification.IosNotification = new IosNotification().setAlert(LONG_TEXT_1);
             payload.Check();
             
             Debug.WriteLine("Size: " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_1).Length);
             Assert.IsTrue(payload.IsIosExceedLength(), "Should exceed - " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_1).Length); 
       }

        [TestMethod]
        public void testAndroidExceed2()
        {
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.notification = new Notification();
            payload.notification.AndroidNotification = new AndroidNotification().setAlert(LONG_TEXT_3);
            payload.Check();
            Debug.WriteLine("Size: " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_3).Length);
            Assert.IsTrue(payload.IsAndroidExceedLength(), "Should exceed - " + UTF8Encoding.UTF8.GetBytes(LONG_TEXT_3).Length);
        }

    }
}
