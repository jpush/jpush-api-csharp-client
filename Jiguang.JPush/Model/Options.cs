using Newtonsoft.Json;
using System.ComponentModel;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#options"/>
    /// </summary>
    public class Options
    {
        /// <summary>
        /// 用来作为 API 调用标识，API 返回时被原样返回，以方便 API 调用方匹配请求与返回。不能为 0。
        /// </summary>
        [JsonProperty("sendno", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SendNo { get; set; }

        /// <summary>
        /// 推送当前用户不在线时，为该用户保留多长时间的离线消息，以便其上线时再次推送。不修改该值即默认为 86400（1 天），最长 10 天。
        /// 设置为 0 表示不保留离线消息，只有推送当前在线的用户可以收到。
        /// <para>单位：秒。</para>
        /// </summary>
        [JsonProperty("time_to_live", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int TimeToLive { get; set; }

        /// <summary>
        /// 要覆盖的消息 Id。
        /// <para>覆盖功能起作用的时限是：1 天</para>
        /// </summary>
        [JsonProperty("override_msg_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long OverrideMessageId { get; set; }

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

        /// <summary>
        /// 又名缓慢推送，把原本尽可能快的推送速度，降低下来，给定的 n 分钟内，均匀地向这次推送的目标用户推送。最大值为 1400，未设置则不是定速推送。不能为 0。
        /// <para>单位：分钟。</para>
        /// </summary>
        [JsonProperty("big_push_duration", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int BigPushDuration { get; set; }
    }
}
