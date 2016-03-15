using cn.jpush.api.util;
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
        public string start;
        public string end;
        public string time;
        public string time_unit;
        public int frequency;
        public HashSet<string> point;

        public Periodical setStart(string start)
        {
            this.start = start;
            return this;
        }

        public string getStart()
        {
            return this.start;
        }

        public Periodical setEnd(string end)
        {
            this.end = end;
            return this;
        }

        public string getEnd()
        {

            return this.end;
        }

        public Periodical setTime(string time)
        {
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
        }

        public Periodical setFrequency(int frequency)
        {
            this.frequency = frequency;
            return this;
        }

        public int getFrequency()
        {
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
