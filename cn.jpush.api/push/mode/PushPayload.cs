using cn.jpush.api.common;
using cn.jpush.api.push.notificaiton;
using cn.jpush.api.util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        private const  String PLATFORM = "platform";
        private const String AUDIENCE = "audience";
        private const String NOTIFICATION = "notification";
        private const String MESSAGE = "message";
        private const String OPTIONS = "options";

        private const int MAX_GLOBAL_ENTITY_LENGTH = 1200;  // Definition acording to JPush Docs
        private const int MAX_IOS_PAYLOAD_LENGTH = 220;  // Definition acording to JPush Docs
        [JsonConverter(typeof(PlatformConverter))]
        public Platform platform{get;set;}
        [JsonConverter(typeof(AudienceConverter))]
        public Audience audience{get;set;}
        //public Notification notification;
        //public Message message;
        //public Options options;
        public PushPayload()
        {
            platform = null;
        }

        public static string ToJson()
        {
            PushPayload payload = new PushPayload();

            payload.platform = Platform.android_ios();
            payload.audience = new Audience();
            payload.audience.tag("1","2","3");
            payload.audience.tag("4", "5", "6");

            //JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            //jsonSetting.NullValueHandling = NullValueHandling.Ignore;

            var json = JsonConvert.SerializeObject(payload, new PlatformConverter(), new AudienceConverter());
            var jsonObject = JsonConvert.DeserializeObject<PushPayload>(json, new PlatformConverter(), new AudienceConverter());
            return json;
        }
        //public PushPayload(Platform platform, Audience audience, Notification notification, Message message=null, Options options=null)
        //{
        //    Debug.Assert(platform != null);
        //    Debug.Assert(audience != null);
        //    Debug.Assert(notification != null || message != null);

        //    this.platform = platform;
        //    this.audience = audience;
        //    this.notification = notification;
        //    this.message = message;
        //    this.options = options;
        //    var jSetting = new JsonSerializerSettings();
        //    jSetting.NullValueHandling = NullValueHandling.Ignore;
        //    var json = JsonConvert.SerializeObject(platform,jSetting);

        //    var jsonConver = new JavaScriptDateTimeConverter();
        //    var jsonPlatform = JsonConvert.DeserializeObject<Platform>(json, jSetting);
        //    Debug.WriteLine(json);

        //    json = JsonConvert.SerializeObject(audience, jSetting);
        //    var jsonAudience = JsonConvert.DeserializeObject<Audience>(json, jSetting);
        //    Debug.WriteLine(json);

        //}
        ///**
        // * The shortcut of building a simple alert notification object to all platforms and all audiences
        //*/
        //public static PushPayload AlertAll(String alert)
        //{
        //    return new PushPayload(new Platform(),
        //                           Audience.all(),
        //                           new Notification(alert),
        //                           null,
        //                           null);
        //}
        ///**
        //* The shortcut of building a simple message object to all platforms and all audiences
        //*/
        //public static PushPayload MessageAll(String msgContent)
        //{
        //    return new PushPayload(new Platform(),
        //                           Audience.all(),
        //                           null,
        //                           Message.content(msgContent),
        //                           null);
        //}
        
        //public static PushPayload FromJSON(String payloadString)
        //{
        //    try
        //    {
        //       var jsonObject = JsonConvert.DeserializeObject(payloadString);
        //       return null;
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine("JSON to PushPayLoad occur error:" + e.Message);
        //        return null;
        //    }
        //}
        //public void ResetOptionsApnsProduction(bool apnsProduction) 
        //{
        //     throw new NotImplementedException();
        //}
        //public void ResetOptionsTimeToLive(long timeToLive)
        //{
        //    throw new NotImplementedException();
        //}
        //public int  GetSendno()
        //{
        //    throw new NotImplementedException();
        //}
        //public bool IsGlobalExceedLength()
        //{
        //    throw new NotImplementedException();
        //    //int messageLength = 0;
        //    //if (notification!=null)
        //    //{
        //    //    var notificationContent=notification.toJsonObject();
        //    //    var notificaitonJson = JsonConvert.SerializeObject(notificationContent);
        //    //    if (notificaitonJson != null)
        //    //    {
        //    //        messageLength += UTF8Encoding.UTF8.GetBytes(notificaitonJson).Length;
        //    //    }
        //    //    if( message ==null){
        //    //        return messageLength > MAX_GLOBAL_ENTITY_LENGTH;
        //    //    }
        //    //    else
        //    //    {
        //    //        var messageJson = JsonConvert.SerializeObject(message.toJsonObject());
        //    //        if (messageJson != null)
        //    //        {
        //    //            messageLength += UTF8Encoding.UTF8.GetBytes(messageJson).Length;
        //    //        }
        //    //        return messageLength > MAX_GLOBAL_ENTITY_LENGTH;
        //    //    }
        //    //}
        //    //return false;
        //}
        //public bool IsIosExceedLength()
        //{
        //    throw new NotImplementedException();
        //    //if(this.notification!=null){
        //    //    Dictionary<string,object> dict = this.notification.toJsonObject() as Dictionary<string,object>;
        //    //    if (dict != null && dict.ContainsKey(iosPlatformNotification.NOTIFICATION_IOS))
        //    //    {
        //    //        var value = dict[iosPlatformNotification.NOTIFICATION_IOS];
        //    //        var iosJson = JsonConvert.SerializeObject(value);
        //    //        if(iosJson != null){
        //    //            return UTF8Encoding.UTF8.GetBytes(iosJson).Length > MAX_IOS_PAYLOAD_LENGTH;
        //    //        }
        //    //    }
        //    //}
        //    //return false;
        //}
    
    }
}
