using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    public class Options : IPushMode
    {
        private  const String SENDNO = "sendno";
        private  const String OVERRIDE_MSG_ID = "override_msg_id";
        private  const String TIME_TO_LIVE = "time_to_live";
        private  const String APNS_PRODUCTION = "apns_production";

        public Options(int sendno, long overrideMsgId, long timeToLive, bool apnsProduction) 
        {
            this.sendno = sendno;
            this.overrideMsgId = overrideMsgId;
            this.timeToLive = timeToLive;
            this.apnsProduction = apnsProduction;
        }

        public int sendno;
        public long overrideMsgId;
        public long timeToLive;
        public bool apnsProduction;
       
        public object toJsonObject()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, Object>(4);

            if (sendno > 0) { dictionary.Add(SENDNO, sendno); }
            if (overrideMsgId > 0) { dictionary.Add(OVERRIDE_MSG_ID, timeToLive); }
            if (timeToLive > 0) { dictionary.Add(TIME_TO_LIVE, timeToLive); }

            dictionary.Add(APNS_PRODUCTION, apnsProduction);
            return dictionary;
        }

    }
}
