using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.schedule
{
    public class Single
    {
        public string time;

        public void setTime(string time)
        {
       
            this.time = time;
        }

        public string getTime()
        {
            return this.time;
        }
    }
}
