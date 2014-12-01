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
       
        private Message()
        {

        }
        private Message(String msgContent)
        {
            Preconditions.checkArgument(!(msgContent==null), "msgContent should be set");

            this.title = null;
            this.msg_content = msgContent;
            this.content_type = null;
            this.extras = null;
        }
        private Message(String msgContent, String title, String contentType)
        {
            Preconditions.checkArgument(!(msgContent == null), "msgContent should be set");

            this.title = title;
            this.msg_content = msgContent;
            this.content_type = contentType;
        }
        public static Message content(string msgContent)
        {
            return new Message(msgContent).Check();
        }
        public Message setTitle(String title)
        {
            this.title = title;
            return this;
        }
        public Message setContentType(String ContentType)
        {
            this.content_type = ContentType;
            return this;
        }
        public Message AddExtras(string key, string value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            if (value != null)
            {
                extras.Add(key, value);
            }
            return this;
        }
        public Message AddExtras(string key, int value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }
        public Message AddExtras(string key, bool value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;

        }
        public Message Check()
        {
            Preconditions.checkArgument(!(msg_content==null), "msgContent should be set");
            return this;
        }
    }
}
