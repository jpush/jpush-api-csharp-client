using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.push.audience;
namespace cn.jpush.api.push.mode
{
    public  class Audience : IPushMode
    {
		private const String ALL = "all";
        private bool allAudience;
		private HashSet<AudienceTarget> targets;

        private Audience(bool all, HashSet<AudienceTarget> targets)
        {
            this.allAudience = all;
			this.targets = targets;
		}
        public static Audience all()
        {
			return new Audience(true,null);
		}
		public static Audience tag(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag (values));
			return new Audience (false, audienceTargets);
		}
        public static Audience tag(params string[] values)
        {
            HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget>();
            audienceTargets.Add(AudienceTarget.tag(new HashSet<string>(values)));
            return new Audience(false, audienceTargets);
        }
		public static Audience tag_and(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag_and (values));
			return new Audience (false, audienceTargets);
		}
        public static Audience tag_and(params string[] values)
        {
            HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget>();
            audienceTargets.Add(AudienceTarget.tag_and(new HashSet<string>( values)));
            return new Audience(false, audienceTargets);
        }
		public static Audience alias(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.alias (values));
			return new Audience (false, audienceTargets);
		}
        public static Audience alias(params string[] values)
        {
            HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget>();
            audienceTargets.Add(AudienceTarget.alias(new HashSet<string>(values)));
            return new Audience(false, audienceTargets);
        }
		public static Audience segment(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag (values));
			return new Audience (false, audienceTargets);
		}
        public static Audience segment(params string[] values)
        {
            HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget>();
            audienceTargets.Add(AudienceTarget.tag(new HashSet<string>(values)));
            return new Audience(false, audienceTargets);
        }
		public static Audience registrationId(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag (values));
			return new Audience (false, audienceTargets);
		}
        public static Audience registrationId(params string[] values)
        {
            HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget>();
            audienceTargets.Add(AudienceTarget.tag(new HashSet<string>(values)));
            return new Audience(false, audienceTargets);
        }
		public bool isAll(){
            return allAudience;
		}
        public object toJsonObject()
        {
            if (allAudience)
            {
                return  ALL;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var target in this.targets)
            {
                dictionary.Add(target.audienceType.ToString(), target.values);
            }
            return dictionary;
        }

    }
}
