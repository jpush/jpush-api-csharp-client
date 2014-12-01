using cn.jpush.api.common;
using cn.jpush.api.push.notification;
using cn.jpush.api.util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace cn.jpush.api.push.mode
{
    public class PushPayload
    {

        private JsonSerializerSettings jSetting;
        private const  String PLATFORM = "platform";
        private const String AUDIENCE = "audience";
        private const String NOTIFICATION = "notification";
        private const String MESSAGE = "message";
        private const String OPTIONS = "options";

        private const int MAX_GLOBAL_ENTITY_LENGTH = 1200;  // Definition acording to JPush Docs
        private const int MAX_IOS_PAYLOAD_LENGTH = 220;  // Definition acording to JPush Docs

        //serializaiton property
        [JsonConverter(typeof(PlatformConverter))]
        public Platform platform{get;set;}
        [JsonConverter(typeof(AudienceConverter))]
        public Audience audience{get;set;}
        public Notification notification { get; set; }
        public Message message { get; set; }
        public Options options { get; set; }
        //construct
        public PushPayload()
        {
            platform = null;
            audience = null;
            notification = null;
            message = null;
            options = new Options();
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
        }
        public PushPayload(Platform platform, Audience audience, Notification notification, Message message = null, Options options = null)
        {
            Debug.Assert(platform != null);
            Debug.Assert(audience != null);
            Debug.Assert(notification != null || message != null);

            this.platform = platform;
            this.audience = audience;
            this.notification = notification;
            this.message = message;
            this.options = options;

            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
        }
        /**
         * The shortcut of building a simple alert notification object to all platforms and all audiences
        */
        public static PushPayload AlertAll(String alert)
        {
            return new PushPayload(Platform.all(),
                                   Audience.all(),
                                   new Notification().setAlert(alert),
                                   null,
                                   new Options());
        }
        //* The shortcut of building a simple message object to all platforms and all audiences
        //*/
        public static PushPayload MessageAll(String msgContent)
        {
            return new PushPayload( Platform.all(),
                                   Audience.all(),
                                   null,
                                   Message.content(msgContent),
                                   new Options());
        }
        public static PushPayload FromJSON(String payloadString)
        {
            try
            {
                var jSetting = new JsonSerializerSettings();
                jSetting.NullValueHandling = NullValueHandling.Ignore;
                jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;

                var jsonObject = JsonConvert.DeserializeObject<PushPayload>(payloadString, jSetting);
                return jsonObject.Check();
            }
            catch (Exception e)
            {
                Console.WriteLine("JSON to PushPayLoad occur error:" + e.Message);
                return null;
            }
        }
        public void ResetOptionsApnsProduction(bool apnsProduction)
        {
            if (this.options == null)
            {
                this.options = new Options();
            }
            this.options.apns_production = apnsProduction;
        }
        public void ResetOptionsTimeToLive(long timeToLive)
        {
            if (this.options == null)
            {
                this.options = new Options();
            }
            this.options.time_to_live = timeToLive;
        }
        public int  GetSendno()
        {
            if (this.options != null)
                return this.options.sendno;
            return 0;
        }
        public bool IsGlobalExceedLength()
        {
            int messageLength = 0;
            if (message!= null)
            {
               var messageJson = JsonConvert.SerializeObject(this.message, jSetting);
               messageLength += UTF8Encoding.UTF8.GetBytes(messageJson).Length;
            }
            if (this.notification == null)
            {
                return messageLength > MAX_GLOBAL_ENTITY_LENGTH;
            }
            else
            {
                var notificationJson = JsonConvert.SerializeObject(this.notification);
                if (notificationJson != null)
                {
                    messageLength += UTF8Encoding.UTF8.GetBytes(notificationJson).Length;
                }
                return messageLength > MAX_GLOBAL_ENTITY_LENGTH;
            }
           
        }
        public bool IsIosExceedLength()
        {
            if (this.notification != null)
            {
                if (this.notification.IosNotification != null)
                {
                    var iosJson = JsonConvert.SerializeObject(this.notification.IosNotification, jSetting);
                    if (iosJson != null)
                    {
                        return UTF8Encoding.UTF8.GetBytes(iosJson).Length > MAX_IOS_PAYLOAD_LENGTH;
                    }
                }
                else
                {
                    if (!(this.notification.alert==null))
                    {
                        string jsonText;
                        using (StringWriter sw = new StringWriter())
                        {
                            JsonWriter writer = new JsonTextWriter(sw);
                            writer.WriteValue(this.notification.alert);
                            writer.Flush();
                            jsonText = sw.GetStringBuilder().ToString();
                        }
                        return UTF8Encoding.UTF8.GetBytes(jsonText).Length > MAX_IOS_PAYLOAD_LENGTH;
                    }
                    else
                    {
                        // No iOS Payload
                    }
                   

                }
                
            }
            return false;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, jSetting);
        }
        public PushPayload Check()
        {
            Preconditions.checkArgument(!(null == audience || null == platform), "audience and platform both should be set.");
            Preconditions.checkArgument(!(null == notification && null == message), "notification or message should be set at least one.");
            if (audience!=null)
            {
                audience.Check();
            }
            if (platform != null)
            {
                platform.Check();
            }
            if (message != null)
            {
                message.Check();
            }
            if (notification != null)
            {
                notification.Check();
            }
            return this;
        }

    }
}
