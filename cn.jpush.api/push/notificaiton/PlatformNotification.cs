using cn.jpush.api.push.mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
    abstract class PlatformNotification:IPushMode
    {
        public abstract string getPlatformName();
        public const String ALERT = "alert";
        private const String EXTRAS = "extras";
        private String alert;
        private Dictionary<String, object> extras;

        public PlatformNotification(String alert, Dictionary<string, object> extras)
        {
            this.alert = alert;
            this.extras = extras;
        }
        public void setAlert(String alert)
        {
            this.alert = alert;
        }
        public void setExras(Dictionary<string, object> extras)
        {
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
