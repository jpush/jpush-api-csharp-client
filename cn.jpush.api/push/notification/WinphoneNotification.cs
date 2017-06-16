using Newtonsoft.Json;
using System.Collections.Generic;

namespace cn.jpush.api.push.notification
{
    public class WinphoneNotification : PlatformNotification
    {
        [JsonProperty]
        private string title;

        [JsonProperty(PropertyName = "_open_page")]
        public string openPage;

        public WinphoneNotification() : base()
        {
            title = null;
            openPage = null;
        }

        public WinphoneNotification setAlert(string alert)
        {
            this.alert = alert;
            return this;
        }
        
        public WinphoneNotification setOpenPage(string openPage)
        {
            this.openPage = openPage;
            return this;
        }

        public WinphoneNotification setTitle(string title)
        {
            this.title = title;
            return this;
        }

        public WinphoneNotification AddExtra(string key, string value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            if (value != null)
            {
                extras.Add(key, value);
            }
            return this;
        }

        public WinphoneNotification AddExtra(string key, int value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }

        public WinphoneNotification AddExtra(string key, bool value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }
    }
}