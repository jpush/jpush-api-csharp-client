using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// 短信补充。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#sms_message"/>
    /// </summary>
    public class SmsMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("delay_time", DefaultValueHandling = DefaultValueHandling.Include)]
        public int DelayTime { get; set; }
    }
}
