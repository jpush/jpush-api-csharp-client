using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace cn.jpush.api.push
{
    internal class PushClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.jpush.cn";
        private const string PUSH_PATH = "/v3/push";

        private string appKey;
        private string masterSecret;

        public PushClient(string appKey, string masterSecret)
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
            string payloadJson = payload.ToJson();
            return sendPush(payloadJson);
        }

        public MessageResult sendPush(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadstring should not be empty");

            string url = HOST_NAME_SSL;
            url += PUSH_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            MessageResult messResult = new MessageResult()
            {
                ResponseResult = result
            };
            JpushSuccess jpushSuccess = JsonConvert.DeserializeObject<JpushSuccess>(result.responseContent);
            messResult.sendno = long.Parse(jpushSuccess.sendno);
            messResult.msg_id = long.Parse(jpushSuccess.msg_id);
            return messResult;
        }

        private string Authorization()
        {
            Debug.Assert(!string.IsNullOrEmpty(appKey));
            Debug.Assert(!string.IsNullOrEmpty(masterSecret));

            string origin = appKey + ":" + masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }

    enum MsgTypeEnum
    {
        NOTIFICATIFY = 1,
        COUSTOM_MESSAGE = 2
    }
}
