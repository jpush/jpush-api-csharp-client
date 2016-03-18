using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using cn.jpush.api.util;

namespace cn.jpush.api.schedule
{
    public class Single
    {
        public string time;

        public void setTime(string time)
        {
            Preconditions.checkArgument(StringUtil.IsDateTime(time),"the time is not valid");
            this.time = time;
        }

        public string getTime()
        {
            return this.time;
        }
    }
}
