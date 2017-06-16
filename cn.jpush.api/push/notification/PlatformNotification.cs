using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace cn.jpush.api.push.notification
{
    public abstract class PlatformNotification
    {
        public const string ALERT = "alert";
        private const string EXTRAS = "extras";

        [JsonProperty]
        public object alert { get; protected set; }

        [JsonProperty]
        public Dictionary<String, object> extras { get; protected set; }

        public PlatformNotification()
        {
            alert = null;
            extras = null;
        }
    }
}
