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
        [JsonProperty("platform", DefaultValueHandling = DefaultValueHandling.Include)]
        public object Platform { get; set; } = "all";

        [JsonProperty("audience", DefaultValueHandling = DefaultValueHandling.Include)]
        public object Audience { get; set; } = "all";

        [JsonProperty("notification", NullValueHandling = NullValueHandling.Ignore)]
        public Notification Notification { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public Message Message { get; set; }

        [JsonProperty("sms_message", NullValueHandling = NullValueHandling.Ignore)]
        public SmsMessage SMSMessage { get; set; }

        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Include)]
        public Options Options { get; set; } = new Options
        {
            IsApnsProduction = false
        };

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
