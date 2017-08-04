using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
    public class DevicePayload
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, object> Tags { get; set;}

        private string GetJson()
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
