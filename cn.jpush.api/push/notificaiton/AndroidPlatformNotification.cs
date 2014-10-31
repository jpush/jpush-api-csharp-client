using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
   public class AndroidPlatformNotification:PlatformNotification
    {
        public const String NOTIFICATION_ANDROID = "android";
    
        private const String TITLE = "title";
        private const String BUILDER_ID = "builder_id";
    
        private  String title;
        private  int builderId;

        private AndroidPlatformNotification(String alert, String title, int builderId,Dictionary<string,string> extras)
            : base(alert,extras)
        {
            this.title = title;
            this.builderId = builderId;
        }
        public new static AndroidPlatformNotification alert(string alert)
        {
            return new AndroidPlatformNotification(alert, null, 0, null);
        }
        public AndroidPlatformNotification setTitle(String title)
        {
            
            this.title = title;
            return this;
        }
        public AndroidPlatformNotification setBuilderID(int builderId)
        {
            this.builderId = builderId;
            return this;
        }
        public AndroidPlatformNotification setAlert(String alert)
        {
            base.alert = alert;
            return this;
        }
        public AndroidPlatformNotification setExras(Dictionary<string, string> extras)
        {
            base.extras = extras;
            return this;
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
