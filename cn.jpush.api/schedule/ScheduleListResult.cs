using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return this.schedules;
        }
        public int getTotal_count()
        {
            return this.total_count;
        }
        public int getTotal_pages()
        {
            return this.total_pages;
        }

        public int getPage()
        {
            return this.page;
        }
    }
}
