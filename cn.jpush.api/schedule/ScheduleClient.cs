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
        private const String DELETE_PATH = "/";
        private const String PUT_PATH = "/";
        private const String GET_PATH = "?page=";
        private JsonSerializerSettings jSetting;
        private String appKey;
        private String masterSecret;

        public ScheduleClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        //POST https://api.jpush.cn/v3/schedules
        //创建一个新的定时任务。
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
            messResult.schedule_id = scheduleSuccess.schedule_id;
            messResult.name = scheduleSuccess.name;
            return messResult;
        }


        //GET /v3/schedules?page=
        //获取有效的schedule列表
        public getScheduleResult getSchedule(int pageid)
        {
            Preconditions.checkArgument(pageid > 0, "page should more than 0.");
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            Console.WriteLine(pageid);
            String url = HOST_NAME_SSL;
            url += PUSH_PATH;
            url += GET_PATH;
            url += pageid.ToString();
            ResponseWrapper result = sendGet(url, Authorization(), pageid.ToString());
            getScheduleResult messResult = new getScheduleResult();
            messResult.ResponseResult = result;

            ScheduleListResult scheduleListResult = JsonConvert.DeserializeObject<ScheduleListResult>(result.responseContent, jSetting);
            
            messResult.page = scheduleListResult.page;
            messResult.total_pages = scheduleListResult.total_pages;
            messResult.total_count = scheduleListResult.total_count;
            messResult.schedules = scheduleListResult.schedules;
            return messResult;
        }

        //获取指定的定时任务
        //GET https://api.jpush.cn/v3/schedules/{schedule_id}

        public SchedulePayload getScheduleById(String id)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(id), "id should be set.");
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            String url = HOST_NAME_SSL;
            url += PUSH_PATH;
            url += "/";
            url += id;
            ResponseWrapper result = sendGet(url, Authorization(),id);

            String schedule = result.responseContent;
            SchedulePayload schedulepayload = JsonConvert.DeserializeObject<SchedulePayload>(schedule, jSetting);
            return schedulepayload;
        }


        //PUT  https://api.jpush.cn/v3/schedules/{schedule_id}
        //修改指定的Schedule
        public ScheduleResult putSchedule(SchedulePayload schedulepayload,String schedule_id)
        {
            Preconditions.checkArgument(schedulepayload != null, "schedulepayload should not be empty");
            Preconditions.checkArgument(schedule_id != null, "schedule_id should not be empty");

            if (schedulepayload.push.audience == null || schedulepayload.push.platform == null) {
                schedulepayload.push = null;
            }
            
            if(schedulepayload.trigger.getTime()==null && schedulepayload.trigger.getSingleTime() == null)
            {
                schedulepayload.trigger = null;
            }

            String schedulepayloadJson = schedulepayload.ToJson();
            Console.WriteLine(schedulepayloadJson);
            return putSchedule(schedulepayloadJson,schedule_id);
        }

        public ScheduleResult putSchedule(string schedulepayload, String schedule_id)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(schedulepayload), "schedulepayload should not be empty");
            Console.WriteLine(schedulepayload);
            String url = HOST_NAME_SSL;
            url += PUSH_PATH;
            url += PUT_PATH;
            url += schedule_id;
            ResponseWrapper result = sendPut(url, Authorization(), schedulepayload);
            ScheduleResult messResult = new ScheduleResult();
            messResult.ResponseResult = result;

            ScheduleSuccess scheduleSuccess = JsonConvert.DeserializeObject<ScheduleSuccess>(result.responseContent);
            messResult.schedule_id = scheduleSuccess.schedule_id;
            messResult.name = scheduleSuccess.name;

            return messResult;
        }

        //DELETE https://api.jpush.cn/v3/schedules/{schedule_id} 
        //删除指定的Schedule任务
        public ScheduleResult deleteSchedule(string schedule_id)
        {
            Preconditions.checkArgument(schedule_id != null, "schedule_id should not be empty");
            Console.WriteLine(schedule_id);
            String url = HOST_NAME_SSL;
            url += PUSH_PATH;
            url += DELETE_PATH;
            url += schedule_id;
            ResponseWrapper result = sendDelete(url, Authorization(), schedule_id);
            ScheduleResult messResult = new ScheduleResult();
            messResult.ResponseResult = result;

            ScheduleSuccess scheduleSuccess = JsonConvert.DeserializeObject<ScheduleSuccess>(result.responseContent);
            //messResult.schedule_id = scheduleSuccess.schedule_id;
            //messResult.name = scheduleSuccess.name;

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