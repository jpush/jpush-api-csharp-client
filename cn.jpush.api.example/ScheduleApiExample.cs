using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.report;
using cn.jpush.api.common;
using cn.jpush.api.util;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using cn.jpush.api.common.resp;
using cn.jpush.api.example;
using cn.jpush.api.schedule;

namespace cn.jpush.api.example
{
    public class ScheduleApiExample
    {

        public static String TITLE = "Test from C# v3 sdk";
        public static String ALERT = "Test from  C# v3 sdk - alert";
        public static String MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        public static String REGISTRATION_ID = "0900e8d85ef";
        public static String TAG = "tag_api";

        public static String NAME = "Test";
        public static bool ENABLED = true;
        public static String TIME = "2016-03-25 14:05:00";

        public static String PUT_TIME = "2016-05-25 14:05:00";
        public static String PUT_NAME = "put_new_name";
        //创建成功后，填入你的schedule_id
        public static String PUT_SCHEDULE_ID = "f0b61b2e-e682-11e5-beab-0021f652c102";

        
        public static String START = "2016-03-11 12:30:00";
        public static String END = "2016-05-12 12:30:00";
        public static String TIME_PERIODICAL = "14:00:00";
        public static String TIME_UNIT = "day";
        public static int FREQUENCY = 1;
        public static HashSet<string> POINT =new HashSet<string>() {};

        public static String PAGEID = "1";
        public static String schedule_id ;

        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "8aae478411e89f7682ed5af6";

        public JPushApiExample jpushExample;

        static void Main(string[] args) { 
            JPushApiExample jpushExample = new JPushApiExample();
            PushPayload pushPayload = JPushApiExample.PushObject_All_All_Alert();
            Console.WriteLine(pushPayload);

            ScheduleClient scheduleclient = new ScheduleClient(app_key, master_secret);

            SchedulePayload schedulepayload = new SchedulePayload();
            Name name = new Name();
            name.setName(NAME);

            Enabled enabled = new Enabled();
            enabled.setEnable(ENABLED);

            schedule.Single single = new schedule.Single();
            single.setTime(TIME);

            Trigger trigger = new Trigger();
            trigger.single = single;

            

            schedulepayload.name = name.getName();
            schedulepayload.enabled = enabled.getEnable();
            schedulepayload.trigger = trigger;
            schedulepayload.push = pushPayload;

            try
            {
                var result = scheduleclient.sendSchedule(schedulepayload);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
                //保留这里获取的schedule_id，作为后面删除schedule的参数，如果不想删除这个可以删掉这一行，另外设置一个schedule_id
                schedule_id = result.schedule_id;

            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }


            SchedulePayload schedulepayloadperiodical = new SchedulePayload();
            Name nameperiodical = new Name();
            nameperiodical.setName(NAME);
            Enabled enabledperiodical = new Enabled();
            enabledperiodical.setEnable(ENABLED);
            Periodical periodical = new Periodical();
            periodical.setStart(START);
            periodical.setEnd(END);
            periodical.setTime(TIME_PERIODICAL);
            periodical.setTime_unit(TIME_UNIT);
            periodical.setFrequency(FREQUENCY);
            periodical.setPoint(POINT);

            Trigger triggerperiodical = new Trigger();
            triggerperiodical.periodical = periodical;

            schedulepayloadperiodical.name = name.getName();
            schedulepayloadperiodical.enabled = enabled.getEnable();
            schedulepayloadperiodical.trigger = triggerperiodical;
            schedulepayloadperiodical.push = pushPayload;

            try
            {
                var result = scheduleclient.sendSchedule(schedulepayloadperiodical);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            //get 
            try
            {
                var result = scheduleclient.getSchedule(PAGEID);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);

                Console.WriteLine(result.schedules[0].name);
                Console.WriteLine(result.schedules);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            //put the name

            SchedulePayload putschedulepayload = new SchedulePayload();
            Name putname = new Name();
            putname.setName(PUT_NAME);
            putschedulepayload.name = putname.getName();
            try
            {
                var result = scheduleclient.putSchedule(putschedulepayload,PUT_SCHEDULE_ID);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                //删除的是第一次创建的schedule_id，如果要保留第一次创建的，请重新传入schedule_id
                var result = scheduleclient.deleteSchedule(schedule_id);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }


        }


    }
}
