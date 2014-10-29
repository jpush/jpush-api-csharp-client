using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.util;

namespace cn.jpush.api.push.audience
{
    class AudienceTarget
    {
        private AudienceType audienceType;
        private HashSet<string> values;

        private AudienceTarget(AudienceType audienceType, HashSet<string> values)
        {
            this.audienceType = audienceType;
            this.values = values;
        }
        public static AudienceTarget tag(HashSet<string> values)
        {
            return new AudienceTarget(AudienceType.tag,values);
        }
        public static AudienceTarget tag_and(HashSet<string> values)
        {
            return new AudienceTarget(AudienceType.tag_and, values);
        }
        public static AudienceTarget alias(HashSet<string> values)
        {
            return new AudienceTarget(AudienceType.alias, values);
        }
		public static AudienceTarget segment(HashSet<string> values)
		{
			return new AudienceTarget(AudienceType.segment, values);
		}
		public static AudienceTarget registrationId(HashSet<string> values)
		{
			return new AudienceTarget(AudienceType.registration_id, values);
		}
		public string toJSON(){

			StringBuilder json = new StringBuilder ();
			json.Append (this.audienceType.ToString());
			json.Append (":");
			json.Append (JsonTool.ObjectToJson (this.values));
            return json.ToString();
		}
    }
}
