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
        public static String TIME = "2016-03-11 14:05:00";

        public static String START = "2016-03-11 12:30:00";
        public static String END = "2016-03-12 12:30:00";
        public static String TIME_PERIODICAL = "14:05:00";
        public static String TIME_UNIT = "day";
        public static int FREQUENCY = 1;
        public static HashSet<string> POINT =new HashSet<string>() {};

        public static String app_key = "997f28c1cea5a9f17d82079a";
        public static String master_secret = "47d264a3c02a6a5a4a256a45";

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
            Trigger trigger = new Trigger();
            trigger.single = single;
            single.setTime(TIME);

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

            Console.WriteLine("periodical:");
            Console.WriteLine(periodical);

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


        }


    }
}
