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
        public static String app_key = "997f28c1cea5a9f17d82079a";
        public static String master_secret = "47d264a3c02a6a5a4a256a45";

        static void Main(string[] args)
        {
          
            Console.WriteLine("*****开始发送******");

            JPushClient client = new JPushClient(app_key, master_secret);
            PushPayload payloadMessage = PushObject_android_and_ios();
            var result = client.SendPush(payloadMessage);
            var apiResult = client.getReceivedApi(result.msg_id.ToString());

            Console.WriteLine("发送结果：{0}", result);
            Console.WriteLine("*****结束发送******");
        }
        public static PushPayload PushObject_All_All_Alert()
        {
            return PushPayload.AlertAll(ALERT);
        }
        public static PushPayload PushObject_All_All_Alias_Alert(string alert)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform =  Platform.all();

            pushPayload.audience = Audience.s_alias("alias1", "alias2"); 
            pushPayload.notification = new Notification(alert);

            return pushPayload;
        }
        public static PushPayload PushObject_Android_Tag_AlertWithTitle()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.ios();
           
            pushPayload.audience = Audience.s_tag("tag1"); 
            pushPayload.notification =  Notification.android(ALERT,TITLE);

            return pushPayload;
        }
        public static PushPayload PushObject_android_and_ios()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();

            var audience = Audience.s_tag("tag1");

            pushPayload.audience = audience;

            pushPayload.notification = new Notification("alert content");
            pushPayload.notification.AndroidNotification = new AndroidNotification().setTitle("Android Title");

            pushPayload.notification.iosNotification = new IosNotification();
            pushPayload.notification.iosNotification.incrBadge(1);
            pushPayload.notification.iosNotification.AddExtra("extra_key", "extra_value");
       
            return pushPayload;
        }
        public static PushPayload buildPushObject_ios_tagAnd_alertWithExtrasAndMessage()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();
            pushPayload.audience = Audience.s_tag("tag1", "tag2").alias("alias1", "alias2"); ;
            Dictionary<string,string> extras=new Dictionary<string,string>();
            extras.Add("from","JPush");
            return pushPayload;

        }

    }
}
