using Jiguang.JPush.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JPush
{
    public class ReportClient
    {
        private const string BASE_URL = "https://report.jpush.cn/v3/";

        /// <summary>
        /// <see cref="GetMessageReport(List{string})"/>
        /// </summary>
        public async Task<HttpResponse> GetMessageReportAsync(List<string> msgIdList)
        {
            if (msgIdList == null)
                throw new ArgumentNullException(nameof(msgIdList));

            var msgIds = string.Join(",", msgIdList);
            var url = BASE_URL + "received?msg_ids=" + msgIds;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 获取指定 msg_id 的消息送达统计数据。
        /// </summary>
        /// <param name="msgIdList">消息的 msg_id 列表，每次最多支持 100 个。</param>
        public HttpResponse GetMessageReport(List<string> msgIdList)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessageReportAsync(msgIdList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetMessageSendStatus(string, List{string}, string)"/>
        /// </summary>
        public async Task<HttpResponse> GetMessageSendStatusAsync(string msgId, List<string> registrationIdList, string data)
        {
            if (string.IsNullOrEmpty(msgId))
                throw new ArgumentNullException(nameof(msgId));

            if (registrationIdList == null)
                throw new ArgumentNullException(nameof(registrationIdList));

            JObject body = new JObject
            {
                { "msg_id", long.Parse(msgId) },
                { "registration_ids", JArray.FromObject(registrationIdList) }
            };

            if (!string.IsNullOrEmpty(data))
                body.Add("data", data);

            var url = BASE_URL + "/status/message";
            var httpContent = new StringContent(body.ToString(), Encoding.UTF8);

            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 查询指定消息的送达状态。
        /// <para><see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/#_7"/></para>
        /// </summary>
        /// <param name="msgId">待查询消息的 Message Id。</param>
        /// <param name="registrationIdList">收到消息设备的 Registration Id 列表。</param>
        /// <param name="data">待查询日期，格式为 yyyy-MM-dd。如果传 null，则默认为当天。</param>
        public HttpResponse GetMessageSendStatus(string msgId, List<string> registrationIdList, string data)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessageSendStatusAsync(msgId, registrationIdList, data));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetMessageDetailReport(List{string})"/>
        /// </summary>
        public async Task<HttpResponse> GetMessageDetailReportAsync(List<string> msgIdList)
        {
            if (msgIdList == null)
                throw new ArgumentNullException(nameof(msgIdList));

            var msgIds = string.Join(",", msgIdList);
            var url = BASE_URL + "messages?msg_ids=" + msgIds;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 提供包括点击数等更详细的统计数据（VIP only）。
        /// </summary>
        /// <param name="msgIdList">消息的 msg_id 列表，每次最多支持 100 个。</param>
        public HttpResponse GetMessageDetailReport(List<string> msgIdList)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessageDetailReportAsync(msgIdList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetUserReport(string, string, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetUserReportAsync(string timeUnit, string startTime, int duration)
        {
            if (string.IsNullOrEmpty(timeUnit))
                throw new ArgumentNullException(nameof(timeUnit));

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));

            if (duration <= 0)
                throw new ArgumentOutOfRangeException(nameof(duration));

            var url = BASE_URL + "users?time_unit=" + timeUnit + "&start=" + startTime + "&duration=" + duration;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 提供近2个月内某时间段的用户相关统计数据：新增用户、在线用户、活跃用户（VIP only）。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/#vip_1"/>
        /// </summary>
        /// <param name="timeUnit">时间单位。支持 "HOUR", "DAY" 或 "MOUNTH"</param>
        /// <param name="startTime">
        ///     起始时间。
        ///     <para>如果单位是小时，则起始时间是小时（包含天），格式例：2014-06-11 09</para>
        ///     <para>如果单位是天，则起始时间是日期（天），格式例：2014-06-11</para>
        ///     <para>如果单位是月，则起始时间是日期（月），格式例：2014-06</para>
        /// </param>
        /// <param name="duration">
        ///     持续时长。
        ///     <para>如果时间单位（timeUnit）是天，则是持续的天数，其他时间单位以此类推。</para>
        ///     <para>只支持查询 60 天以内的用户信息。如果 timeUnit 为 HOUR，则只会输出当天的统计结果。</para>
        /// </param>
        public HttpResponse GetUserReport(string timeUnit, string startTime, int duration)
        {
            Task<HttpResponse> task = Task.Run(() => GetUserReportAsync(timeUnit, startTime, duration));
            task.Wait();
            return task.Result;
        }
    }
}
