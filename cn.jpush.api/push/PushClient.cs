using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push
{
    internal class PushClient:BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.jpush.cn";
        private const String PUSH_PATH = "/v3/push";
        
        private String appKey;
        private String masterSecret;
        public PushClient(String masterSecret, String appKey)
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public MessageResult sendPush(PushPayload payload) 
        {
            Debug.Assert(payload != null);
            Debug.Assert(payload.platform != null);
            Debug.Assert(payload.audience != null);
            Debug.Assert(payload.message!= null||payload.notification!=null);
            if (payload != null && 
                payload.platform != null &&
                payload.audience != null && 
                (payload.message != null || payload.notification != null))
            {
                String payloadJson = payload.ToJson();
                return sendPush(payloadJson);
            }
            return null;
        }
        public MessageResult sendPush(string payloadString)
        {
            String url = HOST_NAME_SSL;
            url += PUSH_PATH;
            ResponseResult result = sendPost(url, Authorization(), payloadString);
            MessageResult messResult = new MessageResult();
            if (result.responseCode == System.Net.HttpStatusCode.OK)
            {
                messResult = (MessageResult)JsonTool.JsonToObject(result.responseContent, messResult);
                String content = result.responseContent;
            }
            messResult.ResponseResult = result;
            return messResult;
        }
        private String Authorization(){

            Debug.Assert(!string.IsNullOrEmpty(this.appKey));
            Debug.Assert(!string.IsNullOrEmpty(this.masterSecret));

            String origin=this.appKey+":"+this.masterSecret;
            return  Base64.getBase64Encode(origin);
        }
    }
    enum MsgTypeEnum
    {
        NOTIFICATIFY = 1,
        COUSTOM_MESSAGE =2
    }
}
