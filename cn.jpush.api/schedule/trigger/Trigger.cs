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

        public Periodical periodical;
        public Single single;
        

        public Trigger()
        {
            Periodical periodical = new Periodical();
            this.periodical = periodical;
            Single single = new Single();
            this.single = single;
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


        public void setStart(String start)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(start), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(start), "The start is not valid.");
            this.periodical.start = start;
        }

        public string getStart()
        {
            return this.periodical.start;
        }

        public Trigger setEnd(string end)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(end), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(end), "The end is not valid.");
            this.periodical.end = end;
            return this;
        }

        public string getEnd()
        {

            return this.periodical.end;
        }

        public Trigger setTime(string time)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
            this.periodical.time = time;
            return this;
        }

        public string getTime()
        {
            return this.periodical.time;
        }

        public Trigger setTime_unit(string time_unit)
        {
            this.periodical.time_unit = time_unit;
            return this;
        }

        public string getTime_unit()
        {
            return this.periodical.time_unit;

        }

        public Trigger setFrequency(int frequency)
        {
            Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");
            this.periodical.frequency = frequency;
            return this;
        }

        public int getFrequency()
        {
            
            return this.periodical.frequency;
        }

        public Trigger setPoint(String[] point)
        {
            this.periodical.point = point;
            return this;
        }

        public String[] getPoint()
        {
            return this.periodical.point;
        }

    }
}
