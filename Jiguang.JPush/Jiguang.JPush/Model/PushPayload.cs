using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    public class PushPayload
    {
        [JsonProperty("cid")]
        public string CId { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; } = "all";

        [JsonProperty("audience")]
        public object Audience { get; set; }

        [JsonProperty("notification")]
        public Notification Notification { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("sms_message")]
        public SmsMessage SMSMessage { get; set; }

        [JsonProperty("options")]
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
