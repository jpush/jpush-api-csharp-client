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

        public SmsMessage() {

        }

        public SmsMessage(String content, int delay_time)
        {
            Preconditions.checkArgument(!(content == null), "sms_message Content should be set");
            Preconditions.checkArgument((this.content.Length <= 480), "sms_message's length should be less than 480 bytes");
            this.Check();
            this.setContent(content);
            this.setDelayTime(delay_time);
        }

        public SmsMessage setDelayTime(int delay_time)
        {
            this.delay_time = delay_time;
            return this;
        }

        public SmsMessage setContent(string content)
        {
            this.content = content;
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
