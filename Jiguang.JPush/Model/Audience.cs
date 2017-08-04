using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// 推送目标。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#audience"/>
    /// </summary>
    public class Audience
    {
        /// <summary>
        /// 多个标签之间取并集（OR）。
        /// 每次最多推送 20 个。
        /// </summary>
        [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tag { get; set; }

        /// <summary>
        /// 多个标签之间取交集（AND）。
        /// 每次最多推送 20 个。
        /// </summary>
        [JsonProperty("tag_and", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TagAnd { get; set; }

        /// <summary>
        /// 多个标签之间，先取并集，再对结果取补集。
        /// 每次最多推送 20 个。
        /// </summary>
        [JsonProperty("tag_not", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TagNot { get; set; }

        /// <summary>
        /// 多个别名之间取并集（OR）。
        /// 每次最多同时推送 1000 个。
        /// </summary>
        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Alias { get; set; }

        /// <summary>
        /// 多个 registration id 之间取并集（OR）。
        /// 每次最多同时推送 1000 个。
        /// </summary>
        [JsonProperty("registration_id", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RegistrationId { get; set; }

        /// <summary>
        /// 在页面创建的用户分群 ID。
        /// 目前一次只能推送一个。
        /// </summary>
        [JsonProperty("segment", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Segment { get; set; }

        /// <summary>
        /// 在页面创建的 A/B 测试 ID。
        /// 目前一次只能推送一个。
        /// </summary>
        [JsonProperty("abtest", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Abtest { get; set; }
    }
}
