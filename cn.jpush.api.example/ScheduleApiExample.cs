using System;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.common.resp;
using cn.jpush.api.schedule;

namespace cn.jpush.api.example
{
    public class ScheduleApiExample
    {
        // 首先运行 DeviceApiExample，它会为设备添加手机号码，标签别名，再运行JPushApiExample, ScheduleApiExample，步骤如下：
        // 1.设置 cn.jpush.api.example 为启动项
        // 2.在 cn.jpush.api.example 项目，右键选择属性，然后选择应用程序，最后在启动对象下拉框中选择 DeviceApiExample
        // 3.按照 2 的步骤设置，运行 JPushApiExample, ScheduleApiExample。

        public static string TITLE = "Test from C# v3 sdk";
        public static string ALERT = "Test from  C# v3 sdk - alert";
        public static string MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        public static string REGISTRATION_ID = "0900e8d85ef";
        public static string TAG = "tag_api";

        public static string NAME = "Test";
        public static bool ENABLED = true;
        public static string TIME = "2016-04-25 14:05:00";
        public static string INVALID_TIME = "2016-03-2514:05:00";
        public static string PUT_TIME = "2016-05-25 14:05:00";
        public static string PUT_NAME = "put_new_name";

        // 创建成功后，填入你的 schedule_id
        public static string PUT_SCHEDULE_ID = "d5ba84b2-f55b-11e5-8496-0021f653c902";
        public static string SCHEDULE_ID = "d5ba84b2-f55b-11e5-8496-0021f653c902";
        public static string START = "2016-03-31 12:30:00";
        public static string END = "2016-05-12 12:30:00";
        public static string TIME_PERIODICAL = "14:00:00";
        public static string INVALID_TIME_PERIODICAL = "4:00:00";
        public static string TIME_UNIT = "WEEK";
        public static int FREQUENCY = 1;
        public static String[] POINT = new String[] { "WED", "FRI" };

        public static int PAGEID = 1;
        public static string schedule_id;
        public static string schedule_id1;
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.all(),
                audience = Audience.all(),
                notification = new Notification().setAlert(ALERT)
            };

            ScheduleClient scheduleclient = new ScheduleClient(app_key, master_secret);
            TriggerPayload triggerConstructor = new TriggerPayload(START, END, TIME_PERIODICAL, TIME_UNIT, FREQUENCY, POINT);
            SchedulePayload schedulepayloadperiodical = new SchedulePayload(NAME, ENABLED, triggerConstructor, pushPayload);

            try
            {
                var result = scheduleclient.sendSchedule(schedulepayloadperiodical);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            SchedulePayload schedulepayloadsingle = new SchedulePayload();
            TriggerPayload triggersingle = new TriggerPayload(TIME);

            schedulepayloadsingle.setPushPayload(pushPayload);
            schedulepayloadsingle.setTrigger(triggersingle);
            schedulepayloadsingle.setName(NAME);
            schedulepayloadsingle.setEnabled(ENABLED);

            try
            {
                var result = scheduleclient.sendSchedule(schedulepayloadsingle);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            // Get schedule.
            try
            {
                var result = scheduleclient.getSchedule(PAGEID);
                Console.WriteLine(result.schedules[0].name);
                Console.WriteLine(result.schedules);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            // Get schedule by Id.
            try
            {
                var result = scheduleclient.getScheduleById(PUT_SCHEDULE_ID);
                Console.WriteLine(result.name);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            // PUT the name.
            SchedulePayload putschedulepayload = new SchedulePayload();
            putschedulepayload.setName(NAME);

            // The default enabled is true, if you want to change it, you have to set it to false.
            try
            {
                var result = scheduleclient.putSchedule(putschedulepayload, SCHEDULE_ID);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var result = scheduleclient.deleteSchedule(SCHEDULE_ID);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it.");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
