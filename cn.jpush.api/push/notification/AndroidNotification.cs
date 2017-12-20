using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace cn.jpush.api.push.notification
{
    public class AndroidNotification : PlatformNotification
    {
        public const string NOTIFICATION_ANDROID = "android";

        private const string TITLE = "title";
        private const string BUILDER_ID = "builder_id";

        [JsonProperty]
        public string title { get; private set; }

        [JsonProperty]
        public int builder_id { get; private set; }

        [JsonProperty]
        public int priority { get; private set; }

        [JsonProperty]
        public string category { get; private set; }

        [JsonProperty]
        public int style { get; private set; }

        [JsonProperty]
        public int alert_type { get; private set; }

        [JsonProperty]
        public string big_text { get; private set; }

        [JsonProperty]
        public string inbox { get; private set; }

        [JsonProperty]
        public string big_pic_path { get; private set; }

        // 华为 only - start

        /// <summary>
        /// 指定开发者想要打开的 Activity，值为 <activity> 节点的 "android:name" 属性值。
        /// </summary>
        [JsonProperty]
        public string url_activity { get; private set; }

        /// <summary>
        /// 指定打开 Activity 的方式，值为 Intent.java 中预定义的 "access flags" 的取值范围。
        /// </summary>
        [JsonProperty]
        public string url_flag { get; private set; }

        /// <summary>
        /// 指定开发者想要打开的 Activity，值为 <activity> -> <intent-filter> -> <action> 节点中的 "android:name" 属性值。
        /// </summary>
        [JsonProperty]
        public string uri_action { get; private set; }

        // 华为 only - end

        public AndroidNotification() : base()
        {
            title = null;
            builder_id = 0;
        }

        public AndroidNotification setTitle(string title)
        {
            this.title = title;
            return this;
        }

        public AndroidNotification setBuilderID(int builder_id)
        {
            this.builder_id = builder_id;
            return this;
        }

        public AndroidNotification setAlert(string alert)
        {
            this.alert = alert;
            return this;
        }

        public AndroidNotification setPriority(int priority)
        {
            this.priority = priority;
            return this;
        }

        public AndroidNotification setCategory(string category)
        {
            this.category = category;
            return this;
        }

        public AndroidNotification setStyle(int style)
        {
            this.style = style;
            return this;
        }

        public AndroidNotification setAlert_type(int alert_type)
        {
            this.alert_type = alert_type;
            return this;
        }

        public AndroidNotification setBig_text(string big_text)
        {
            this.big_text = big_text;
            return this;
        }

        public AndroidNotification setInbox(string inbox)
        {
            this.inbox = inbox;
            return this;
        }

        public AndroidNotification setBig_pic_path(string big_pic_path)
        {
            this.big_pic_path = big_pic_path;
            return this;
        }

        public AndroidNotification setUrlActivity(string url_activity)
        {
            this.url_activity = url_activity;
            return this;
        }

        public AndroidNotification setUrlFlag(string url_flag)
        {
            this.url_flag = url_flag;
            return this;
        }

        public AndroidNotification setUriAction(string uri_action)
        {
            this.uri_action = uri_action;
            return this;
        }

        public AndroidNotification AddExtra(string key, string value)
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

        public AndroidNotification AddExtra(string key, int value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }

        public AndroidNotification AddExtra(string key, bool value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }

        public AndroidNotification AddExtra(string key, object value)
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
