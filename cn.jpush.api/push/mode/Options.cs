using cn.jpush.api.util;
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
        private  const String SENDNO = "sendno";
        private  const String OVERRIDE_MSG_ID = "override_msg_id";
        private  const String TIME_TO_LIVE = "time_to_live";
        private  const String APNS_PRODUCTION = "apns_production";

        public Options(int sendno, long overrideMsgId, long timeToLive, bool apnsProduction=true) 
        {
            this.sendno = sendno;
            this.override_msg_id = overrideMsgId;
            this.time_to_live = timeToLive;
            this.apns_production = apnsProduction;
        }
        [DefaultValue(0)]
        public int sendno{get;set;}
        [DefaultValue(0)]
        public long override_msg_id { get; set; }
        [DefaultValue(0)]
        public long time_to_live { get; set; }
        public bool apns_production { get; set; }
       
        public object toJsonObject()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, Object>(4);

            if (sendno > 0) { dictionary.Add(SENDNO, sendno); }
            if (override_msg_id > 0) { dictionary.Add(OVERRIDE_MSG_ID, override_msg_id); }
            if (time_to_live > 0) { dictionary.Add(TIME_TO_LIVE, time_to_live); }

            dictionary.Add(APNS_PRODUCTION, apns_production);
            return dictionary;
        }

    }
}
