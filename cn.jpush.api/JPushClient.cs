using cn.jpush.api.common;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using cn.jpush.api.report;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api
{
    /// <summary>
    /// Main Entrance - 该类为JPush服务的主要入口
    /// </summary>
    public class JPushClient
    {
        private PushClient _pushClient;
        private ReportClient _reportClient;
        /// <summary>
        /// 带两个参数的构造函数，该状态下，ApnsProduction默认为true
        /// </summary>
        /// <param name="app_key">Portal上产生的app_key</param>
        /// <param name="masterSecret">你的API MasterSecret</param>
        public JPushClient(String app_key, String masterSecret)
        {
            _pushClient = new PushClient(masterSecret, app_key);
            _reportClient = new ReportClient(app_key, masterSecret);
            List<int> list0 = new List<int>();
            list0.Add(1);

            List<String> list1 = new List<String>();
            list1.Add("2");
            list1.Add("3");

            Dictionary<string, object> dict2 = new Dictionary<string, object>();
            dict2.Add("subdict", 2);


            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("int",2);
            dict.Add("string", "2");
            dict.Add("list<int>", list0);
            dict.Add("list<string>",list1);
            dict.Add("dict<string,object>", dict2);
            string json = JsonTool.DictionaryToJson(dict);
            Debug.WriteLine(json);
            //
        }
        /// <summary>
        /// JPushClient构造函数，可指定
        /// </summary>
        /// <param name="app_key">Portal上产生的app_key</param>
        /// <param name="masterSecret">你的API MasterSecret</param>
        /// <param name="time_to_live">有效期</param>
        /// <param name="platform">目标推送平台</param>
        /// <param name="apnsProduction">是否iOS生产环境 - true为生产环境，false为开发环境</param>

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notificationContent">通知内容</param>
        /// <param name="notifyParams"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        //public MessageResult sendNotification(String notificationContent, NotificationParams notifyParams, String extras)
        //{
        //    return _pushClient.sendNotification(notificationContent, notifyParams, extras);
        //}

        /// <summary>
        /// 发送自定义消息
        /// </summary>
        /// <param name="msgTitle"></param>
        /// <param name="msgContent"></param>
        /// <param name="customParams"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        //public MessageResult sendCustomMessage(String msgTitle, String msgContent, CustomMessageParams customParams, String extras)
        //{
        //    return _pushClient.sendCustomMessage(msgTitle, msgContent, customParams, extras);
        //}

        //public MessageResult sendNotificationAll(String notificationContent)
        //{
        //    NotificationParams notifyParams = new NotificationParams();
        //    notifyParams.ReceiverType = ReceiverTypeEnum.APP_KEY;
        //    return _pushClient.sendNotification(notificationContent, notifyParams, null);
        //}

        //public MessageResult sendCustomMessageAll(String msgTitle, String msgContent) {
        //    CustomMessageParams customParams = new CustomMessageParams();
        //    customParams.ReceiverType = ReceiverTypeEnum.APP_KEY;
        //    return _pushClient.sendCustomMessage(msgTitle, msgContent, customParams, null);
        //}
        public void SendPush(String message){

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg_ids">需要查询的msg_id列表，多个msg_id之间用逗号(,)分隔</param>
        /// <returns>ReceivedResult</returns>
        /// 
        public ReceivedResult getReceivedApi(String msg_ids)
        {
            return _reportClient.getReceiveds(msg_ids);
        }

        static void Main(String[] args) 
        {
            Console.WriteLine("2222");
        }
    }
}
