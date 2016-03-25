using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using Newtonsoft.Json.Linq;
using cn.jpush.api.push.notification;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class NotificationTest:BaseRemotePushTest
    {
       
        [TestMethod]
        public void sendNotification_alert_json() 
        {
            JObject json = new JObject();
            json.Add("key1", "value1");
            json.Add("key2", true);
        
            String alert = json.ToString();
            Console.WriteLine(alert);
            
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.notification = new Notification()
                                   .setAndroid (new AndroidNotification()
                                                .setAlert(alert)
                                                .setTitle("title"));
           
            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
	
    // --------------- Android
	
      [TestMethod]
        public void sendNotification_android_title() 
      {
          
          PushPayload payload = new PushPayload();
          payload.platform = Platform.all();
          payload.audience = Audience.all();
          payload.notification = new Notification()
                                 .setAndroid(new AndroidNotification()
                                              .setAlert(ALERT)
                                              .setTitle("title"));

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
    
       [TestMethod]
        public void sendNotification_android_buildId()
       {
           
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.notification = new Notification()
                                   .setAlert(ALERT)
                                   .setAndroid(new AndroidNotification()
                                                .setBuilderID(100));
            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
                                               
        }
    
       [TestMethod]
        public void sendNotification_android_extras()  
       {
           
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.notification = new Notification()
                                   .setAlert(ALERT)
                                   .setAndroid(new AndroidNotification()
                                    .AddExtra("key1", "value1")
                                    .AddExtra("key2", 222));
                                                  
            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
    
    
    // ------------------ ios
    
       [TestMethod]
        public void sendNotification_ios_badge() 
       {
            PushPayload payload = new PushPayload();
            payload.platform = Platform.ios();
            payload.audience = Audience.all();
            payload.notification = Notification.ios_auto_badge();

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
    }
}
