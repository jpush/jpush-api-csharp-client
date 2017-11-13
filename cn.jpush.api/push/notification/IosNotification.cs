using cn.jpush.api.common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace cn.jpush.api.push.notification
{
    public class IosNotification : PlatformNotification
    {
        public const string NOTIFICATION_IOS = "ios";

        private const string DEFAULT_SOUND = "";
        private const string DEFAULT_BADGE = "+1";

        private const string BADGE = "badge";
        private const string SOUND = "sound";
        private const string CONTENT_AVAILABLE = "content-available";
        private const string CATEGORY = "category";

        private const string ALERT_VALID_BADGE = "Badge number should be 0~99999, "
                + "and badgeDisabled property must be false";
        private const string SOUND_VALID_BADGE = "Sound should not be null or empty, "
                + "and disableSound property must be false";

        private bool soundDisabled;
        private bool badgeDisabled;

        [JsonProperty]
        public string sound { get; private set; }

        [JsonProperty]
        public string badge { get; private set; }

        [JsonProperty(PropertyName = "content-available")]
        public bool contentAvailable { get; private set; }

        [JsonProperty(PropertyName = "mutable-content")]
        public bool mutableContent { get; private set; }

        [JsonProperty]
        public string category { get; private set; }

        public IosNotification()
        {
            alert = null;
            extras = null;
            soundDisabled = false;
            badgeDisabled = false;
            contentAvailable = false;
            category = null;
            badge = DEFAULT_BADGE;
            sound = DEFAULT_SOUND;
        }

        public IosNotification disableSound()
        {
            soundDisabled = true;
            sound = null;
            return this;
        }

        public IosNotification disableBadge()
        {
            badgeDisabled = true;
            badge = null;
            return this;
        }

        public IosNotification setSound(string sound)
        {
            if ((sound == null) || soundDisabled)
            {
                Console.WriteLine(SOUND_VALID_BADGE);
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
            return incrBadge(1);
        }

        public IosNotification incrBadge(int badge)
        {
            if (!ServiceHelper.isValidIntBadge(Math.Abs(badge)) || badgeDisabled)
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

        public IosNotification setAlert(object alert)
        {
            this.alert = alert;
            return this;
        }

        public IosNotification setContentAvailable(bool contentAvailable)
        {
            this.contentAvailable = contentAvailable;
            return this;
        }

        public IosNotification setMutableContent(bool mutableContent)
        {
            this.mutableContent = mutableContent;
            return this;
        }

        public IosNotification setCategory(string category)
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
            if (value != null)
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

        public IosNotification AddExtra(string key, object value)
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