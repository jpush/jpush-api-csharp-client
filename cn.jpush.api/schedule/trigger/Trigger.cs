using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.util;

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

        public Trigger(Single single)
        {
            this.single = single;
            this.periodical = null;
            throw new System.NotImplementedException();
        }


        public Trigger(Periodical periodical)
        {

            this.periodical = periodical;
            single = null;
            throw new System.NotImplementedException();
        }

        public Trigger(String time)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(time), "the time is not valid");
            Single single = new Single();
            single.setTime(time);
            this.single = single;
            this.single.time = single.getTime();
        }

        public Trigger(String start, String end, String time, String time_unit, int frequency, String[] point)
        {

            Preconditions.checkArgument(!String.IsNullOrEmpty(start), "The time must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(end), "The time must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(time_unit), "The time_unit must not be empty.");
            Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");
            Preconditions.checkArgument(StringUtil.IsDateTime(start), "The start is not valid.");
            Preconditions.checkArgument(StringUtil.IsDateTime(end), "The end is not valid.");
            Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
            this.periodical = new Periodical(start, end, time, time_unit, frequency, point);
        }

    }
}
