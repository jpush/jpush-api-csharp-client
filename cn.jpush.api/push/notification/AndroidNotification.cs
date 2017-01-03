using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notification
{
   public class AndroidNotification:PlatformNotification
    {
        public const String NOTIFICATION_ANDROID = "android";
    
        private const String TITLE = "title";
        private const String BUILDER_ID = "builder_id";

        [JsonProperty]
        public String title{get;private set;}
        [JsonProperty]
        public int builder_id { get; private set; }
        [JsonProperty]
        public int priority { get; private set; }
        [JsonProperty]
        public String category { get; private set; }
        [JsonProperty]
        public int style { get; private set; }
        [JsonProperty]
        public String big_text { get; private set; }
        [JsonProperty]
        public String inbox { get; private set; }
        [JsonProperty]
        public String big_pic_path { get; private set; }


        public AndroidNotification():base()
        {
            this.title = null;
            this.builder_id = 0;
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
        public AndroidNotification setAlert(String alert)
        {
            this.alert = alert;
            return this;
        }

        public AndroidNotification setPriority(int priority)
        {
            this.priority = priority;
            return this;
        }
        public AndroidNotification setCategory(String category)
        {
            this.category = category;
            return this;
        }

        public AndroidNotification setStyle(int style)
        {
            this.style = style;
            return this;
        }
        public AndroidNotification setBig_text(String big_text)
        {
            this.big_text = big_text;
            return this;
        }

        public AndroidNotification setInbox(String inbox)
        {
            this.inbox = inbox;
            return this;
        }
        public AndroidNotification setBig_pic_patht(String big_pic_path)
        {
            this.big_pic_path = big_pic_path;
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
       
        
    }
}
