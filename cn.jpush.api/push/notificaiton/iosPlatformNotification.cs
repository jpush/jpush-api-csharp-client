using cn.jpush.api.common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
    public class iosPlatformNotification : PlatformNotification
    {
        public  const String NOTIFICATION_IOS = "ios";
        
        private   const String DEFAULT_SOUND = "";
        private   const String DEFAULT_BADGE = "+1";
    
        private   const String BADGE = "badge";
        private   const String SOUND = "sound";
        private   const String CONTENT_AVAILABLE = "content-available";
        private   const String CATEGORY = "category";
    
        private   const String ALERT_VALID_BADGE = "Badge number should be 0~99999, "
                + "and can be prefixed with + to add, - to minus";
        private const String SOUNd_VALID_BADGE = "Sound  should not be null or empty, "
                + "and disableSound property must be false";


        private bool soundDisabled;
        private bool badgeDisabled;

        public String sound{get;set;}
        public String badge{get;set;}

        [JsonProperty(PropertyName = "content-available")]
        public bool contentAvailable{get;set;}

        public String category{get;set;}
        public iosPlatformNotification()
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
        public iosPlatformNotification(string alert)
        {
            this.alert = alert;
            this.soundDisabled = false;
            this.badgeDisabled = false;
            this.contentAvailable = false;
            this.category = null;
            this.badge = null;
            this.sound = null;
            this.badge = null != badge ? badge : DEFAULT_BADGE;
            this.sound = null != sound ? sound : DEFAULT_SOUND;
        }

        public iosPlatformNotification(String alert, 
                                        String sound=null, 
                                        String badge=null,
                                        bool contentAvailable=false,
                                        bool soundDisabled=false,
                                        bool badgeDisabled=false,
                                        String category=null,
                                        Dictionary<String, string> extras=null)
            : base(alert,extras)
        {
            this.category = category;
            this.contentAvailable = contentAvailable;
            this.soundDisabled = soundDisabled;
            this.badgeDisabled = badgeDisabled;
            if (!badgeDisabled)
            {
                this.badge = null != badge ? badge : DEFAULT_BADGE;
            }
            if (!soundDisabled)
            {
                this.sound = null != sound ? sound : DEFAULT_SOUND;
            }
        }

        public void disableSound()
        {
            this.soundDisabled = true;
            this.sound = null;
        }
        public void disableBadge()
        {
            this.badgeDisabled = true;
            this.badge = null;
        }
        public void setSound(String sound)
        {
            Debug.Assert(!string.IsNullOrEmpty(sound) && !soundDisabled);
            if (!soundDisabled && !string.IsNullOrEmpty(sound))
            {
                 this.sound = sound;
            }
            else
            {
                Console.WriteLine(SOUNd_VALID_BADGE);
            }
        }
        public void setBadge(int badge)
        {
            if (ServiceHelper.isValidIntBadge(Math.Abs(badge)))
            {
                Console.WriteLine(ALERT_VALID_BADGE);
            }
            this.badge = badge.ToString();
        }
        public void autoBadge()
        {
             incrBadge(1);
        }
        public void incrBadge(int badge)
        {
            if (ServiceHelper.isValidIntBadge(Math.Abs(badge)))
            {
                Console.WriteLine(ALERT_VALID_BADGE);
            }
            if (badge >= 0)
            {
                this.badge = "+" + badge;
            }
            else
            {
                this.badge = "" + badge;
            }
        }

        override  public String getPlatformName()
        {
            return NOTIFICATION_IOS;
        }
        override  public object toJsonObject()
        {
            Dictionary<string,object> dict = base.toJsonObject() as Dictionary<string, object>;
            if (dict == null)
            {
                dict = new Dictionary<string, object>();
            }

            if (!badgeDisabled)
            {
                if (null != badge)
                {
                    dict.Add(BADGE,this.badge);
                }
                else
                {
                    dict.Add(BADGE, DEFAULT_BADGE);
                }
            }
            if (!soundDisabled)
            {
                if (null != sound)
                {
                    dict.Add(SOUND, sound);
                }
                else
                {
                    dict.Add(SOUND, DEFAULT_SOUND);
                }
            }
            if (contentAvailable)
            {
                dict.Add(CONTENT_AVAILABLE, 1);
            }
            if (null != category)
            {
                dict.Add(CATEGORY,category);
            }
            return dict;
        }
    }
}
