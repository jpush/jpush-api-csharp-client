using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public String title{get;set;}
        [DefaultValue(0)]
        public int builder_id { get; set; }
        public AndroidPlatformNotification():base()
        {
            this.title = null;
            this.builder_id = 0;
        }
        public AndroidPlatformNotification(string alert)
            : base(alert)
        {
            this.title = null;
            this.builder_id = 0;
        }
        public AndroidPlatformNotification(String alert, String title, int builderId,Dictionary<string,string> extras)
            : base(alert,extras)
        {
            this.title = title;
            this.builder_id = builderId;
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
            if (builder_id > 0) { dictionary.Add(BUILDER_ID, builder_id); }

            return dictionary;
        }
    }
}
