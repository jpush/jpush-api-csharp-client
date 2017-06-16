using cn.jpush.api.util;
using Newtonsoft.Json;
using System;

namespace cn.jpush.api.schedule
{
    public class Periodical
    {
        [JsonProperty]
        private String start;

        [JsonProperty]
        private String end;

        [JsonProperty]
        private String time;

        [JsonProperty]
        private String time_unit;

        [JsonProperty]
        private int frequency;

        [JsonProperty]
        private String[] point;

        //init the periodical by Constructor function
        //通过构造函数来构造Periodical类，并传递参数。
        public Periodical(String start, String end, String time, String time_unit, int frequency, String[] point)
        {
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

        //init the periodical 
        public Periodical()
        {
            start = null;
            end = null;
            time = null;
            time_unit = null;
            frequency = 0;
            point = null;
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
            return start;
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
            return end;
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
            return time;
        }

        public Periodical setTime_unit(string time_unit)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(time_unit), "The time_unit must not be empty.");
            Preconditions.checkArgument(StringUtil.IsTimeunit(time_unit), "The time_unit must be the right format.");
            this.time_unit = time_unit;
            return this;
        }

        public string getTime_unit()
        {
            return time_unit;
        }

        public Periodical setFrequency(int frequency)
        {
            Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");
            Preconditions.checkArgument((0 < frequency && frequency < 101), "The frequency must be less than 100.");
            this.frequency = frequency;
            return this;
        }

        public int getFrequency()
        {
            return frequency;
        }

        public Periodical setPoint(String[] point)
        {
            this.point = point;
            return this;
        }

        public String[] getPoint()
        {
            return point;
        }
    }
}
