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
        private const long NONE_TIME_TO_LIVE = -1;

        public Options() 
        {
            this.sendno = 0;
            this.override_msg_id = 0;
            this.time_to_live = NONE_TIME_TO_LIVE;
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
        private int _sendno;
        [DefaultValue(0)]
        public int sendno
        {
            get
            {
                return _sendno;
            }
            set
            {
                Preconditions.checkArgument(value >= 0, "sendno should be greater than 0.");
                _sendno = value;
            }
        }
        private long _override_msg_id;
        [DefaultValue(0)]
        public long override_msg_id 
        {
            get
            {
                return _override_msg_id;
            }
            set
            {
                Preconditions.checkArgument(value >= 0, "override_msg_id should be greater than 0.");
                _override_msg_id = value;
            }
        }
        private long _time_to_live;
        [DefaultValue(NONE_TIME_TO_LIVE)]
        public long time_to_live 
        {
            get
            {
                return _time_to_live;
            } 
            set
            {
                Preconditions.checkArgument(value >= NONE_TIME_TO_LIVE, "time_to_live should be greater than 0.");
                _time_to_live = value;
            }
        }
        private long _big_push_duration;
        [DefaultValue(0)]
        public long big_push_duration 
        {
            get
            {
                return _big_push_duration;
            }
            set
            {
                Preconditions.checkArgument(value >= 0, "big_push_duration should be greater than 0.");
                _big_push_duration = value;
            }
        }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool apns_production { get; set; }
    }
}
