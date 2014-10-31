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

        public class ExtralClass
        {
            public String sound = "ssss";

            public String menu="button";
        }


        static void Main(string[] args)
        {
          
            Console.WriteLine("*****开始发送******");

            String app_key = "997f28c1cea5a9f17d82079a";
            String master_secret = "47d264a3c02a6a5a4a256a45 ";
            JPushClient client = new JPushClient(app_key, master_secret);
            PushPayload payload = PushPayload.AlertAll("test");

            client.SendPush(payload);
            Console.WriteLine("*****发送******");
        }
    
        public class IOSExtras
        {
            public int badge = 888;
            public String sound = "happy";
        }
    }
}
