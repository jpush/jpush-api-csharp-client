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
            Console.WriteLine("************");
            Console.WriteLine("*****开始发送******");

            //String result;
            String app_key = "_";
            String master_secret = "_";
            //int sendno = 9;
            JPushClient client = new JPushClient(app_key, master_secret);
            
            MessageResult result = null;
            //Params.
            //传入json字符串

            Console.WriteLine("*****发送******");
            //String	msg_json = {
            //    "platform":["ios","android","winphone"],#android,ios,winphone
            //    "audience":"all",#{"alias":[u"别名11"]},#{"registration_id":["0900a5daa82"]},#{"registration_id":["0506b515432"]},#{"alias":[u"苹果公司",u"5890"]}, #{"tag_and":[u"标签",u"看看"]}"registration_id":["040f62efa59","0112a362e14","062214f49a3"]

            //    "notification":
            //        {
            //            "android":
            //            {"alert":u"Android－ %d notifi－V3  %d ,%s" %(i+1,i+1,c_time),
            //            "title":u"android 通知标题 %d" %(i+1),
            //            "builder_id":2,
            //            "extras":
            //                {u"android notification jian1":u"android notification zhi1",u"第%d 次android推送" %(i+1) :"我是值2"}
            //            },

            //            "ios":
            //            {"alert":u"－－ %d apns－－V3 APNS %d , %s" % (i+1,i+1, c_time),#abcde12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",#u"第%d 条ios通知 %s" % (i+1, c_time),
            //            "sound":"sound.caf",
            //            "badge":"3",
            //            "content-available":1,
            //            "extras":
            //                {"sound":"default","badge":"＋5","p":"1035","c":1}
            //                # {"ios apns jian1":"ios apns zhi1",,u"第%d 次ios推送" %(i+1) :"我是值2"}
            //            },

            //            "winphone":
            //            {"alert":u"V3 Winphone MPNS %d ,%s" % (i+1, c_time),
            //            "title":"WinPhone title",
            //            "_open_page":"Page1.xaml",
            //            "extras":
            //                {"winphone mpns key1":"winphone mpns value1",u"第%d 次wp推送" %(i+1) :"我是值2"}
            //            }
            //        }, 

            //    "message":
            //    {
            //        "msg_content":"－－ %d message－－V3:This is  %d 时间：%s" %(i+1,i+1,c_time),
            //       // #This is the %d ‘s message%s" % (i+1, c_time),
            //        "title":u"自定义消息标题 %d" %(i+1),
            //        "content_type":u"我是content-type",
            //        "extras":
            //            {u"MSG键1":u"MSG值1",u"第%d 次推送" %(i+1) :"我是值2" }
            //    },

            //    "options":{"time_to_live":6000, "sendno":sendno,"apns_production":False,"content-avaliable":1}#,"override_msg_id":687188391,"apns_production":False
                //}"
            //

            /**
             *发送类型 
             *APP_KEY      通知
             *TAG           TAG
             *ALIAS       ALIAS
             *REGISTRATION_ID  REGISTRATION_ID
             */
            //notifyParams.OverrideMsgId = "1";

            //result = client.sendNotification("酷派tag111111", notifyParams, extras);
            //Console.WriteLine("sendNotification by tag:**返回状态：" + result.getErrorCode().ToString() +
            //              "  **返回信息:" + result.getErrorMessage() +
            //              "  **Send No.:" + result.getSendNo() +
            //              "  msg_id:" + result.getMessageId() +
            //              "  频率次数：" + result.getRateLimitQuota() +
            //              "  可用频率：" + result.getRateLimitRemaining() +
            //              "  重置时间：" + result.getRateLimitReset());



            //Console.WriteLine("*****发送带tag消息******");

            ////customParams.addPlatform(DeviceEnum.Android);
            //customParams.ReceiverType = ReceiverTypeEnum.TAG;
            //customParams.ReceiverValue = "tag_api";

            //customParams.SendNo = 256;
            //result = client.sendCustomMessage("send custom mess by tag", "tag notify content", customParams, extras);
            //Console.WriteLine("sendCustomMessage:**返回状态：" + result.getErrorCode().ToString() +
            //              "  **返回信息:" + result.getErrorMessage() +
            //              "  **Send No.:" + result.getSendNo() +
            //              "  msg_id:" + result.getMessageId() +
            //              "  频率次数：" + result.getRateLimitQuota() +
            //              "  可用频率：" + result.getRateLimitRemaining() +
            //              "  重置时间：" + result.getRateLimitReset());
            

            //Console.WriteLine();

            //String msg_ids = "1613113584,1229760629,1174658841,1174658641";
            //ReceivedResult receivedResult = client.getReceivedApi(msg_ids);

            //Console.WriteLine("Report Result:");
            //foreach(ReceivedResult.Received re in receivedResult.ReceivedList)
            //{
            //    Console.WriteLine("getReceivedApi************msgid=" + re.msg_id+ "  ***andriod received="+re.android_received+" ***ios received="+re.ios_apns_sent);            
            //}
            //Console.WriteLine();
        }
    
        public class IOSExtras
        {
            public int badge = 888;
            public String sound = "happy";
        }
    }
}
