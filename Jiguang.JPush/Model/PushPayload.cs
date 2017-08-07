using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    public class PushPayload
    {
        [JsonProperty("cid", NullValueHandling = NullValueHandling.Ignore)]
        public string CId { get; set; }

        /// <summary>
        /// 推送平台。可以为 "android" / "ios" / "all"。
        /// </summary>
        [JsonProperty("platform")]
        public string Platform { get; set; } = "all";

        [JsonProperty("audience")]
        public object Audience { get; set; } = "all";

        [JsonProperty("notification")]
        public Notification Notification { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public Message Message { get; set; }

        [JsonProperty("sms_message", NullValueHandling = NullValueHandling.Ignore)]
        public SmsMessage SMSMessage { get; set; }

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Options Options { get; set; }

        internal string GetJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
        }

        public override string ToString()
        {
            return GetJson();
        }
    }
}
