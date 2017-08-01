using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#options"/>
    /// </summary>
    public class Options
    {
        [JsonProperty("sendno")]
        public int SendNo { get; set; }

        [JsonProperty("time_to_live")]
        public int TimeToLive { get; set; }

        [JsonProperty("override_msg_id")]
        public long OverrideMessageId { get; set; }

        [JsonProperty("apns_production")]
        public bool IsApnsProduction { get; set; }

        [JsonProperty("apns_collapse_id")]
        public string ApnsCollapseId { get; set; }

        [JsonProperty("big_push_duration")]
        public int BigPushDuration { get; set; }
    }
}
