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
        [JsonProperty("delay_time")]
        public int DelayTime { get; set; }
        [JsonProperty("temp_id")]
        public long TempId { get; set; }
        [JsonProperty("temp_para", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> TempPara { get; set; }
    }
}
