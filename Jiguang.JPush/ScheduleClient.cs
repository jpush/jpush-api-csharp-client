using Jiguang.JPush.Model;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Jiguang.JPush
{
    public class ScheduleClient
    {
        public const string BASE_URL_SCHEDULE_DEFAULT = "https://api.jpush.cn/v3/schedules";
        public const string BASE_URL_SCHEDULE_BEIJING = "https://bjapi.push.jiguang.cn/v3/push/schedules";

        private string BASE_URL = BASE_URL_SCHEDULE_DEFAULT;

        private HttpClient _client;
        
        private JsonSerializer jsonSerializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public ScheduleClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// 设置 Schedule API 的调用地址。
        /// </summary>
        /// <param name="url"><see cref="BASE_URL_SCHEDULE_DEFAULT"/> or <see cref="BASE_URL_SCHEDULE_BEIJING"/></param>
        public void SetBaseURL(string url)
        {
            BASE_URL = url;
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

            HttpContent requestContent = new StringContent(json, Encoding.UTF8);
            HttpResponseMessage msg = await _client.PostAsync(BASE_URL, requestContent).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#_4"/>
        /// </summary>
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
                ["push"] = JObject.FromObject(pushPayload, jsonSerializer),
                ["trigger"] = new JObject
                {
                    ["single"] = new JObject
                    {
                        ["time"] = triggeringTime
                    }
                }
            };

            return await CreateScheduleTaskAsync(requestJson.ToString()).ConfigureAwait(false);
        }

        /// <summary>
        /// 创建单次定时任务。
        /// </summary>
        /// <param name="name">表示 schedule 任务的名字，由 schedule-api 在用户成功创建 schedule 任务后返回，不得超过 255 字节，由汉字、字母、数字、下划线组成。</param>
        /// <param name="pushPayload">推送对象</param>
        /// <param name="trigger">触发器</param>
        public HttpResponse CreateSingleScheduleTask(string name, PushPayload pushPayload, string triggeringTime)
        {
            Task<HttpResponse> task = Task.Run(() => CreateSingleScheduleTaskAsync(name, pushPayload, triggeringTime));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="CreatePeriodicalScheduleTask(string, PushPayload, Trigger)"/>
        /// </summary>
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
                ["push"] = JObject.FromObject(pushPayload, jsonSerializer),
                ["trigger"] = new JObject()
                {
                    ["periodical"] = JObject.FromObject(trigger)
                }
            };

            return await CreateScheduleTaskAsync(requestJson.ToString()).ConfigureAwait(false);
        }

        /// <summary>
        /// 创建会在一段时间内重复执行的定期任务。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#_4"/>
        /// </summary>
        /// <param name="name">表示 schedule 任务的名字，由 schedule-api 在用户成功创建 schedule 任务后返回，不得超过 255 字节，由汉字、字母、数字、下划线组成。</param>
        /// <param name="pushPayload">推送对象</param>
        /// <param name="trigger">触发器</param>
        public HttpResponse CreatePeriodicalScheduleTask(string name, PushPayload pushPayload, Trigger trigger)
        {
            Task<HttpResponse> task = Task.Run(() => CreatePeriodicalScheduleTaskAsync(name, pushPayload, trigger));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetValidScheduleTasks(int)"/>
        /// </summary>
        public async Task<HttpResponse> GetValidScheduleTasksAsync(int page = 1)
        {
            if (page <= 0)
                throw new ArgumentNullException(nameof(page));

            var url = BASE_URL + "?page=" + page;
            HttpResponseMessage msg = await _client.GetAsync(url).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取有效的定时任务列表。
        /// </summary>
        /// <param name="page">
        ///     <para>返回当前请求页的详细的 schedule-task 列表，如未指定 page 则 page 为 1。</para>
        ///     <para>排序规则：创建时间，由 schedule-service 完成。</para>
        ///     <para>如果请求页数大于总页数，则 page 为请求值，schedules 为空。</para>
        ///     <para>每页最多返回 50 个 task，如请求页实际的 task 的个数小于 50，则返回实际数量的 task。</para>
        /// </param>
        public HttpResponse GetValidScheduleTasks(int page = 1)
        {
            Task<HttpResponse> task = Task.Run(() => GetValidScheduleTasksAsync(page));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetScheduleTask(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetScheduleTaskAsync(string scheduleId)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(nameof(scheduleId));

            var url = BASE_URL + $"/{scheduleId}";
            HttpResponseMessage msg = await _client.GetAsync(url).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取指定的定时任务。
        /// </summary>
        /// <param name="scheduleId">定时任务 ID。在创建定时任务时会返回。</param>
        public HttpResponse GetScheduleTask(string scheduleId)
        {
            Task<HttpResponse> task = Task.Run(() => GetScheduleTaskAsync(scheduleId));
            task.Wait();
            return task.Result;
        }


        /// <summary>
        /// <see cref="GetScheduleTaskMsgId(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetScheduleTaskMsgIdAsync(string scheduleId)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(nameof(scheduleId));

            var url = BASE_URL + $"/{scheduleId}/msg_ids";
            HttpResponseMessage msg = await _client.GetAsync(url).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取定时任务对应的所有 msg_id。
        /// </summary>
        /// <param name="scheduleId">定时任务 ID。在创建定时任务时会返回。</param>
        public HttpResponse GetScheduleTaskMsgId(string scheduleId)
        {
            Task<HttpResponse> task = Task.Run(() => GetScheduleTaskMsgIdAsync(scheduleId));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> UpdateScheduleTaskAsync(string scheduleId, string json)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(nameof(scheduleId));

            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json));

            var url = BASE_URL + $"/{scheduleId}";
            HttpContent requestContent = new StringContent(json, Encoding.UTF8);
            HttpResponseMessage msg = await _client.PutAsync(url, requestContent).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// <see cref="UpdateSingleScheduleTask(string, string, bool?, string, PushPayload)"/>
        /// </summary>
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
                json["push"] = JObject.FromObject(pushPayload, jsonSerializer);
            }

            return await UpdateScheduleTaskAsync(scheduleId, json.ToString()).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新单次定时任务。
        /// </summary>
        /// <param name="scheduleId">任务 ID</param>
        /// <param name="name">任务名称，为 null 表示不更新。</param>
        /// <param name="enabled">是否可用，为 null 表示不更新。</param>
        /// <param name="triggeringTime">触发时间，类似 "2017-08-03 12:00:00"，为 null 表示不更新。</param>
        /// <param name="pushPayload">推送内容，为 null 表示不更新。</param>
        public HttpResponse UpdateSingleScheduleTask(string scheduleId, string name, bool? enabled, string triggeringTime, PushPayload pushPayload)
        {
            Task<HttpResponse> task = Task.Run(() => UpdateSingleScheduleTaskAsync(scheduleId, name, enabled, triggeringTime, pushPayload));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="UpdatePeriodicalScheduleTask(string, string, bool?, Trigger, PushPayload)"/>
        /// </summary>
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
                    ["periodical"] = JObject.FromObject(trigger, jsonSerializer)
                };
            }

            if (pushPayload != null)
            {
                json["push"] = JObject.FromObject(pushPayload, jsonSerializer);
            }

            return await UpdateScheduleTaskAsync(scheduleId, json.ToString()).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新会重复执行的定时任务。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule_2"/>
        /// </summary>
        /// <param name="scheduleId">任务 ID</param>
        /// <param name="name">任务名称，为 null 表示不更新。</param>
        /// <param name="enabled">是否可用，为 null 表示不更新。</param>
        /// <param name="trigger">触发器对象，为 null 表示不更新。</param>
        /// <param name="pushPayload">推送内容，为 null 表示不更新。</param>
        public HttpResponse UpdatePeriodicalScheduleTask(string scheduleId, string name, bool? enabled, Trigger trigger, PushPayload pushPayload)
        {
            Task<HttpResponse> task = Task.Run(() => UpdatePeriodicalScheduleTaskAsync(scheduleId, name, enabled, trigger, pushPayload));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="DeleteScheduleTask(string)"/>
        /// </summary>
        public async Task<HttpResponse> DeleteScheduleTaskAsync(string scheduleId)
        {
            if (string.IsNullOrEmpty(scheduleId))
                throw new ArgumentNullException(nameof(scheduleId));

            var url = BASE_URL + $"/{scheduleId}";
            HttpResponseMessage msg = await _client.DeleteAsync(url).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 删除指定的定时任务。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule_3"/>
        /// </summary>
        /// <param name="scheduleId">已创建的 schedule 任务的 id。如果 scheduleId 不合法，即不是有效的 uuid，则返回 404。</param>
        public HttpResponse DeleteScheduleTask(string scheduleId)
        {
            Task<HttpResponse> task = Task.Run(() => DeleteScheduleTaskAsync(scheduleId));
            task.Wait();
            return task.Result;
        }
    }
}
