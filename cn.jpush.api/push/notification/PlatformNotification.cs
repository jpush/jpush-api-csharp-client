using cn.jpush.api.push.mode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notification
{
    public abstract class PlatformNotification
    {
        
        public const String ALERT = "alert";
        private const String EXTRAS = "extras";
        [JsonProperty]
        public String alert{get;protected set;}
        [JsonProperty]
        public Dictionary<String, object> extras { get; protected set; }

        public PlatformNotification()
        {
            this.alert = null;
            this.extras = null;
        }
      

    }
}
