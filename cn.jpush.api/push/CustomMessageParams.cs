using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push
{
    public class CustomMessageParams:MessageParams
    {
        private CustomMessageContent customMsgContent = new CustomMessageContent();

        public CustomMessageContent CustomMsgContent
        {
            get { return customMsgContent; }
            set { customMsgContent = value; }
        }   

        public class CustomMessageContent
        {
            public String content_type;

            public String extras = "";

            public String title = "";

            public String message = "";
        }


        public override void setMsgContent()
        {
            if (customMsgContent != null)
            {
                MsgContent = "{\"message\":\"" + customMsgContent.message + "\"";
                
                if (!String.IsNullOrEmpty(customMsgContent.content_type))
                {
                    MsgContent += ",\"content_type\":\"" + customMsgContent.content_type + "\"";
                }
                if (!String.IsNullOrEmpty(customMsgContent.extras))
                {
                    MsgContent += ",\"extras\":" + customMsgContent.extras;
                }
                if (!String.IsNullOrEmpty(customMsgContent.title))
                {
                    MsgContent += ",\"title\":\"" + customMsgContent.title + "\"";
                }
                MsgContent += "}";
                //MsgContent = JsonTool.ObjectToJson(customMsgContent);
            }
        }
    }
}
