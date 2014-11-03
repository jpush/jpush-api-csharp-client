using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    public class Message : IPushMode
    {
        private static  String TITLE = "title";
        private static  String MSG_CONTENT = "msg_content";
        private static  String CONTENT_TYPE = "content_type";
        private static  String EXTRAS = "extras";
    
        private  String title;
        private  String msgContent;
        private  String contentType;
        Dictionary<string, object> extras;
      
        private Message(String title, String msgContent, String contentType, Dictionary<string, object> dict)
        {
            this.title = title;
            this.msgContent = msgContent;
            this.contentType = contentType;
            this.extras = dict;
        }
        public static Message content(String msgContent)
        {
            return new Message(null,msgContent,null,null);
        }
        public Message setTitle(string title)
        {
            this.title=title;
            return this;
        }
        public Message setContentType(string contentType)
        {
            this.contentType = contentType;
            return this;
        }
        public Message setExtras(Dictionary<string,object> extras)
        {
            this.extras = extras;
            return this;
        }
        public object toJsonObject()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if(title!=null)
            {
                dict.Add(TITLE,title);
            }
            if (msgContent != null)
            {
                dict.Add(MSG_CONTENT, msgContent);
            }
            if(contentType != null)
            {
                dict.Add(CONTENT_TYPE,contentType);
            }
            if (this.extras != null)
            {
                dict.Add(EXTRAS, this.extras);
            }
            return dict;
        }
    }
}
