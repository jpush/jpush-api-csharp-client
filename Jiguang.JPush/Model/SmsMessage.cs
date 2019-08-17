using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// 短信补充。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#sms_message"/>
    /// </summary>
    public class SmsMessage
    {
        [JsonProperty("delay_time", DefaultValueHandling = DefaultValueHandling.Include)]
        public int DelayTime { get; set; }

        [JsonProperty("signid", NullValueHandling = NullValueHandling.Ignore)]
        public int Signid { get; set; }

        [JsonProperty("temp_id", DefaultValueHandling = DefaultValueHandling.Include)]
        public long TempId { get; set; }

        [JsonProperty("temp_para", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> TempPara { get; set; }

        [JsonProperty("active_filter", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ActiveFilter { get; set; }
    }
}
