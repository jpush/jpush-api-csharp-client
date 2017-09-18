using Jiguang.JPush.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
