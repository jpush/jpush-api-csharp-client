using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class ExceptionTest:BaseRemotePushTest
    {
        [TestMethod]
        public void appKeyNotExist()
        {
            String appKey = "dd1066407b044738b6479274";
            JPushClient client = new JPushClient(appKey, MASTER_SECRET);
            PushPayload payload = PushPayload.AlertAll(ALERT);
            try
            {
                var result = _client.SendPush(payload);
            }
            catch (APIRequestException e)
            {
               Assert.AreEqual(APPKEY_NOT_EXIST, e.ErrorCode);
            }
        }
        [TestMethod]
        public void authenticationFail()
        {
            String masterSecret = "2b38ce69b1de2a7fa95706e2";
            JPushClient client = new JPushClient(masterSecret, APP_KEY);
            PushPayload payload = PushPayload.AlertAll(ALERT);
            try
            {
                var result = _client.SendPush(payload);
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(AUTHENTICATION_FAIL, e.ErrorCode);
            }
        }
        [TestMethod]
        public void tooBig() 
        {
            String msgContent = "娣卞湷鍒堕�犲巶鐨勬湅鍙嬪憡璇夋垜锛岃繃鍘荤殑涓�骞达紝浠栦滑鏈嶅姟浜嗗嚑鍗佸灏忓瀷鍒涗笟鍏徃锛屼唬宸ユ櫤鑳芥墜琛ㄣ�備笉杩囷紝浠婂勾杩欎簺鍒涗笟鍏徃宸茬粡鎵句笉鍒颁簡锛屽簡骞哥殑鏄紝浠ｅ伐鍘傞兘鏄厛浠樻鍐嶇敓浜э紝涔熷氨娌℃湁鎹熷け銆傚彲绌挎埓璁惧銆佺‖浠跺垱鏂帮紝澶ф疆鍒濊捣锛屾偿娌欎勘涓嬶紝娴疆杩囧悗锛屽嵈鏄亶鍦扮嫾钘夈�傚浗鍐呯殑鏅鸿兘鎵嬬幆銆佹墜琛ㄤ滑锛屽鍦熸浖銆佹灉澹筹紝鍦� Jawbone銆丟oogle Glass 浠紩棰嗕笅锛岀悍绾锋帹鍑衡�滃垝鏃朵唬鈥濈殑浜у搧锛屼竴鏃堕棿锛屽浗鍐呭绉拌鍋氬彲绌挎埓璁惧鐨勫叕鍙革紝濡傝繃姹熶箣椴��2013 骞达紝涓嶈鍙ョ‖浠跺垱鏂帮紝涓嶆埓娆炬櫤鑳芥墜鐜紝閮戒笉濂芥剰鎬濊鑷繁鏄珯鍦ㄤ汉鏂囦笌绉戞妧鐨勫崄瀛楄矾鍙ｃ��2013 骞达紝韬竟鐨勬湅鍙嬬悍绾蜂僵鎴翠笂浜� Jawbone锛屽垢杩愮殑浜轰篃浼氭埓涓婁紶璇翠腑鐨勬櫤鑳芥墜琛ㄣ�備笉杩囷紝鐜板湪瓒婃潵瓒婂鐨勬湅鍙嬪紑濮嬫斁寮冭繖浜涙墍璋撶殑鍙┛鎴村紡璁惧銆�";
            PushPayload payload = PushPayload.MessageAll(msgContent);
            String content = payload.ToString();
            Console.WriteLine("len: " + System.Text.UTF8Encoding.UTF8.GetByteCount(content));
            try {
                var result = _client.SendPush(payload);
            } catch (APIRequestException e) {
                Assert.AreEqual(TOO_BIG, e.ErrorCode);
            }
        }
        [TestMethod]
	    public void invalidParams_platform() {
            JObject payload = new JObject();
	        payload.Add("platform", JToken.FromObject("all_platform"));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
            payload.Add("notification", JToken.FromObject(new  Notification().setAlert(ALERT)));

            Console.WriteLine("json string: " + payload.ToString());
	        
	        try {
                var result = _client.SendPush(payload.ToString());
	        } catch (APIRequestException e) {
                 Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
	        }
	    }
        [TestMethod]
        public void invalidParams_audience() 
        {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject("all_audience"));
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.DefaultValueHandling = DefaultValueHandling.Ignore;
            payload.Add("notification", JToken.FromObject(new Notification().setAlert(ALERT), jsonSerializer));

            Console.WriteLine("json string: " + payload.ToString());
            try {
                 var result = _client.SendPush(payload.ToString());
            }catch (APIRequestException e) {
                 Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
	        }
         }
        [TestMethod]
        public void invalidParams_notification()
         {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
          
            payload.Add("notification",  JToken.FromObject(ALERT));
            Console.WriteLine("json string: " + payload.ToString());
        
            try {
                _client.SendPush(payload.ToString());

            } catch (APIRequestException e) {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_android()
        {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
           
            JObject notification = new JObject();
            notification.Add("android", JToken.FromObject(ALERT));
            payload.Add("notification", notification);

            Console.WriteLine("json string: " + payload.ToString());
        
            try {
                _client.SendPush(payload.ToString());
           
            } catch (APIRequestException e) {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_ios()
         {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
            JObject notification = new JObject();
            notification.Add("ios",JToken.FromObject(ALERT));
            payload.Add("notification", notification);

            Console.WriteLine("json string: " + payload.ToString());
        
            try {
                _client.SendPush(payload.ToString());
            
            } catch (APIRequestException e) {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);

            }
        }
        [TestMethod]
        public void invalidParams_notification_winphone()
        {
           JObject payload = new JObject();
           payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
           payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
           JObject notification = new JObject();
           notification.Add("winphone", JToken.FromObject(ALERT));
           payload.Add("notification", notification);
           Console.WriteLine("json string: " + payload.ToString());
           try
           {
                _client.SendPush(payload.ToString());
           }
           catch (APIRequestException e)
           {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
           }
        }
        [TestMethod]
        public void invalidParams_notification_android_builderidNotNumber() 
         {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
            JObject notification = new JObject();
            JObject android = new JObject();
            android.Add("builder_id", JToken.FromObject("builder_id_string"));
        
            notification.Add("android", android);
            payload.Add("notification", notification);

            Console.WriteLine("json string: " + payload.ToString());

            try
            {
                _client.SendPush(payload.ToString());
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_android_empty()
        {
             JObject payload = new JObject();
             payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
             payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
        
             JObject notification = new JObject();
              JObject android = new JObject();
        
            notification.Add("android", android);
            payload.Add("notification", notification);
            Console.WriteLine("json string: " + payload.ToString());
            try
            {
                _client.SendPush(payload.ToString());
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_ios_empty() {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
           JObject notification = new JObject();
           JObject ios = new JObject();
        
        
            notification.Add("ios", ios);
            payload.Add("notification", notification);

            Console.WriteLine("json string: " + payload.ToString());
            try
            {
                _client.SendPush(payload.ToString());
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_winphone_empty() 
        {
             JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
           JObject notification = new JObject();
           JObject winphone = new JObject();
        
            notification.Add("winphone", winphone);
            payload.Add("notification", notification);

            Console.WriteLine("json string: " + payload.ToString());
            try
            {
                _client.SendPush(payload.ToString());
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_android_noalert() 
         {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
           JObject notification = new JObject();
           JObject android = new JObject();
           android.Add("title", JToken.FromObject("title"));

           notification.Add("android", android);
           payload.Add("notification", notification);

            Console.WriteLine("json string: " + payload.ToString());
            try
            {
                _client.SendPush(payload.ToString());
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_ios_noalert() 
         {
            JObject payload = new JObject();
            payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
        
               JObject notification = new JObject();
               JObject ios = new JObject();
               ios.Add("badge",JToken.FromObject(11));
        
            notification.Add("ios", ios);
            payload.Add("notification", notification);
            Console.WriteLine("json string: " + payload.ToString());
            try
            {
                _client.SendPush(payload.ToString());
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void invalidParams_notification_winphone_noalert() 
        {
            JObject payload = new JObject();
             payload.Add("platform", JToken.FromObject(JsonConvert.SerializeObject(Platform.all(), new PlatformConverter())));
            payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));
        
        
             JObject notification = new JObject();
               JObject winphone = new JObject();
            winphone.Add("title",  JToken.FromObject("title"));
        
            notification.Add("winphone", winphone);
            payload.Add("notification", notification);

            Console.WriteLine("json string: " + payload.ToString());
            try
            {
                _client.SendPush(payload.ToString());
            }
            catch (APIRequestException e)
            {
                Assert.AreEqual(INVALID_PARAMS, e.ErrorCode);
            }
        }
        [TestMethod]
        public void lackOfParams_platform()
       {
           JsonSerializer jsonSerializer = new JsonSerializer();
           jsonSerializer.DefaultValueHandling = DefaultValueHandling.Ignore;
           JObject payload = new JObject();
           payload.Add("audience", JToken.FromObject(JsonConvert.SerializeObject(Audience.all(), new AudienceConverter())));

           payload.Add("notification", JToken.FromObject(new Notification().setAlert(ALERT), jsonSerializer));
          
           Console.WriteLine("json string: " + payload.ToString());
           try
           {
               _client.SendPush(payload.ToString());
           }
           catch (APIRequestException e)
           {
               Assert.AreEqual(LACK_OF_PARAMS, e.ErrorCode);
           }


        }
    }
}
