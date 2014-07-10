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
    class Program
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
            String master_secret = "e743748895a980e2f3a91b23";
            String app_key = "e282be7e8127908fb7012ef7";
            //int sendno = 9;

            HashSet<DeviceEnum> set = new HashSet<DeviceEnum>();
            set.Add(DeviceEnum.Android);
            set.Add(DeviceEnum.IOS);
            JPushClient client = new JPushClient(app_key, master_secret, 0, set, true);

            MessageResult result = null;

            NotificationParams notifyParams = new NotificationParams();
            CustomMessageParams customParams = new CustomMessageParams();

            //notifyParams.

            //传入json字符串


            String extras = null;


            extras = "{\"ios\":{\"badge\":88, \"sound\":\"happy\"}}";


            //extras中有中文请用HttpUtility.UrlEncode编码
            //System.Web.HttpUtility.UrlEncode(notificationContent, Encoding.UTF8);

            Console.WriteLine("*****发送带tag通知******");

            /**
             *发送类型 
             *APP_KEY      通知
             *TAG           TAG
             *ALIAS       ALIAS
             *REGISTRATION_ID  REGISTRATION_ID
             */
            //notifyParams.ReceiverType = ReceiverTypeEnum.APP_KEY;

            notifyParams.ReceiverType = ReceiverTypeEnum.TAG;
            notifyParams.ReceiverValue = "tag_api";

            //notifyParams.ReceiverType = ReceiverTypeEnum.ALIAS;
            //notifyParams.ReceiverValue = "alias_api";
            //notifyParams.ReceiverType = ReceiverTypeEnum.REGISTRATION_ID;
            //notifyParams.ReceiverValue = "REGISTRATION_ID";
            //notifyParams.SendNo = sendno

            notifyParams.SendNo = 256;
            //notifyParams.OverrideMsgId = "1";

            result = client.sendNotification("酷派tag", notifyParams, extras);
            Console.WriteLine("sendNotification by tag:**返回状态：" + result.getErrorCode().ToString() +
                          "  **返回信息:" + result.getErrorMessage() +
                          "  **Send No.:" + result.getSendNo() +
                          "  msg_id:" + result.getMessageId() +
                          "  频率次数：" + result.getRateLimitQuota() +
                          "  可用频率：" + result.getRateLimitRemaining() +
                          "  重置时间：" + result.getRateLimitReset());



            Console.WriteLine("*****发送带tag消息******");

            //customParams.addPlatform(DeviceEnum.Android);
            customParams.ReceiverType = ReceiverTypeEnum.TAG;
            customParams.ReceiverValue = "tag_api";

            //customParams.ReceiverType = ReceiverTypeEnum.ALIAS;
            //customParams.ReceiverValue = "alias_api";
            //customParams.ReceiverType = ReceiverTypeEnum.REGISTRATION_ID;
            //customParams.ReceiverValue = "REGISTRATION_ID";

            customParams.SendNo = 256;
            result = client.sendCustomMessage("send custom mess by tag", "tag notify content", customParams, extras);
            Console.WriteLine("sendCustomMessage:**返回状态：" + result.getErrorCode().ToString() +
                          "  **返回信息:" + result.getErrorMessage() +
                          "  **Send No.:" + result.getSendNo() +
                          "  msg_id:" + result.getMessageId() +
                          "  频率次数：" + result.getRateLimitQuota() +
                          "  可用频率：" + result.getRateLimitRemaining() +
                          "  重置时间：" + result.getRateLimitReset());
            

            Console.WriteLine();

            String msg_ids = "1613113584,1229760629,1174658841,1174658641";
              ReceivedResult receivedResult = client.getReceivedApi(msg_ids);

              Console.WriteLine("Report Result:");
            foreach(ReceivedResult.Received re in receivedResult.ReceivedList)
            {
                Console.WriteLine("getReceivedApi************msgid=" + re.msg_id+ "  ***andriod received="+re.android_received+" ***ios received="+re.ios_apns_sent);            
            }
            Console.WriteLine();
        }
    
        public class IOSExtras
        {
            public int badge = 888;
            public String sound = "happy";
        }
    }
}
