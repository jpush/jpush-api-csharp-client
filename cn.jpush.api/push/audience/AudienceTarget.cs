using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.util;

namespace cn.jpush.api.push.audience
{
    public class AudienceTarget
    {
        public AudienceType audienceType{get;private set;}
        public HashSet<string> valueBuilder { get; private set; }
        
        private AudienceTarget(AudienceType audienceType, HashSet<string> values)
        {
            this.audienceType = audienceType;
            this.valueBuilder = values;
        }
        public static AudienceTarget tag(HashSet<string> values)
        {
           return new AudienceTarget(AudienceType.tag,values).Check();
            
        }
        public static AudienceTarget tag_and(HashSet<string> values)
        {
            return new AudienceTarget(AudienceType.tag_and, values).Check();
        }
        public static AudienceTarget alias(HashSet<string> values)
        {
            return new AudienceTarget(AudienceType.alias, values).Check();
            
        }
		public static AudienceTarget segment(HashSet<string> values)
		{
            return new AudienceTarget(AudienceType.segment, values).Check();
           
		}
		public static AudienceTarget registrationId(HashSet<string> values)
		{
           return new AudienceTarget(AudienceType.registration_id, values).Check();
		}
        public AudienceTarget Check()
        {
            Preconditions.checkArgument(null != valueBuilder, "Target values should be set one at least.");
            return this;
        }
    }
}
