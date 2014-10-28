using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
