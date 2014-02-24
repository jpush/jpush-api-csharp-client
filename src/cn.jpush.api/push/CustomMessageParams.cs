using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push
{
    public class CustomMessageParams:MessageParams
    {
        private CustomMessageContent customMsgContent;

        public CustomMessageContent CustomMsgContent
        {
            get { return customMsgContent; }
            set { customMsgContent = value; }
        }   

        public class CustomMessageContent : MessageParams.MsgContent 
        {
            private String contentType;

            private Dictionary<String, Object> extras = new Dictionary<String, Object>();

            public String ContentType
            {
                get { return contentType; }
                set { contentType = value; }
            }

            public Dictionary<String, Object> Extras
            {
                get { return extras; }
                set { extras = value; }
            }

        }

    }
}
