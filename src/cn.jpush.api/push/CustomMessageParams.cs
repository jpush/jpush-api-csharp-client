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

            public Dictionary<String, Object> extras = new Dictionary<string, object>();

            public String title = "";

            public String message = "";
        }


        public override void setMsgContent()
        {
            if (customMsgContent != null)
            {
                MsgContent = JsonTool.ObjectToJson(customMsgContent);
            }
        }
    }
}
