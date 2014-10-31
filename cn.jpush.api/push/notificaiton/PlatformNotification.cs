using cn.jpush.api.push.mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
    public abstract class PlatformNotification:IPushMode
    {
        public abstract string getPlatformName();
        public const String ALERT = "alert";
        private const String EXTRAS = "extras";

        protected String alert;
        protected Dictionary<String, string> extras;

        public PlatformNotification(String alert, Dictionary<string,string> extras)
        {
            this.alert = alert;
            this.extras = extras;
        }
     
        virtual  public object toJsonObject()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (alert != null)
            {
                dictionary.Add(ALERT,alert);
            }
            if (extras != null)
            {
                dictionary.Add(EXTRAS, extras);
            }
            return dictionary;
        }

    }
}
