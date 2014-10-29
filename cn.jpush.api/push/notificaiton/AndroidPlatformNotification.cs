using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
    class AndroidPlatformNotification:PlatformNotification
    {
        public const String NOTIFICATION_ANDROID = "android";
    
        private const String TITLE = "title";
        private const String BUILDER_ID = "builder_id";
    
        private  String title;
        private  int builderId;

        private AndroidPlatformNotification(String alert, String title, int builderId,Dictionary<string,object> extras)
            : base(alert,extras)
        {
            this.title = title;
            this.builderId = builderId;
        }
        public static AndroidPlatformNotification alert(string alert)
        {
            return new AndroidPlatformNotification(alert, null, 0, null);
        }
        public void setTitle(String title)
        {
            this.title = title;
        }
        public void setBuilderID(int builderId)
        {
            this.builderId = builderId;
        }
        override public string getPlatformName()
        {
            return NOTIFICATION_ANDROID;
        }
        override  public object toJsonObject()
        {
            Dictionary<string,object> dictionary =  base.toJsonObject() as Dictionary<string,object>;
            if (dictionary == null) { dictionary = new Dictionary<string, object>(); }

            if (title != null) { dictionary.Add(TITLE, title); }
            if (builderId > 0) { dictionary.Add(BUILDER_ID, builderId); }

            return dictionary;
        }
    }
}
