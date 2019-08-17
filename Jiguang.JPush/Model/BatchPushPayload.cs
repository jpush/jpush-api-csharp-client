using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
    public class BatchPushPayload
    {
        [JsonProperty("pushlist")]
        public Dictionary<string, SinglePayload> Pushlist { get; set; }

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
