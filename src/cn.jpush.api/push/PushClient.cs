using cn.jpush.api.common;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push
{
    class PushClient:BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.jpush.cn";
        private const String HOST_NAME = "http://api.jpush.cn:8800";
        private const String PUSH_PATH = "/v2/push";

        private String appKey;
        private String masterSecret;
        private bool enableSSL = false;
        private long timeToLive;
        private bool apnsProduction = false;
        private HashSet<DeviceEnum> devices = new HashSet<DeviceEnum>();

        public PushClient(String masterSecret, String appKey, long timeToLive, DeviceEnum device, bool apnsProduction) 
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;
            this.timeToLive = timeToLive;
            if (device != null)
            {
                this.devices.Add(device);            
            }        
        }

        public MessageResult sendNotification(String notificationContent, NotificationParams notParams, Dictionary<String, Object> extras)
        {
            if (extras != null)
            {
                notParams.MsgContent.Extras = extras;

            }
            return sendMessage(notificationContent, notParams);
        }

        public MessageResult sendCustomMessage(String msgTitle, String msgContent, CustomMessageParams cParams, Dictionary<String, Object> extras)
        {
            if (msgTitle != null)
            {
                cParams.MsgContent.Title = msgTitle;
            }
            if (extras != null)
            {
                cParams.MsgContent.Extras = extras;
            }
            return sendMessage(msgContent, cParams);
        }


        private MessageResult sendMessage(String notificationContent, MessageParams msgParams) 
        {
            msgParams.ApnsProduction = this.apnsProduction ? 1 : 0;
            msgParams.AppKey = this.appKey;
            msgParams.MasterSecret = this.masterSecret;
            if (msgParams.TimeToLive == MessageParams.NO_TIME_TO_LIVE) 
            {
                msgParams.TimeToLive = this.timeToLive;            
            }
            foreach(DeviceEnum device in this.devices)
            {
                msgParams.addPlatform(device);
            }
            return null; 
        }

        private MessageResult sendPush(String notificationContent, MessageParams msgParams, MsgTypeEnum msgType) 
        { 
            String url = enableSSL ? HOST_NAME_SSL : HOST_NAME;
            url += PUSH_PATH;
            String pamrams = prase(msgParams, msgType);
            ResponseResult result = sendPost(url, pamrams, null);
            MessageResult messResult = new MessageResult();
            messResult.ResponseResult = result;
            if (result.responseCode == System.Net.HttpStatusCode.OK) 
            {
                messResult = (MessageResult)JsonTool.JsonToObject(result.responseContent, messResult);
                String content = result.responseContent;
            }

            return messResult;

        }

        private String prase(MessageParams message, MsgTypeEnum msgType) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(message.SendNo).Append(message.ReceiverType).Append(message.ReceiverValue).Append(message.MasterSecret);
            String verificationCode = sb.ToString();
            verificationCode = Md5.getMD5Hash(verificationCode);
            sb.Clear();

            sb.Append("sendno=").Append(message.SendNo).Append("&app_key=").Append(message.AppKey).Append("&receiver_type=").Append(message.ReceiverType)
                .Append("&receiver_value=").Append(message.ReceiverValue).Append("&verification_code=").Append(verificationCode)
                .Append("&msg_type=").Append(msgType).Append("&msg_content=").Append(message.MessageContent).Append("&platform=").Append(message.getPlatform())
                .Append("&apns_production=").Append(message.ApnsProduction);
            if(message.TimeToLive >= 0)
            {
                sb.Append("&time_to_live=").Append(message.TimeToLive);
            }
            if(message.OverrideMsgId != null)
            {
                sb.Append("&override_msg_id=").Append(message.OverrideMsgId);
            }
            return sb.ToString();
        }

    }

    enum MsgTypeEnum
    {
        NOTIFICATIFY = 1,
        COUSTOM_MESSAGE =2
    }
}
