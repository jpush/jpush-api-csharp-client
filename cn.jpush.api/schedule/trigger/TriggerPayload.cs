using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.util;
using Newtonsoft.Json;

namespace cn.jpush.api.schedule
{
    public class TriggerPayload
    {
        [JsonProperty]
        private Periodical periodical;
        [JsonProperty]
        private Single single;

        private JsonSerializerSettings jSetting;
        public TriggerPayload()
        {
            Periodical periodical = new Periodical();
            this.periodical = periodical;
            Single single = new Single();
            this.single = single;
        }

        public TriggerPayload(Single single)
        {
            this.single = single;
            this.periodical = null;
            throw new System.NotImplementedException();
        }


        public TriggerPayload(Periodical periodical)
        {

            this.periodical = periodical;
            this.single = null;
            throw new System.NotImplementedException();
        }

        public TriggerPayload(String time)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(time), "the time is not valid");
            Single single = new Single();
            single.setTime(time);
            this.periodical = null;
            this.single = single;
        }

        public TriggerPayload(String start, String end, String time, String time_unit, int frequency, String[] point)
        {

            Preconditions.checkArgument(!String.IsNullOrEmpty(start), "The start must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(end), "The end must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(!String.IsNullOrEmpty(time_unit), "The time_unit must not be empty.");
            Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");
            Preconditions.checkArgument(StringUtil.IsDateTime(start), "The start is not valid.");
            Preconditions.checkArgument(StringUtil.IsDateTime(end), "The end is not valid.");
            Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
            Preconditions.checkArgument((0 < frequency && frequency < 101), "The frequency must be less than 100.");
            Preconditions.checkArgument(StringUtil.IsTimeunit(time_unit), "The time_unit must be the right format.");
            this.single = null;
            this.periodical = new Periodical(start, end, time, time_unit, frequency, point);

        }


        // "time": "2014-09-17 12:00:00"  //YYYY-MM-DD HH:MM:SS
        public TriggerPayload setSingleTime(string time)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(!StringUtil.IsTime(time), "The time must be the right format.");
            this.single.setTime(time);
            this.periodical = null;
            return this;
        }

        public string getSingleTime()
        {
            return this.single.getTime();
        }



        public TriggerPayload setTime(string time)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
            this.periodical.setTime(time);
            this.single = null;
            return this;
        }

        public string getTime()
        {
            return this.periodical.getTime();
        }

        public void setStart(String start)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(start), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(start), "The start is not valid.");
            this.single = null;
            this.periodical.setStart(start);
        }

        public string getStart()
        {
            return this.periodical.getStart();
        }

        public TriggerPayload setEnd(string end)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(end), "The time must not be empty.");
            Preconditions.checkArgument(StringUtil.IsDateTime(end), "The end is not valid.");
            this.periodical.setEnd(end);
            this.single = null;
            return this;
        }

        public string getEnd()
        {

            return this.periodical.getEnd();
        }

        public TriggerPayload setTime_unit(string time_unit)
        {
            this.periodical.setTime_unit(time_unit);
            this.single = null;
            return this;
        }

        public string getTime_unit()
        {
            return this.periodical.getTime_unit();

        }

        public TriggerPayload setFrequency(int frequency)
        {
            Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");
            Preconditions.checkArgument((0 < frequency && frequency < 101), "The name must be the right format.");
            this.periodical.setFrequency(frequency);
            this.single = null;
            return this;
        }

        public int getFrequency()
        {
            return this.periodical.getFrequency();
        }

        public TriggerPayload setPoint(String[] point)
        {
            this.periodical.setPoint(point);
            this.single = null;
            return this;
        }

        public String[] getPoint()
        {
            return this.periodical.getPoint();
        }


        public string ToJson()
        {
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            return JsonConvert.SerializeObject(this, jSetting);
        }

    }
}

