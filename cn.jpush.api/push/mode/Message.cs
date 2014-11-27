using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    public class Message
    {
        public String title{get;set;}
        public String msg_content { get; set; }
        public String content_type { get; set; }
        [JsonProperty]
        private Dictionary<string, object> extras { get; set; } 
       
        public Message()
        {

        }
        public Message(String msgContent)
        {
            Preconditions.checkArgument(!(string.IsNullOrEmpty(msgContent)), "msgContent should be set");

            this.title = null;
            this.msg_content = msgContent;
            this.content_type = null;
            this.extras = null;
        }
        public Message(String msgContent, String title, String contentType)
        {
            Preconditions.checkArgument(!(string.IsNullOrEmpty(msgContent)), "msgContent should be set");

            this.title = title;
            this.msg_content = msgContent;
            this.content_type = contentType;
        }
        public void AddExtras(string key,string value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
        }
        public void AddExtras(string key, int value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
        }
        public void AddExtras(string key, bool value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
        }
        public void Check()
        {
            Preconditions.checkArgument(!(string.IsNullOrEmpty(msg_content)), "msgContent should be set");
        }
    }
}
