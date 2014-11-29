using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notification
{
    public class WinphoneNotification : PlatformNotification
    {
        [JsonProperty]
        private  String title;
        [JsonProperty(PropertyName = "_open_page")]
        public  String openPage;

        public WinphoneNotification():base()
        {
            this.title = null;
            this.openPage = null;
        }
       
        public WinphoneNotification setAlert(String alert)
        {
            this.alert = alert;
            return this;
        }
        public WinphoneNotification setOpenPage(String openPage)
        {
            this.openPage = openPage;
            return this;
        }
        public WinphoneNotification setTitle(String title)
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
            if (value!=null)
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