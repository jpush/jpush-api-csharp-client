using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    public class Options
    {
        public Options() 
        {
            this.sendno = 0;
            this.override_msg_id = 0;
            this.time_to_live = 0;
            this.big_push_duration = 0;
            this.apns_production = false;
        }
        public Options(int sendno, 
                       long overrideMsgId, 
                       long timeToLive, 
                       int bigPushDuration, 
                       bool apnsProduction=false) 
        {
            this.sendno = sendno;
            this.override_msg_id = overrideMsgId;
            this.time_to_live = timeToLive;
            this.big_push_duration = bigPushDuration;
            this.apns_production = apnsProduction;
        }
        [DefaultValue(0)]
        public int sendno{get;set;}
        [DefaultValue(0)]
        public long override_msg_id { get; set; }
        [DefaultValue(0)]
        public long time_to_live { get; set; }
        [DefaultValue(0)]
        public long big_push_duration { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool apns_production { get; set; }
    }
}
