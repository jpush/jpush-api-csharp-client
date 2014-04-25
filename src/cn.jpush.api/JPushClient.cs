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


        public JPushClient(String app_key, String masterSecret, int time_to_live, HashSet<DeviceEnum> devices, bool apnsProduction)
        {
            _pushClient = new PushClient(masterSecret, app_key, time_to_live, devices, apnsProduction);
            _reportClient = new ReportClient(app_key, masterSecret);
        }

        /**
         * 发送通知
         * 
         */
        public MessageResult sendNotification(String notificationContent, NotificationParams notifyParams, String extras)
        {
            return _pushClient.sendNotification(notificationContent, notifyParams, extras);
        }

        /**
         * 发送自定义消息
         * 
         */
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

        /**
         * 
         * @param String $app_key
         * @param String $msg_ids  msg_id以，连接
         */
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
