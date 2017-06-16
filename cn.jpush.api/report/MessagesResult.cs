using cn.jpush.api.common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace cn.jpush.api.report
{
    public class MessagesResult : BaseResult
    {
        public List<Message> messages = new List<Message>();

        public static MessagesResult fromResponse(ResponseWrapper responseWrapper)
        {
            MessagesResult receivedsResult = new MessagesResult();
            if (responseWrapper.responseCode == HttpStatusCode.OK)
            {
                receivedsResult.messages = JsonConvert.DeserializeObject<List<Message>>(responseWrapper.responseContent);
            }
            receivedsResult.ResponseResult = responseWrapper;
            return receivedsResult;
        }

        public override bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }

        public class Message
        {
            public Message()
            {
                msg_id = 0;
                android = null;
                ios = null;
            }

            public long? msg_id;
            public Android android;
            public Ios ios;
        }

        public class Android
        {
            public Android()
            {
                received = 0;
                target = 0;
                online_push = 0;
                click = 0;
            }

            public int? received;
            public int? target;
            public int? online_push;
            public int? click;
        }

        public class Ios
        {
            public Ios()
            {
                apns_sent = 0;
                apns_target = 0;
                click = 0;
            }

            public int? apns_sent;
            public int? apns_target;
            public int? click;
        }
    }
}
