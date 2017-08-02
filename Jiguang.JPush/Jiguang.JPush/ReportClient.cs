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
        /// 获取指定 msg_id 的消息送达统计数据。
        /// 最多支持 100 个 msg_id。
        /// </summary>
        /// <param name="msgId">具体消息的 msg_id</param>
        public async Task<HttpResponse> GetMessageReport(List<string> msgIdList)
        {
            if (msgIdList == null)
                throw new ArgumentNullException(nameof(msgIdList));

            var msgIds = string.Join(",", msgIdList);
            var url = BASE_URL + "received?msg_ids=" + msgIds;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url);
            var content = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 提供包括点击数等更详细的统计数据（VIP only）。
        /// 一次最多支持 100 个 msg_id。
        /// </summary>
        /// <param name="msgIdList"></param>
        /// <returns></returns>
        public async Task<HttpResponse> GetMessageDetailReport(List<string> msgIdList)
        {
            if (msgIdList == null)
                throw new ArgumentNullException(nameof(msgIdList));

            var msgIds = string.Join(",", msgIdList);
            var url = BASE_URL + "messages?msg_ids=" + msgIds;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url);
            var content = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        public async Task<HttpResponse> GetUserReport(string timeUnit, string startTime, int duration)
        {
            if (string.IsNullOrEmpty(timeUnit))
                throw new ArgumentNullException(nameof(timeUnit));

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));

            if (duration <= 0)
                throw new ArgumentOutOfRangeException(nameof(duration));

            var url = BASE_URL + "users?time_unit=" + timeUnit + "&start=" + startTime + "&duration=" + duration;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url);
            var content = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }
    }
}
