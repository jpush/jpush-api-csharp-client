using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace cn.jpush.api.push.mode
{
    public class Message
    {
        public string title { get; set; }
        public string msg_content { get; set; }
        public string content_type { get; set; }

        [JsonProperty]
        private Dictionary<string, object> extras { get; set; }

        private Message()
        {
        }

        private Message(string msgContent)
        {
            Preconditions.checkArgument(!(msgContent == null), "msgContent should be set");

            title = null;
            msg_content = msgContent;
            content_type = null;
            extras = null;
        }

        private Message(string msgContent, string title, string contentType)
        {
            Preconditions.checkArgument(!(msgContent == null), "msgContent should be set");

            this.title = title;
            msg_content = msgContent;
            content_type = contentType;
        }

        public static Message content(string msgContent)
        {
            return new Message(msgContent).Check();
        }

        public Message setTitle(string title)
        {
            this.title = title;
            return this;
        }

        public Message setContentType(string ContentType)
        {
            content_type = ContentType;
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
            Preconditions.checkArgument(!(msg_content == null), "msgContent should be set");
            return this;
        }
    }
}
