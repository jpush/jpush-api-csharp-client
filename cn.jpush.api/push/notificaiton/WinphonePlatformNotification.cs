using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
    class WinphonePlatformNotification:PlatformNotification
    {
        private static  String NOTIFICATION_WINPHONE = "winphone";
    
        private static  String TITLE = "title";
        private static  String _OPEN_PAGE = "_open_page";
    
        private  String title;
        private  String openPage;

        private WinphonePlatformNotification(String alert, String title, String openPage, Dictionary<string,object> extras):base(alert,extras) {
        
            this.title = title;
            this.openPage = openPage;
        }
        public static WinphonePlatformNotification alert(String alert)
        {
            return new WinphonePlatformNotification(alert, null, null, null);
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