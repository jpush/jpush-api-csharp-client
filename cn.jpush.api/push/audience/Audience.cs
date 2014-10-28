using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.audience
{
    class Audience
    {
		private const String ALL = "all";
		private bool all;
		private HashSet<AudienceTarget> targets;
		private Audience (bool all,HashSet<AudienceTarget> targets){
			this.all = all;
			this.targets = targets;
		}
		public static Audience allPlatform(){
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
		public string toJSON(){
			if (all) {
				return "\""+ALL+"\"";
			}
			StringBuilder json = StringBuilder ();
			foreach (var target in this.targets) {
				json.Append(target.toJSON()).Append(",");
			}
			if (json.length > 0) {
				json.remove(json.length-1,1);
			}
			json.Append("}");
			json.Append ("{");
			return json.toString ();
		}

    }
}
