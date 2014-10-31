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
            String master_secret = "47d264a3c02a6a5a4a256a45 ";
            JPushClient client = new JPushClient(app_key, master_secret);
            PushPayload payload = PushPayload.AlertAll("test");

            //PushPayload payload2 = PushPayload.FromJSON("{\"platform\":\"all\",\"audience\":\"all\",\"notification\":{\"alert\":\"test\"}}");

            client.SendPush(payload);
            Console.WriteLine("*****发送******");
        }

        public static PushPayload PushObject_All_All_Alert()
        {
            return PushPayload.AlertAll(ALERT);
        }
        public static PushPayload PushObject_All_All_Alias_Alert(string alert)
        {
            PushPayload pushPayload = new PushPayload(Platform.all(),
                                                      Audience.alias("alias1"),
                                                      Notification.android(ALERT,TITLE,null));
            return pushPayload;
        }

        public static PushPayload PushObject_Android_Tag_AlertWithTitle()
        {
            return new PushPayload(Platform.android(),
                                   Audience.tag("tag1"),
                                   Notification.alert(TITLE));
        }

    }
}
