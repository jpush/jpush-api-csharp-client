using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace cn.jpush.api.push
{
    public class NotificationParams:MessageParams
    {
        private NotificationContent notyfyMsgContent;

        public NotificationContent NotyfyMsgContent
        {
            get { return notyfyMsgContent; }
            set { notyfyMsgContent = value; }
        }
        
        public class NotificationContent : MessageParams.MsgContent
        {
            private Dictionary<String, Object> extras = new Dictionary<String, Object>();
            private int builderId = 0;

            public int BuilderId
            {
                get { return builderId; }
                set { builderId = value; }
            }

            public Dictionary<String, Object> Extras
            {
                get { return extras; }
                set { extras = value; }
            }           
        }

    }
}
