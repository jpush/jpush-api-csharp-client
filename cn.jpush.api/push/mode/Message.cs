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
        public Dictionary<string, string> extras { get; set; }

        public Message()
        {

        }
        public Message(String msgContent)
        {
            this.title = null;
            this.msg_content = msgContent;
            this.content_type = null;
            this.extras = null;
        }
        public Message(String title, String msgContent, String contentType, Dictionary<string, string> dict)
        {
            this.title = title;
            this.msg_content = msgContent;
            this.content_type = contentType;
            this.extras = dict;
        }
        
    }
}
