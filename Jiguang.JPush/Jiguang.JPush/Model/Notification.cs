using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#notification"/>
    /// </summary>
    public class Notification
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("android")]
        public AndroidNotification Android { get; set; }

        [JsonProperty("ios")]
        public IosNotification IOS { get; set; }

        public Notification()
        {
            Android = new AndroidNotification();
            IOS = new IosNotification();
        }
    }

    public class AndroidNotification
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("builder_id")]
        public int BuilderId { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("style")]
        public int Style { get; set; }

        [JsonProperty("alert_type")]
        public int AlertType { get; set; }

        [JsonProperty("big_text")]
        public string BigText { get; set; }

        //[JsonProperty("inbox", NullValueHandling = NullValueHandling.Ignore)]
        //public Dictionary<string, object> Inbox { get; set; } = null;

        [JsonProperty("big_pic_path")]
        public string BigPicturePath { get; set; }

        //[JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        //public Dictionary<string, object> Extras { get; set; } = null;
    }

    public class IosNotification
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        /// <summary>
        /// Apple 官方定义的 alert payload 结构。
        /// <see ="https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/PayloadKeyReference.html#//apple_ref/doc/uid/TP40008194-CH17-SW5"/>
        /// </summary>
        //[JsonProperty("alert", NullValueHandling = NullValueHandling.Ignore)]
        //public Dictionary<string, object> AlertPayload { get; set; } = null;

        [JsonProperty("sound")]
        public string Sound { get; set; }

        [JsonProperty("badge")]
        public int Badge { get; set; }

        [JsonProperty("content-available")]
        public bool ContentAvailable { get; set; }

        [JsonProperty("mutable-content")]
        public bool MutableContent { get; set; }
    }
}
