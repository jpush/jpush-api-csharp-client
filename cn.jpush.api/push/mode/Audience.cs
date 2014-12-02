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
            Debug.Assert(target != null && target.valueBuilder != null);
            if (target != null && target.valueBuilder != null)
            {
                this.allAudience = null;
                if (dictionary == null)
                {
                    dictionary = new Dictionary<string, HashSet<string>>();
                }
                if (dictionary.ContainsKey(target.audienceType.ToString()))
                {
                    HashSet<string> origin = dictionary[target.audienceType.ToString()];
                    foreach (var item in target.valueBuilder)
                    {
                        origin.Add(item);
                    }
                }
                else
                {
                    dictionary.Add(target.audienceType.ToString(), target.valueBuilder);
                }
            }
        }

     
        public Dictionary<string, HashSet<string>> dictionary;
        private Audience()
        {
            allAudience = ALL;
            dictionary = null;
        }

        public static Audience all()
        {
           return  new Audience() { allAudience = ALL, dictionary = null }.Check();
        }
        public static Audience s_tag(HashSet<string> values)
        {
           return new Audience().tag(values);
        }
        public static Audience s_tag(params string[] values)
        {
            return new Audience().tag(values);
        }
        public static Audience s_tag_and(HashSet<string> values)
        {
            return new Audience().tag_and(values);
        }
        public static Audience s_tag_and(params string[] values)
        {
            return new Audience().tag_and(values);
        }
        public static Audience s_alias(HashSet<string> values)
        {
            return new Audience().alias(values);
        }
        public static Audience s_alias(params string[] values)
        {
            return new Audience().alias(values);
        }
        public static Audience s_segment(HashSet<string> values)
        {
            return new Audience().segment(values);
        }
        public static Audience s_segment(params string[] values)
        {
            return new Audience().segment(values);
        }
        public static Audience s_registrationId(HashSet<string> values)
        {
            return new Audience().registrationId(values);
        }
        public static Audience s_registrationId(params string[] values)
        {
            return new Audience().registrationId(values);
        }
        public Audience tag(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AudienceTarget target = AudienceTarget.tag(values);
            AddWithAudienceTarget(target);
            return this.Check();
		}
        public Audience tag(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            var valueList = new HashSet<string>(values);
            return tag(valueList);
           
        }
        public Audience tag_and(HashSet<string> values)
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
            return this.Check();
		}
        public Audience tag_and(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            HashSet<string> list = new HashSet<string>(values);
            return tag_and(list);
           
        }
        public Audience alias(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AddWithAudienceTarget( AudienceTarget.alias(values));
            return this.Check();
		}
        public Audience alias(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
           return alias(new HashSet<string>(values));
            
        }
        public Audience segment(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AddWithAudienceTarget(AudienceTarget.segment(values));
            return this.Check();
		}
        public Audience segment(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            return segment(new HashSet<string>(values));
            
        }
        public Audience registrationId(HashSet<string> values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
            AddWithAudienceTarget(AudienceTarget.registrationId(values));
            return this.Check();
		}
        public Audience registrationId(params string[] values)
        {
            if (allAudience != null)
            {
                allAudience = null;
            }
           return registrationId(new HashSet<string>(values));
           
        }
		public bool isAll(){

            return allAudience != null;
		}
        public Audience Check()
        {
            Preconditions.checkArgument(!(isAll() && null != dictionary), "Since all is enabled, any platform should not be set.");
            Preconditions.checkArgument(!(!isAll() && null == dictionary), "No any deviceType is set.");
            return this;
        }
    }
}
