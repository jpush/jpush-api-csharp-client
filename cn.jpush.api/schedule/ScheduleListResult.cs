using System;
using cn.jpush.api.common;

namespace cn.jpush.api.schedule
{
    class ScheduleListResult : BaseResult
    {
        public int total_count;
        public int total_pages;
        public int page;
        public SchedulePayload[] schedules;

        public override bool isResultOK()
        {
            throw new NotImplementedException();
        }

        public SchedulePayload[] getSchedules()
        {
            return schedules;
        }

        public int getTotal_count()
        {
            return total_count;
        }

        public int getTotal_pages()
        {
            return total_pages;
        }

        public int getPage()
        {
            return page;
        }
    }
}
