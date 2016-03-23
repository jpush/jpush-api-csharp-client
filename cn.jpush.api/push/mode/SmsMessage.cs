using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    public class SmsMessage
    {
        public String content { get; set; }
        public int delay_time { get; set; }

        [JsonProperty]
        private Dictionary<string, object> extras { get; set; }

        public SmsMessage()
        {

        }
        private SmsMessage(String content)
        {
            Preconditions.checkArgument(!(content == null), "sms_message Content should be set");
            Preconditions.checkArgument((this.content.Length <= 480), "sms_message's length should be less than 480 bytes");
            this.content = content;
        }
        private SmsMessage(String content, String title)
        {
            Preconditions.checkArgument(!(content == null), "sms_message Content should be set");

            this.content = content;
      
        }
        public static SmsMessage smsContent(string smsContent)
        {
            return new SmsMessage(smsContent).Check();
        }

        public SmsMessage setDelayTime(int delay_time)
        {
            this.delay_time = delay_time;
            return this;
        }

        public SmsMessage Check()
        {
            Preconditions.checkArgument(!(content == null), "sms_message Content should be set");
            Preconditions.checkArgument((this.content.Length<=480), "sms_message's length should be less than 480 bytes");
            return this;
        }
    }
}
