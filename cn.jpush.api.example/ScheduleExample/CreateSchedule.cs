using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.common.resp;
using cn.jpush.api.schedule;
namespace cn.jpush.api.example.Schedule
{
    public class CreateSchedule : BaseExample
    {
        public static void Main(string[] args)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            pushPayload.notification = new Notification().setAlert(ALERT);
            ScheduleClient scheduleclient = new ScheduleClient(app_key, master_secret);

            //init a TriggerPayload
            TriggerPayload triggerConstructor = new TriggerPayload(START, END, TIME_PERIODICAL, TIME_UNIT, FREQUENCY, POINT);
            //init a SchedulePayload
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
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
