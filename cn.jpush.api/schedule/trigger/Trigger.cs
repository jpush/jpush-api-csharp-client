using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.schedule
{
    public class Trigger
    {
        private const String TRIGGER = "trigger";
        private const String SINGLE = "single";

        public Periodical periodical { get; set; }
        public Single single { get; set; }

        public Trigger()
        {
            periodical = null;
            single = null;        
        }

        public Trigger(Periodical periodical)
        {
            this.periodical = periodical;
            single = null;
            throw new System.NotImplementedException();
        }

        public Trigger(Single single)
        {
            this.single = single;
            this.periodical = null;
            throw new System.NotImplementedException();
        }

    }
}
