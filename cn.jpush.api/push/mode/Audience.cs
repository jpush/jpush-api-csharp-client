using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.push.audience;
using System.Diagnostics;
using cn.jpush.api.util;
namespace cn.jpush.api.push.mode
{
    public  class Audience
    {
        private const String ALL = "all";
        public string allAudience;

        private void AddWithAudienceTarget(AudienceTarget target)
        {
            Debug.Assert(target != null && target.values != null);
            if (target != null && target.values != null)
            {
                this.allAudience = null;
                if (dictionary == null)
                {
                    dictionary = new Dictionary<string, HashSet<string>>();
                }
                if (dictionary.ContainsKey(target.audienceType.ToString()))
                {
                    HashSet<string> origin = dictionary[target.audienceType.ToString()];
                    foreach (var item in target.values)
                    {
                        origin.Add(item);
                    }
                }
                else
                {
                    dictionary.Add(target.audienceType.ToString(), target.values);
                }
            }
        }

     
        public Dictionary<string, HashSet<string>> dictionary;
        public Audience()
        {
            allAudience = ALL;
            dictionary = null;
        }
        public Audience(bool all)
        {
            if (all)
            {
                this.allAudience = ALL;
            }
            dictionary = null;
        }
        public Audience(HashSet<AudienceTarget> targets)
        {
            this.allAudience = null;
            if (targets != null)
            {
                dictionary = new Dictionary<string, HashSet<string>>(targets.Count);
                foreach (var target in targets)
                {
                    dictionary.Add(target.audienceType.ToString(), target.values);
                }
            }
        }
        public Audience(bool all, HashSet<AudienceTarget> targets)
        {
            Debug.Assert(all && targets == null || !all && targets != null);

            if (all)
            {
                this.allAudience = ALL;
            }
            if (targets != null)
            {
                dictionary = new Dictionary<string, HashSet<string>>(targets.Count);
                foreach (var target in targets)
                {
                    dictionary.Add(target.audienceType.ToString(), target.values);
                }
            }
        }

		public void tag(HashSet<string> values){
            if (allAudience != null)
            {
                allAudience = null;
            }
            AudienceTarget target = AudienceTarget.tag(values);
            AddWithAudienceTarget(target);
           
		}
        public void tag(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            var valueList = new HashSet<string>(values);
            tag(valueList);
        }
		public void tag_and(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AudienceTarget target = AudienceTarget.tag_and(values);
            this.allAudience = null;
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, HashSet<string>>();
            }
            if (dictionary.ContainsKey(target.audienceType.ToString()))
            {
                HashSet<string> origin = dictionary[target.audienceType.ToString()];
                foreach (var item in values)
                {
                    origin.Add(item);
                }
            }
            else
            {
                dictionary.Add(target.audienceType.ToString(), values);
            }
		}
        public void tag_and(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            HashSet<string> list = new HashSet<string>();
            tag_and(list);
        }
		public void alias(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AddWithAudienceTarget( AudienceTarget.alias(values));
		}
        public void alias(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            alias(new HashSet<string>(values));
        }
		public void segment(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AddWithAudienceTarget(AudienceTarget.segment(values));
		}
        public void segment(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            segment(new HashSet<string>(values));
        }
		public void registrationId(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AddWithAudienceTarget(AudienceTarget.registrationId(values));
		}
        public void registrationId(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            registrationId(new HashSet<string>(values));
        }
		public bool isAll(){

            return allAudience != null;
		}
        public void setAll(bool all)
        {
            if (all)
            {
                this.allAudience = ALL;
                this.dictionary = null;
            }
            else
            {
                this.allAudience = null;
            }
        }
        public void Check()
        {
            Preconditions.checkArgument(!(isAll() && null != dictionary), "Since all is enabled, any platform should not be set.");
            Preconditions.checkArgument(!(!isAll() && null == dictionary), "No any deviceType is set.");
        }
    }
}
