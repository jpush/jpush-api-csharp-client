using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.report;
using cn.jpush.api.common;
using cn.jpush.api.util;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notificaiton;
namespace JpushApiClientExample
{
    class JPushApiExample
    {
        public static String TITLE = "Test from API example";
        public static String ALERT = "Test from API Example - alert";
        public static String MSG_CONTENT = "Test from API Example - msgContent";
        public static String REGISTRATION_ID = "0900e8d85ef";
        public static String TAG = "tag_api";

        static void Main(string[] args)
        {
          
            Console.WriteLine("*****开始发送******");

            String app_key = "997f28c1cea5a9f17d82079a";
            String master_secret = "47d264a3c02a6a5a4a256a45";
            JPushClient client = new JPushClient(app_key, master_secret);

            PushPayload payloadMessage = PushObject_All_All_Alias_Alert(ALERT);
           
            var result = client.SendPush(payloadMessage);

            Console.WriteLine("发送结果：{0}",result);
            Console.WriteLine("*****结束发送******");
        }
        public static PushPayload PushObject_All_All_Alert()
        {
            return PushPayload.AlertAll(ALERT);
        }
        public static PushPayload PushObject_All_All_Alias_Alert(string alert)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = new Platform(true);

            var audience = new Audience();
            audience.alias("alias1","alias2");
            pushPayload.audience = audience;
            pushPayload.notification = new Notification(alert);

            return pushPayload;
            
        }
        public static PushPayload PushObject_Android_Tag_AlertWithTitle()
        {
            //return new PushPayload(Platform.android(),
            //                       Audience.tag("tag1"),
            //                       Notification.alert(TITLE));
            throw new NotImplementedException();
        }
        public static PushPayload PushObject_android_and_ios() 
        {
            //Dictionary<string, string> extras = new Dictionary<string, string>();
            //extras.Add("extras_key", "extras_value");
            //var android = AndroidPlatformNotification.alert("Android Title");
            //var ios = iosPlatformNotification.initWithAlert(null).incrBadge(1).setExtras(extras);

            //return new PushPayload(Platform.android_ios(),
            //                      Audience.tag_and("tag1", "tag_all"),
            //                      Notification.alert("alert content").addPlatform(android).addPlatform(ios)
            //                      );

            throw new NotImplementedException();
        }
        public static PushPayload buildPushObject_ios_tagAnd_alertWithExtrasAndMessage()
        {
            //Dictionary<string, string> extras = new Dictionary<string, string>();
            //extras.Add("from", "JPush");
            //var ios = iosPlatformNotification.initWithAlert(null).incrBadge(1).setExtras(extras);

            //return new PushPayload(Platform.android_ios(),
            //                    Audience.tag_and("tag1", "tag_all"),
            //                    null,
            //                    Message.content(MSG_CONTENT)
            //                    );

            throw new NotImplementedException();
        }

    }
}
