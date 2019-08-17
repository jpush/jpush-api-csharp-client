using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#notification"/>
    /// </summary>
    public class Notification
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("android", NullValueHandling = NullValueHandling.Ignore)]
        public Android Android { get; set; }

        [JsonProperty("ios", NullValueHandling = NullValueHandling.Ignore)]
        public IOS IOS { get; set; }
    }

    public class Android
    {
        /// <summary>
        /// 必填。
        /// </summary>
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("builder_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? BuilderId { get; set; }

        [JsonProperty("channel_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ChannelId { get; set; }

        [JsonProperty("priority", NullValueHandling = NullValueHandling.Ignore)]
        public int? Priority { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public int? Style { get; set; }

        [JsonProperty("alert_type", NullValueHandling = NullValueHandling.Ignore)]
        public int? AlertType { get; set; }

        [JsonProperty("big_text", NullValueHandling = NullValueHandling.Ignore)]
        public string BigText { get; set; }

        [JsonProperty("inbox", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Inbox { get; set; }

        [JsonProperty("big_pic_path", NullValueHandling = NullValueHandling.Ignore)]
        public string BigPicturePath { get; set; }

        [JsonProperty("large_icon", NullValueHandling = NullValueHandling.Ignore)]
        public string LargeIcon { get; set; }

        [JsonProperty("intent", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Indent { get; set; }

        [JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Extras { get; set; }

        /// <summary>
        /// (VIP only)指定开发者想要打开的 Activity，值为 <activity> 节点的 "android:name" 属性值。
        /// </summary>
        [JsonProperty("uri_activity", NullValueHandling = NullValueHandling.Ignore)]
        public string URIActivity { get; set; }

        /// <summary>
        /// (VIP only)指定打开 Activity 的方式，值为 Intent.java 中预定义的 "access flags" 的取值范围。
        /// </summary>
        [JsonProperty("uri_flag", NullValueHandling = NullValueHandling.Ignore)]
        public string URIFlag { get; set; }

        /// <summary>
        /// (VIP only)指定开发者想要打开的 Activity，值为 <activity> -> <intent-filter> -> <action> 节点中的 "android:name" 属性值。
        /// </summary>
        [JsonProperty("uri_action", NullValueHandling = NullValueHandling.Ignore)]
        public string URIAction { get; set; }
    }

    public class IOS
    {
        /// <summary>
        /// 可以是 string，也可以是 Apple 官方定义的 alert payload 结构。
        /// <para><see ="https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/PayloadKeyReference.html#//apple_ref/doc/uid/TP40008194-CH17-SW5"/></para>
        /// </summary>
        [JsonProperty("alert")]
        public object Alert { get; set; }

        [JsonProperty("sound", NullValueHandling = NullValueHandling.Ignore)]
        public string Sound { get; set; }

        /// <summary>
        /// 默认角标 +1。
        /// </summary>
        [JsonProperty("badge")]
        public string Badge { get; set; } = "+1";

        [JsonProperty("content-available", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ContentAvailable { get; set; }

        [JsonProperty("mutable-content", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MutableContent { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Extras { get; set; }

        [JsonProperty("thread-id", NullValueHandling = NullValueHandling.Ignore)]
        public string ThreadId { get; set; }
    }
}
