using cn.jpush.api.common;
using System.Net;
using cn.jpush.api.schedule;

namespace cn.jpush.api.push
{
    //"{\"sendno\":\"0\",\"msg_id\":\"1704649583\"}"
    public class MessageResult : BaseResult
    {
        public long msg_id { get; set; }
        public long sendno { get; set; }

        override public bool isResultOK()
        {
            return Equals(ResponseResult.responseCode, HttpStatusCode.OK) ? true : false;
        }

        public override string ToString()
        {
            return string.Format("sendno:{0},message_id:{1}", sendno, msg_id);
        }
    }

    //{"schedule_id":"some-id-aaaaaaaaaaaaaaaaa","name":"Test"}
    public class ScheduleResult : BaseResult
    {
        public string schedule_id { get; set; }
        public string name { get; set; }

        override public bool isResultOK()
        {
            return Equals(ResponseResult.responseCode, HttpStatusCode.OK) ? true : false;
        }

        public override string ToString()
        {
            return string.Format("sendno:{0},message_id:{1}", schedule_id, name);
        }
    }

    public class getScheduleResult : BaseResult
    {
        public int total_count { get; set; }
        public int total_pages { get; set; }
        public int page { get; set; }
        public SchedulePayload[] schedules;

        override public bool isResultOK()
        {
            return Equals(ResponseResult.responseCode, HttpStatusCode.OK) ? true : false;
        }

        public override string ToString()
        {
            return string.Format("total_count:{0},total_pages:{1},page:{2}", total_count, total_pages, page);
        }
    }

    public class JpushSuccess
    {
        public string sendno;
        public string msg_id;
    }

    public class ScheduleSuccess
    {
        public string schedule_id;
        public string name;
    }

    public class getScheduleSuccess
    {
        public int total_count;
        public int total_pages;
        public int page;
        public SchedulePayload[] schedules;
    }

    public class JpushError
    {
        public JpushErrorObject error;
        public long msg_id;
    }

    public class JpushErrorObject
    {
        public int code;
        public string message;
    }
}
