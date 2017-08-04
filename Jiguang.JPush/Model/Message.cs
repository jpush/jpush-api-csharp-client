using Newtonsoft.Json;
using System.Collections;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// 自定义消息。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#message"/>
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 消息内容本身（必填）。
        /// </summary>
        [JsonProperty("msg_content")]
        public string Content { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("extras")]
        public IDictionary Extras { get; set; }
    }
}
