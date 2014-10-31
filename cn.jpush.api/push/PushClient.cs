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
        //private const String HOST_NAME_SSL = "http://192.168.3.1:20015";
        //private const String PUSH_PATH = "";

        private String appKey;
        private String masterSecret;
        //private bool enableSSL = false;
        //private long timeToLive;
        //private bool apnsProduction = false;
        //private HashSet<DeviceEnum> devices = new HashSet<DeviceEnum>();

        public PushClient(String masterSecret, String appKey)
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;

        }
        public MessageResult sendPush(PushPayload payload) 
        {
            String msgParams = payload.ToJson();
            String url =   HOST_NAME_SSL ;
            url += PUSH_PATH;
            //String pamrams = prase(msgParams, msgType);
            Console.WriteLine("begin post");
            Console.WriteLine("send json:{0}",msgParams);
            ResponseResult result = sendPost(url, Authorization(), msgParams);
            Console.WriteLine("end post");

            MessageResult messResult = new MessageResult();
            if (result.responseCode == System.Net.HttpStatusCode.OK)
            {
                //Console.WriteLine("responseContent===" + result.responseContent);
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
            //return 
        }

    }
    enum MsgTypeEnum
    {
        NOTIFICATIFY = 1,
        COUSTOM_MESSAGE =2
    }
}
