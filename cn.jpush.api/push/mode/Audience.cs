using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.push.audience;
namespace cn.jpush.api.push.mode
{
    class Audience : IPushMode
    {
		private const String ALL = "all";
		private bool all;
		private HashSet<AudienceTarget> targets;
		private Audience (bool all,HashSet<AudienceTarget> targets){
			this.all = all;
			this.targets = targets;
		}
        public static Audience allAudience()
        {
			return new Audience(true,null);
		}
		public static Audience tag(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag (values));
			return new Audience (false, audienceTargets);
		}
		public static Audience tag_and(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag_and (values));
			return new Audience (false, audienceTargets);
		}
		public static Audience alias(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.alias (values));
			return new Audience (false, audienceTargets);
		}
		public static Audience segment(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag (values));
			return new Audience (false, audienceTargets);
		}
		public static Audience registrationId(HashSet<string> values){
			HashSet<AudienceTarget> audienceTargets = new HashSet<AudienceTarget> ();
			audienceTargets.Add (AudienceTarget.tag (values));
			return new Audience (false, audienceTargets);
		}
		public bool isAll(){
			return all;
		}
        public object toJsonObject()
        {
            if (all)
            {
                return  ALL;
            }
            List<string> jsonList = new List<string>();
            foreach (var target in this.targets)
            {
                jsonList.Add(target.ToString());
            }
            return jsonList;
        }

    }
}
