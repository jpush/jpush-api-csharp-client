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
    
        private  String title;
        private  String openPage;

        private WinphonePlatformNotification(String alert, String title, String openPage, Dictionary<string,string> extras):base(alert,extras) {
        
            this.title = title;
            this.openPage = openPage;
        }
        public new static WinphonePlatformNotification alert(String alert)
        {
            return new WinphonePlatformNotification(alert, null, null, null);
        }
        public WinphonePlatformNotification setTitle(string title)
        {
            this.title = title;
            return this;
        }
        public WinphonePlatformNotification setOpenPage(string openPage)
        {
            this.openPage = openPage;
            return this;
        }
        public WinphonePlatformNotification setAlert(String alert)
        {
            base.alert = alert;
            return this;
        }
        public WinphonePlatformNotification setExras(Dictionary<string, string> extras)
        {
            base.extras = extras;
            return this;
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