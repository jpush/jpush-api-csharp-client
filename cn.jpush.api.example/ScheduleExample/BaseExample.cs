using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.common.resp;
using cn.jpush.api.schedule;


namespace cn.jpush.api.example.Schedule
{
    public class BaseExample
    {
        public static String TITLE = "Test from C# v3 sdk";
        public static String ALERT = "Test from  C# v3 sdk - alert";
        public static String MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        public static String REGISTRATION_ID = "0900e8d85ef";
        public static String TAG = "tag_api";

        public static String NAME = "Test";
        public static bool ENABLED = true;
        public static String TIME = "2016-04-25 14:05:00";
        public static String INVALID_TIME = "2016-03-2514:05:00";
        public static String PUT_TIME = "2016-05-25 14:05:00";
        public static String PUT_NAME = "put_new_name";

        public static String PUT_SCHEDULE_ID = "5bf75dde-0de9-11e6-b65a-0021f653c902";
        public static String SCHEDULE_ID = "5bf75dde-0de9-11e6-b65a-0021f653c902";
        public static String START = "2016-03-31 12:30:00";
        public static String END = "2016-05-12 12:30:00";
        public static String TIME_PERIODICAL = "14:00:00";
        public static String INVALID_TIME_PERIODICAL = "4:00:00";
        public static String TIME_UNIT = "WEEK";
        public static int FREQUENCY = 1;
        public static String[] POINT = new String[] { "WED", "FRI" };

        public static int PAGEID = 1;
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "9349ad7c90292a603c512e92";
    }
}
