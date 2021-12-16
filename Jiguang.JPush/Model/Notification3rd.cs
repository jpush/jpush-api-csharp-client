using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#notification_3rd"/>
    /// </summary>
    public class Notification3rd
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        /// <summary>
        /// 必填。
        /// </summary>
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty("channel_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ChannelId { get; set; }

        [JsonProperty("uri_activity", NullValueHandling = NullValueHandling.Ignore)]
        public string UriActivity { get; set; }

        [JsonProperty("uri_action", NullValueHandling = NullValueHandling.Ignore)]
        public string UriAction { get; set; }

        [JsonProperty("badge_add_num", NullValueHandling = NullValueHandling.Ignore)]
        public string BadgeAddNum { get; set; }

        [JsonProperty("badge_class", NullValueHandling = NullValueHandling.Ignore)]
        public string BadgeClass { get; set; }

        [JsonProperty("sound", NullValueHandling = NullValueHandling.Ignore)]
        public string Sound { get; set; }

        [JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Extras { get; set; }
    }
}