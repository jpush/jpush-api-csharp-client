using Jiguang.JPush.Model;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace Jiguang.JPush
{
    public class ScheduleClient
    {
        private const string BASE_URL = "https://api.jpush.cn";

        /// <summary>
        /// 创建单次定时任务。
        /// </summary>
        /// <param name="name">定时任务名称。</param>
        /// <param name="pushPayload">要推送的推送数据结构体</param>
        /// <param name="triggeringDate">触发日期。类似："2017-08-03 12:00:00"</param>
        public async Task<HttpResponse> CreateSingleScheduleTaskAsync(string name, PushPayload pushPayload, string triggeringTime)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (pushPayload == null)
                throw new ArgumentNullException(nameof(pushPayload));

            if (string.IsNullOrEmpty(triggeringTime))
                throw new ArgumentNullException(nameof(triggeringTime));

            JObject requestJson = new JObject
            {
                ["name"] = name,
                ["enabled"] = true,
                ["push"] = JObject.FromObject(pushPayload),
                ["trigger"] = new JObject
                {
                    ["single"] = new JObject
                    {
                        ["time"] = triggeringTime
                    }
                }
            };

            return await CreateScheduleTaskAsync(requestJson.ToString());
        }

        /// <summary>
        /// 创建会在一段时间内重复执行的定期任务。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pushPayload"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public async Task<HttpResponse> CreatePeriodicalScheduleTaskAsync(string name, PushPayload pushPayload, Trigger trigger)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (pushPayload == null)
                throw new ArgumentNullException(nameof(pushPayload));

            if (trigger == null)
                throw new ArgumentNullException(nameof(trigger));

            JObject requestJson = new JObject
            {
                ["name"] = name,
                ["enabled"] = true,
                ["push"] = JObject.FromObject(pushPayload),
                ["trigger"] = new JObject()
                {
                    ["periodical"] = JObject.FromObject(trigger)
                }
            };

            return await CreateScheduleTaskAsync(requestJson.ToString());
        }

        /// <summary>
        /// 创建定时任务。
        /// </summary>
        /// <param name="json">
        ///     自己构造的请求 json 字符串。
        ///     <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule"/>
        /// </param>
        public async Task<HttpResponse> CreateScheduleTaskAsync(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json));

            var url = BASE_URL + "/v3/schedules";
            HttpContent requestContent = new StringContent(json, Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent);
            string responseContent = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取有效的定时任务列表。
        /// </summary>
        /// <param name="page">
        ///     返回当前请求页的详细的 schedule-task 列表，如未指定 page 则 page 为 1。
        ///     排序规则：创建时间，由 schedule-service 完成。
        ///     如果请求页数大于总页数，则 page 为请求值，schedules 为空。
        ///     每页最多返回 50 个 task，如请求页实际的 task 的个数小于 50，则返回实际数量的 task。
        /// </param>
        /// <returns></returns>
        public async Task<HttpResponse> GetValidScheduleTasksAsync(int page = 1)
        {
            if (page <= 0)
                throw new ArgumentNullException(nameof(page));

            var url = BASE_URL + "/v3/schedules?page=" + page;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url);
            string responseContent = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取指定的定时任务。
        /// </summary>
        /// <param name="scheduleId">定时任务 ID。在创建定时任务时会返回。</param>
        /// <returns></returns>
        public async Task<HttpResponse> GetScheduleTaskAsync(string scheduleId)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(nameof(scheduleId));

            var url = BASE_URL + "/v3/schedules/" + scheduleId;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url);
            string responseContent = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 更新定时任务。
        /// </summary>
        /// <param name="scheduleId">任务 ID</param>
        /// <param name="name">任务名称，为 null 表示不更新。</param>
        /// <param name="enabled">是否可用，为 null 表示不更新。</param>
        /// <param name="triggeringTime">触发时间，类似 "2017-08-03 12:00:00"，为 null 表示不更新。</param>
        /// <param name="pushPayload">推送内容，为 null 表示不更新。</param>
        public async Task<HttpResponse> UpdateSingleScheduleTaskAsync(string scheduleId, string name, bool? enabled,
            string triggeringTime, PushPayload pushPayload)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(scheduleId);

            JObject json = new JObject();

            if (!string.IsNullOrEmpty(name))
                json["name"] = name;

            if (enabled != null)
                json["enabled"] = enabled;

            if (triggeringTime != null)
            {
                json["trigger"] = new JObject
                {
                    ["single"] = new JObject
                    {
                        ["time"] = triggeringTime
                    }
                };
            }

            if (pushPayload != null)
            {
                json["push"] = JObject.FromObject(pushPayload);
            }

            return await UpdateScheduleTaskAsync(scheduleId, json.ToString());
        }

        public async Task<HttpResponse> UpdatePeriodicalScheduleTaskAsync(string scheduleId, string name, bool? enabled,
            Trigger trigger, PushPayload pushPayload)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(scheduleId);

            JObject json = new JObject();

            if (!string.IsNullOrEmpty(name))
                json["name"] = name;

            if (enabled != null)
                json["enabled"] = enabled;

            if (trigger != null)
            {
                json["trigger"] = new JObject
                {
                    ["periodical"] = JObject.FromObject(trigger)
                };
            }

            if (pushPayload != null)
            {
                json["push"] = JObject.FromObject(pushPayload);
            }

            return await UpdateScheduleTaskAsync(scheduleId, json.ToString());
        }

        /// <summary>
        /// https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule_2
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponse> UpdateScheduleTaskAsync(string scheduleId, string json)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(nameof(scheduleId));

            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json));

            var url = BASE_URL + "/v3/schedules/" + scheduleId;
            HttpContent requestContent = new StringContent(json, Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PutAsync(url, requestContent);
            string responseContent = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        public async Task<HttpResponse> DeleteScheduleTaskAsync(string scheduleId)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(nameof(scheduleId));

            var url = BASE_URL + "/v3/schedules/" + scheduleId;
            HttpResponseMessage msg = await JPushClient.HttpClient.DeleteAsync(url);
            string responseContent = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }
    }
}
