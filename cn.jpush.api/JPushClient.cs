using cn.jpush.api.common;
using cn.jpush.api.push;
using cn.jpush.api.report;
using System;
using System.Collections.Generic;
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

        public JPushClient(String app_key, String masterSecret)
        {
            HashSet<DeviceEnum> devices = new HashSet<DeviceEnum>();
            devices.Add(DeviceEnum.IOS);
            devices.Add(DeviceEnum.Android);
            _pushClient = new PushClient(masterSecret, app_key, MessageParams.NO_TIME_TO_LIVE, null, true);
            _reportClient = new ReportClient(app_key, masterSecret);
        }


        /// <summary>
        /// JPushClient构造函数，可指定
        /// </summary>
        /// <param name="app_key">Portal上产生的app_key</param>
        /// <param name="masterSecret">你的API MasterSecret</param>
        /// <param name="time_to_live">有效期</param>
        /// <param name="platform">目标推送平台</param>
        /// <param name="apnsProduction">是否iOS生产环境 - true为生产环境，false为开发环境</param>
        public JPushClient(String app_key, String masterSecret, int time_to_live, HashSet<DeviceEnum> platform, bool apnsProduction)
        {
            _pushClient = new PushClient(masterSecret, app_key, time_to_live, platform, apnsProduction);
            _reportClient = new ReportClient(app_key, masterSecret);
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notificationContent">通知内容</param>
        /// <param name="notifyParams"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        public MessageResult sendNotification(String notificationContent, NotificationParams notifyParams, String extras)
        {
            return _pushClient.sendNotification(notificationContent, notifyParams, extras);
        }

        /// <summary>
        /// 发送自定义消息
        /// </summary>
        /// <param name="msgTitle"></param>
        /// <param name="msgContent"></param>
        /// <param name="customParams"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        public MessageResult sendCustomMessage(String msgTitle, String msgContent, CustomMessageParams customParams, String extras)
        {
            return _pushClient.sendCustomMessage(msgTitle, msgContent, customParams, extras);
        }

        public MessageResult sendNotificationAll(String notificationContent)
        {
            NotificationParams notifyParams = new NotificationParams();
            notifyParams.ReceiverType = ReceiverTypeEnum.APP_KEY;
            return _pushClient.sendNotification(notificationContent, notifyParams, null);
        }

	    public MessageResult sendCustomMessageAll(String msgTitle, String msgContent) {
            CustomMessageParams customParams = new CustomMessageParams();
            customParams.ReceiverType = ReceiverTypeEnum.APP_KEY;
	        return _pushClient.sendCustomMessage(msgTitle, msgContent, customParams, null);
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
