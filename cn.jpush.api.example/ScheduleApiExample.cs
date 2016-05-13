using System;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.common.resp;
using cn.jpush.api.schedule;

namespace cn.jpush.api.example
{
    public class ScheduleApiExample
    {
        //run the DeviceApiExample first,it will add mobile,tags,alias to the device:
        //首先运行DeviceApiExample，它会为设备添加手机号码，标签别名，再运行JPushApiExample,ScheduleApiExample，步骤如下：
        //1.设置cn.jpush.api.example为启动项
        //2.在cn.jpush.api.example项目，右键选择属性，然后选择应用程序，最后在启动对象下拉框中选择DeviceApiExample
        //3.按照2的步骤设置，运行JPushApiExample,ScheduleApiExample.

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
        //创建成功后，填入你的schedule_id
        public static String PUT_SCHEDULE_ID = "d5ba84b2-f55b-11e5-8496-0021f653c902";
        public static String SCHEDULE_ID = "d5ba84b2-f55b-11e5-8496-0021f653c902";
        public static String START = "2016-03-31 12:30:00";
        public static String END = "2016-05-12 12:30:00";
        public static String TIME_PERIODICAL = "14:00:00";
        public static String INVALID_TIME_PERIODICAL = "4:00:00";
        public static String TIME_UNIT = "WEEK";
        public static int FREQUENCY = 1;
        public static String[] POINT =new String[]{ "WED", "FRI"};

        public static int PAGEID = 1;
        public static String schedule_id ;
        public static String schedule_id1;
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "9349ad7c90292a603c512e92";

        public static void Main(string[] args) {
            //init a pushpayload
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            pushPayload.notification = new Notification().setAlert(ALERT);

            ScheduleClient scheduleclient = new ScheduleClient(app_key, master_secret);

            //init a TriggerPayload
            TriggerPayload triggerConstructor = new TriggerPayload(START, END, TIME_PERIODICAL, TIME_UNIT, FREQUENCY, POINT);
            //init a SchedulePayload
            SchedulePayload schedulepayloadperiodical = new SchedulePayload(NAME, ENABLED,triggerConstructor,pushPayload);

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


            //get schedule
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


            //get schedule by id 
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

            //PUT the name
            SchedulePayload putschedulepayload = new SchedulePayload();
            
            putschedulepayload.setName(NAME);

            //the default enabled is true,if you want to change it,you have to set it to false
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


            //delete Schedule
            try
            {
                // add the right  
                var result = scheduleclient.deleteSchedule(SCHEDULE_ID);
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
        }

    }
}
