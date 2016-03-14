using cn.jpush.api.common;
using cn.jpush.api.push.notification;
using cn.jpush.api.push.mode;
using cn.jpush.api.util;
using cn.jpush.api.schedule;
using cn.jpush.api.push;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.schedule
{
    public class ScheduleClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.jpush.cn";
        private const String PUSH_PATH = "/v3/schedules";
        
        private String appKey;
        private String masterSecret;
        public ScheduleClient(String appKey, String masterSecret)
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ScheduleResult sendSchedule(SchedulePayload schedulepayload)
        { 
            Preconditions.checkArgument(schedulepayload != null, "schedulepayload should not be empty");
            schedulepayload.Check();
            String schedulepayloadJson = schedulepayload.ToJson();
            Console.WriteLine(schedulepayloadJson);
            return sendSchedule(schedulepayloadJson);
        }
        public ScheduleResult sendSchedule(string schedulepayload)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(schedulepayload), "schedulepayload should not be empty");
            Console.WriteLine(schedulepayload);
            String url = HOST_NAME_SSL;
            url += PUSH_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), schedulepayload);
            ScheduleResult messResult = new ScheduleResult();
            messResult.ResponseResult = result;

            ScheduleSuccess scheduleSuccess = JsonConvert.DeserializeObject<ScheduleSuccess>(result.responseContent);
            messResult.schedule_id =scheduleSuccess.schedule_id;
            messResult.name = scheduleSuccess.name;

            return messResult;
        }
        private String Authorization()
        {

            Debug.Assert(!string.IsNullOrEmpty(this.appKey));
            Debug.Assert(!string.IsNullOrEmpty(this.masterSecret));

            String origin = this.appKey + ":" + this.masterSecret;
            return Base64.getBase64Encode(origin);
        }


    }




}