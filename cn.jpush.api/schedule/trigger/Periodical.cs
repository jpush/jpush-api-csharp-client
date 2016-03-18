using cn.jpush.api.util;
using cn.jpush.api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.schedule
{
    public class Periodical
    {
        public String start;
        public String end;
        public String time;
        public String time_unit;
        public int frequency;
        public HashSet<string> point;

        public Periodical(String start, String end, String time, String time_unit, int frequency, HashSet<string> point) {

            Preconditions.checkArgument(!String.IsNullOrEmpty(start), "The time must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(end), "The time must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(time_unit), "The time_unit must not be empty.");
            Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");

            Preconditions.checkArgument(StringUtil.IsDateTime(start), "The start is not valid.");
            Preconditions.checkArgument(StringUtil.IsDateTime(end), "The end is not valid.");
            Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
            this.start = start;
            this.end = end;
            this.time = time;
            this.time_unit = time_unit;
            this.frequency = frequency;
            this.point = point;
        }

        public Periodical() {

        }

        public Periodical setStart(String start)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(start), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(start), "The start is not valid.");
            this.start = start;
            return this;
        }

        public string getStart()
        {
            return this.start;
        }

        public Periodical setEnd(string end)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(end), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(end), "The end is not valid.");
            this.end = end;
            return this;
        }

        public string getEnd()
        {

            return this.end;
        }

        public Periodical setTime(string time)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
            this.time = time;
            return this;
        }

        public string getTime()
        {
            return this.time;
        }

        public Periodical setTime_unit(string time_unit)
        {
            this.time_unit = time_unit;
            return this;
        }

        public string getTime_unit()
        {
            return this.time_unit;
            Preconditions.checkArgument(!String.IsNullOrEmpty(time_unit), "The time_unit must not be empty.");
        }

        public Periodical setFrequency(int frequency)
        {
            this.frequency = frequency;
            return this;
        }

        public int getFrequency()
        {
            Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");
            return this.frequency;
        }

        public Periodical setPoint(HashSet<string> point)
        {
            this.point = point;
            return this;
        }

        public HashSet<string> getPoint()
        {
            return this.point;
        }

        
    }
}
