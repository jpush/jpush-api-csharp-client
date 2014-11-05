using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.push.audience;
using System.Diagnostics;
namespace cn.jpush.api.push.mode
{
    public  class Audience
    {
        private const String ALL = "all";
        private void AddWithAudienceTarget(AudienceTarget target)
        {
            Debug.Assert(target != null && target.values != null);
            if (target != null && target.values != null)
            {
                this.all = null;
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

        public string all;
        public Dictionary<string, HashSet<string>> dictionary;
        public Audience()
        {
            all = ALL;
            dictionary = null;
        }
        public Audience(bool all)
        {
            if (all)
            {
                this.all = ALL;
            }
            dictionary = null;
        }
        public Audience(HashSet<AudienceTarget> targets)
        {
            this.all = null;
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
                this.all = ALL;
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

            AudienceTarget target = AudienceTarget.tag(values);
            AddWithAudienceTarget(target);
           
		}
        public void tag(params string[] values)
        {
            var valueList = new HashSet<string>(values);
            tag(valueList);
        }
		public void tag_and(HashSet<string> values)
        {
            AudienceTarget target = AudienceTarget.tag_and(values);
            this.all = null;
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
            HashSet<string> list = new HashSet<string>();
            tag_and(list);
        }
		public void alias(HashSet<string> values){
           
            AddWithAudienceTarget( AudienceTarget.alias(values));
		}
        public void alias(params string[] values)
        {
            alias(new HashSet<string>(values));
        }
		public void segment(HashSet<string> values)
        {
            AddWithAudienceTarget(AudienceTarget.segment(values));
		}
        public void segment(params string[] values)
        {
            segment(new HashSet<string>(values));
        }
		public void registrationId(HashSet<string> values)
        {
            AddWithAudienceTarget(AudienceTarget.registrationId(values));
		}
        public void registrationId(params string[] values)
        {
            registrationId(new HashSet<string>(values));
        }
		public bool isAll(){
            return all!=null;
		}
    }
}
