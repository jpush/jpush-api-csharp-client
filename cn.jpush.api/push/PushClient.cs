using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        public PushClient(String appKey,String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public MessageResult sendPush(PushPayload payload) 
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            payload.Check();
            String payloadJson = payload.ToJson();
            return sendPush(payloadJson);
        }
        public MessageResult sendPush(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
           
            String url = HOST_NAME_SSL;
            url += PUSH_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            MessageResult messResult = new MessageResult();
            messResult.ResponseResult = result;
           
            JpushSuccess jpushSuccess = JsonConvert.DeserializeObject<JpushSuccess>(result.responseContent);
            messResult.sendno = long.Parse(jpushSuccess.sendno);
            messResult.msg_id = long.Parse(jpushSuccess.msg_id);
           
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
