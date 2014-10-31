using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    public class PushPayload
    {
        private static  String PLATFORM = "platform";
        private static  String AUDIENCE = "audience";
        private static  String NOTIFICATION = "notification";
        private static  String MESSAGE = "message";
        private static  String OPTIONS = "options";

        private static  int MAX_GLOBAL_ENTITY_LENGTH = 1200;  // Definition acording to JPush Docs
        private static  int MAX_IOS_PAYLOAD_LENGTH = 220;  // Definition acording to JPush Docs

        private Platform platform;
        private Audience audience;
        private Notification notificaiton;
        private Message message;
        private Options options;

        public PushPayload(Platform platform, Audience audience, Notification notification, Message message=null, Options options=null)
        {
            Debug.Assert(platform != null);
            Debug.Assert(audience != null);
            Debug.Assert(notificaiton != null || message != null);

            this.platform = platform;
            this.audience = audience;
            this.notificaiton = notification;
            this.message = message;
            this.options = options;
        }
        /**
         * The shortcut of building a simple alert notification object to all platforms and all audiences
        */
        public static PushPayload AlertAll(String alert)
        {
            return new PushPayload(Platform.all(),
                                   Audience.all(),
                                   Notification.alert(alert),
                                   null,
                                   null);
        }
        /**
        * The shortcut of building a simple message object to all platforms and all audiences
        */
        public static PushPayload MessageAll(String msgContent)
        {
            return new PushPayload(Platform.all(),
                                   Audience.all(),
                                   null,
                                   Message.content(msgContent),
                                   null);
        }

        public static PushPayload FromJSON(String payloadString)
        {
            try
            {
               PushPayload pushPayLoad = JsonConvert.DeserializeObject<PushPayload>(payloadString);
               return pushPayLoad;
            }
            catch(Exception e)
            {
                Console.WriteLine("JSON to PushPayLoad occur error:" + e.Message);
                return null;
            }
        }
        public void ResetOptionsApnsProduction(bool apnsProduction) 
        {
             throw new NotImplementedException();
        }
        public void ResetOptionsTimeToLive(long timeToLive)
        {
            throw new NotImplementedException();
        }
        public int  GetSendno()
        {
            throw new NotImplementedException();
        }
        public bool IsGlobalExceedLength()
        {
            throw new NotImplementedException();
        }
        public bool IsIosExceedLength()
        {
            throw new NotImplementedException();
        }
        public string ToJson()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (platform != null)
            {
                dict.Add(PLATFORM,platform.toJsonObject());
            }
            if(audience != null)
            {
                dict.Add(AUDIENCE, audience.toJsonObject());
            }
            if (notificaiton != null)
            {
                dict.Add(NOTIFICATION, notificaiton.toJsonObject());
            }
            if(message != null)
            {
                dict.Add(MESSAGE, message.toJsonObject());
            }
            if(options !=null)
            {
                dict.Add(OPTIONS, options.toJsonObject());
            }
            return JsonConvert.SerializeObject(dict);
            //return JsonTool.DictionaryToJson(dict);
        }
    }
}
