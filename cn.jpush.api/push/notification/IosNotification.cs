using cn.jpush.api.common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notification
{
    public class IosNotification : PlatformNotification
    {
        public  const String NOTIFICATION_IOS = "ios";
        
        private   const String DEFAULT_SOUND = "";
        private   const String DEFAULT_BADGE = "+1";
    
        private   const String BADGE = "badge";
        private   const String SOUND = "sound";
        private   const String CONTENT_AVAILABLE = "content-available";
        private   const String CATEGORY = "category";
    

        private   const String ALERT_VALID_BADGE = "Badge number should be 0~99999, "
                + "and badgeDisabled property must be false";
        private const String SOUNd_VALID_BADGE = "Sound  should not be null or empty, "
                + "and disableSound property must be false";


        private bool soundDisabled;
        private bool badgeDisabled;

        [JsonProperty]
        public String sound { get;private set; }
        [JsonProperty]
        public String badge { get; private set; }

        [JsonProperty(PropertyName = "content-available")]
        public bool contentAvailable { get; private set; }
        [JsonProperty]
        public String category { get; private set; }

        public IosNotification()
        {
            base.alert = null;
            base.extras = null;
            this.soundDisabled = false;
            this.badgeDisabled = false;
            this.contentAvailable = false;
            this.category = null;
            this.badge = DEFAULT_BADGE;
            this.sound = DEFAULT_SOUND;
        }
       
        public IosNotification disableSound()
        {
            this.soundDisabled = true;
            this.sound = null;
            return this;
        }
        public IosNotification disableBadge()
        {
            this.badgeDisabled = true;
            this.badge = null;
            return this;
        }
        public IosNotification setSound(String sound)
        {
            
            if ((sound ==null) || soundDisabled)
            {
                Console.WriteLine(SOUNd_VALID_BADGE);
                return this;
            }
            this.sound = sound;
            return this;
        }
        public IosNotification setBadge(int badge)
        {
            if (!ServiceHelper.isValidIntBadge(Math.Abs(badge)) || badgeDisabled)
            {
                Console.WriteLine(ALERT_VALID_BADGE);
                return this;
            }
            this.badge = badge.ToString();
            return this;
        }
        public IosNotification autoBadge()
        {
           return  incrBadge(1);

        }
        public IosNotification incrBadge(int badge)
        {
            if (!ServiceHelper.isValidIntBadge(Math.Abs(badge))|| badgeDisabled)
            {
                Console.WriteLine(ALERT_VALID_BADGE);
                return this;
            }
            if (badge >= 0)
            {
                this.badge = "+" + badge;
            }
            else
            {
                this.badge = "" + badge;
            }
            return this;
        }
        public IosNotification setAlert(String alert)
        {
            this.alert = alert;
            return this;
        }
        public IosNotification setContentAvailable(bool contentAvailable)
        {
            this.contentAvailable = contentAvailable;
            return this;
        }
        public IosNotification setCategory(String category)
        {
            this.category = category;
            return this;
        }
        public IosNotification AddExtra(string key, string value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            if (value !=null)
            {
                extras.Add(key, value);
            }
            return this;
        }
        public IosNotification AddExtra(string key, int value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }
        public IosNotification AddExtra(string key, bool value)
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
