using Newtonsoft.Json;
using System.ComponentModel;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#options"/>
    /// </summary>
    public class Options
    {
        [JsonProperty("sendno", NullValueHandling = NullValueHandling.Ignore)]
        public int? SendNo { get; set; }

        [JsonProperty("time_to_live", NullValueHandling = NullValueHandling.Ignore)]
        public int? TimeToLive { get; set; }

        [JsonProperty("override_msg_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OverrideMessageId { get; set; }

        /// <summary>
        /// iOS 推送是否为生产环境。默认为 false - 开发环境。
        /// <para>true: 生产环境；false: 开发环境。</para>
        /// </summary>
        [JsonProperty("apns_production", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsApnsProduction { get; set; } = false;

        /// <summary>
        /// 更新 iOS 通知的标识符。
        /// <para>APNs 新通知如果匹配到当前通知中心有相同 apns-collapse-id 字段的通知，则会用新通知内容来更新它，并使其置于通知中心首位。collapse id 长度不可超过 64 bytes。</para>
        /// </summary>
        [JsonProperty("apns_collapse_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ApnsCollapseId { get; set; }

        [JsonProperty("big_push_duration", NullValueHandling = NullValueHandling.Ignore)]
        public int? BigPushDuration { get; set; }
    }
}
