using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    class Message : IPushMode
    {
        private static  String TITLE = "title";
        private static  String MSG_CONTENT = "msg_content";
        private static  String CONTENT_TYPE = "content_type";
        private static  String EXTRAS = "extras";
    
        private  String title;
        private  String msgContent;
        private  String contentType;
        Dictionary<string, object> dict;
        private Message(String title, String msgContent, String contentType, Dictionary<string, object> dict)
        {
            this.title = title;
            this.msgContent = msgContent;
            this.contentType = contentType;
            this.dict = dict;
        }
        public static Message content(String msgContent)
        {
            return new Message(null,msgContent,null,null);
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
            if(EXTRAS!=null)
            {
                dict.Add(EXTRAS, this.dict);
            }
            return dict;
        }


    }
}
