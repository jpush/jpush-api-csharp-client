using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
    public class SinglePayload
    {
        /// <summary>
        /// 推送平台。可以为 "android" / "ios" / "all"。
        /// </summary>
        [JsonProperty("platform", DefaultValueHandling = DefaultValueHandling.Include)]
        public object Platform { get; set; } = "all";

        /// <summary>
        /// 推送设备指定。
        /// 如果是调用RegID方式批量单推接口（/v3/push/batch/regid/single），那此处就是指定regid值；
        /// 如果是调用Alias方式批量单推接口（/v3/push/batch/alias/single），那此处就是指定alias值。
        /// </summary>
        [JsonProperty("target", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Target { get; set; }

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
