using cn.jpush.api.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using cn.jpush.api.schedule;
using cn.jpush.api.push.mode;

namespace cn.jpush.api.push
{

    //"{\"sendno\":\"0\",\"msg_id\":\"1704649583\"}"
    public class MessageResult : BaseResult
    {
        public long msg_id{get;set;}
        public long sendno{ get; set; }

        override public bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;            
            }
            return false;
        }
        public override string ToString()
        {
             return string.Format("sendno:{0},message_id:{1}", sendno, msg_id);
        }
    }

    //{"schedule_id":"some-id-aaaaaaaaaaaaaaaaa","name":"Test"}
    public class ScheduleResult : BaseResult
    {
        public String schedule_id { get; set; }
        public String name { get; set; }

        override public bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return string.Format("sendno:{0},message_id:{1}", schedule_id, name);
        }
    }

    /*
    {
        "total_count":1000,  //&#25968;字 表示总数
        "total_pages":5,      //&#24635;页数。
        "page":4,     //&#24403;前为第四页。
        "schedules":[
            {"schedule_id":"0eac1b80-c2ac-4b69-948b-c65b34b96512","name":"","enabled":...},{}, //&#35814;细信息列表。
        ]
    }
    */

    public class getScheduleResult : BaseResult
    {
        public int total_count { get; set; }
        public int total_pages { get; set; }
        public int page { get; set; }
        public SchedulePayload[] schedules;


        override public bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
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
       public int     code;
       public String  message;
    }
}
