using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
    public class WinphonePlatformNotification : PlatformNotification
    {
        private static  String NOTIFICATION_WINPHONE = "winphone";
    
        private static  String TITLE = "title";
        private static  String _OPEN_PAGE = "_open_page";
    
        public  String title;
        [JsonProperty(PropertyName = "_open_page")]
        public  String openPage;
        public WinphonePlatformNotification():base()
        {
            this.title = null;
            this.openPage = null;
        }
        public WinphonePlatformNotification(string alert)
            : base(alert)
        {
            this.title = null;
            this.openPage = null;
        }
        public WinphonePlatformNotification(String alert, String title, String openPage, Dictionary<string,string> extras):
            base(alert,extras) {
        
            this.title = title;
            this.openPage = openPage;
        }
        override public String getPlatformName()
        {
            return NOTIFICATION_WINPHONE;
        }
        override public object toJsonObject()
        {
            Dictionary<string, object> dict = base.toJsonObject() as Dictionary<string, object>;
            if (dict != null)
            {
                dict = new Dictionary<string, object>();
            }
            if (title != null)
            {
                dict.Add(TITLE,title);
            }
            if (openPage != null)
            {
                dict.Add(_OPEN_PAGE, openPage);
            }
            return toJsonObject();
        }
    }
}