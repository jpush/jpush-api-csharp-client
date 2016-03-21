using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using cn.jpush.api.common;
using cn.jpush.api.util;
using System.IO;
using Newtonsoft.Json;

namespace cn.jpush.api.schedule.trigger
{
    class TriggerPayload
    {
        private Type type;
        private String start;
        private String end;
        private String time;
        private TimeUnit time_unit;
        private int frequency;
        private String[] point;

        public enum Type
        {
            single, periodical
        }

        
        private TriggerPayload(String time)
        {
            this.type = Type.single;
            this.time = time;
        }

        private TriggerPayload(String start, String end, String time, TimeUnit time_unit, int frequency, String[] point)
        {
            this.type = Type.periodical;
            this.start = start;
            this.end = end;
            this.time = time;
            this.time_unit = time_unit;
            this.frequency = frequency;
            this.point = point;
        }


        public static Builder newBuilder()
        {
            return new Builder();
        }

        public class Builder
        {

            private String start;
            private String end;
            private String time;
            private TimeUnit time_unit;
            private int frequency;
            private String[] point;

            public Builder setSingleTime(String time)
            {
                this.time = time;
                return this;
            }

            public Builder setPeriodTime(String start, String end, String time)
            {
                this.start = start;
                this.end = end;
                this.time = time;
                return this;
            }

            public Builder setTimeFrequency(TimeUnit time_unit, int frequency, String[] point)
            {
                this.time_unit = time_unit;
                this.frequency = frequency;
                this.point = point;
                return this;
            }

            public TriggerPayload buildSingle()
            {
                Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
                Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
                return new TriggerPayload(time);
            }

            public TriggerPayload buildPeriodical()
            {
                Preconditions.checkArgument(!String.IsNullOrEmpty(start), "The time must not be empty.");
                Preconditions.checkArgument(!String.IsNullOrEmpty(end), "The time must not be empty.");
                Preconditions.checkArgument(!String.IsNullOrEmpty(time), "The time must not be empty.");
                Preconditions.checkArgument(!String.IsNullOrEmpty(time_unit.ToString()), "The time_unit must not be empty.");
                Preconditions.checkArgument(StringUtil.IsNumber(frequency.ToString()), "The frequency must be number.");
                Preconditions.checkArgument(StringUtil.IsDateTime(start), "The start is not valid.");
                Preconditions.checkArgument(StringUtil.IsDateTime(end), "The end is not valid.");
                Preconditions.checkArgument(StringUtil.IsTime(time), "The time must be the right format.");
                Preconditions.checkArgument(isTimeUnitOk(time_unit), "The time unit must be DAY, WEEK or MONTH.");
                return new TriggerPayload(start, end, time, time_unit, frequency, point);
            }

            private Boolean isTimeUnitOk(TimeUnit timeUnit)
            {
                switch (timeUnit)
                {
                    case TimeUnit.DAY:
                    case TimeUnit.WEEK:
                    case TimeUnit.MONTH:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
