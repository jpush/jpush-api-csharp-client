using Jiguang.JPush.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jiguang.JPush
{
    public class JPushClient
    {
        public const string BASE_URL_PUSH_DEFAULT = "https://api.jpush.cn/v3/push";
        public const string BASE_URL_PUSH_BEIJING = "https://bjapi.push.jiguang.cn/v3/push";

        private string BASE_URL = BASE_URL_PUSH_DEFAULT;

        public DeviceClient Device;
        public ScheduleClient Schedule;
        private ReportClient report;

        public ReportClient Report { get => report; set => report = value; }

        public static HttpClient HttpClient;

        static JPushClient()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public JPushClient(string appKey, string masterSecret)
        {
            if (string.IsNullOrEmpty(appKey))
                throw new ArgumentNullException(nameof(appKey));

            if (string.IsNullOrEmpty(masterSecret))
                throw new ArgumentNullException(nameof(masterSecret));

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(appKey + ":" + masterSecret));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            Report = new ReportClient();
            Device = new DeviceClient();
            Schedule = new ScheduleClient();
        }

        /// <summary>
        /// 设置 push 功能的 API 调用地址。
        /// <para>
        /// 如果极光应用分配在北京机房（极光控制台 “应用设置” -> “应用信息” 中可以看到），并且开发者接口调用的服务器也位于北京，则可以调用如下地址：
        ///
        /// https://bjapi.push.jiguang.cn/v3/push
        /// <para>可以提升 API 的响应速度。</para>
        /// </para>
        /// </summary>
        /// <param name="url"><see cref="BASE_URL_DEFAULT"/> or <see cref="BASE_URL_BEIJING"/></param>
        public void SetBaseURL(string url)
        {
            BASE_URL = url;
        }

        public async Task<HttpResponse> SendPushAsync(string jsonBody)
        {
            if (string.IsNullOrEmpty(jsonBody))
                throw new ArgumentNullException(nameof(jsonBody));

            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8);
            HttpResponseMessage msg = await HttpClient.PostAsync(BASE_URL, httpContent).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// <see cref="SendPush(PushPayload)"/>
        /// </summary>
        public async Task<HttpResponse> SendPushAsync(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            string body = payload.ToString();
            return await SendPushAsync(body);
        }

        /// <summary>
        /// 进行消息推送。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#_1"/>
        /// </summary>
        /// <param name="pushPayload"> 推送对象。<see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#_7"/> </param>
        public HttpResponse SendPush(PushPayload pushPayload)
        {
            Task<HttpResponse> task = Task.Run(() => SendPushAsync(pushPayload));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> IsPushValidAsync(string jsonBody)
        {
            if (string.IsNullOrEmpty(jsonBody))
                throw new ArgumentNullException(nameof(jsonBody));

            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8);
            var url = BASE_URL + "/validate";
            HttpResponseMessage msg = await HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// <see cref="IsPushValid(PushPayload)"/>
        /// </summary>
        public async Task<HttpResponse> IsPushValidAsync(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            var body = payload.ToString();
            return await IsPushValidAsync(body);
        }

        /// <summary>
        /// 校验推送能否成功。与推送 API 的区别在于：不会实际向用户发送任何消息。 其他字段说明和推送 API 完全相同。
        /// </summary>
        /// <param name="pushPayload"> 推送对象。<see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#_7"/> </param>
        public HttpResponse IsPushValid(PushPayload pushPayload)
        {
            Task<HttpResponse> task = Task.Run(() => IsPushValidAsync(pushPayload));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetCIdList(int?, string)"/>
        /// </summary>
        public async Task<HttpResponse> GetCIdListAsync(int? count, string type)
        {
            if (count != null && count < 1 && count > 1000)
                throw new ArgumentOutOfRangeException(nameof(count));

            var url = BASE_URL + "/cid";

            if (count != null)
            {
                url += ("?count=" + count);

                if (!string.IsNullOrEmpty(type))
                    url += ("&type=" + type);
            }

            HttpResponseMessage msg = await HttpClient.GetAsync(url).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 获取 CId（推送的唯一标识符） 列表。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#cid"/>
        /// </summary>
        /// <param name="count">不传默认为 1。范围为[1, 1000]</param>
        /// <param name="type">CId 的类型。取值："push" (默认) 或 "schedule"</param>
        public HttpResponse GetCIdList(int? count, string type)
        {
            Task<HttpResponse> task = Task.Run(() => GetCIdListAsync(count, type));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 针对RegID方式批量单推（VIP专属接口）
        /// 如果您在给每个用户的推送内容都不同的情况下，可以使用此接口。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#vip"/>
        /// </summary>
        /// <param name="singlePayLoadList">批量单推的载体列表</param>
        public async Task<HttpResponse> BatchPushByRegidAsync(List<SinglePayload> singlePayLoadList)
        {
            var url = BASE_URL + "/batch/regid/single";
            return await BatchPushAsync(url, singlePayLoadList);
        }

        /// <summary>
        /// 针对RegID方式批量单推（VIP专属接口）
        /// 如果您在给每个用户的推送内容都不同的情况下，可以使用此接口。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#vip"/>
        /// </summary>
        /// <param name="singlePayLoadList">批量单推的载体列表</param>
        public HttpResponse BatchPushByRegid(List<SinglePayload> singlePayLoadList)
        {
            Task<HttpResponse> task = Task.Run(() => BatchPushByRegidAsync(singlePayLoadList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 针对Alias方式批量单推（VIP专属接口）
        /// 如果您在给每个用户的推送内容都不同的情况下，可以使用此接口。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#vip"/>
        /// </summary>
        /// <param name="singlePayLoadList">批量单推的载体列表</param>
        public async Task<HttpResponse> BatchPushByAliasAsync(List<SinglePayload> singlePayLoadList)
        {
            var url = BASE_URL + "/batch/alias/single";
            return await BatchPushAsync(url, singlePayLoadList);
        }

        /// <summary>
        /// 针对Alias方式批量单推（VIP专属接口）
        /// 如果您在给每个用户的推送内容都不同的情况下，可以使用此接口。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#vip"/>
        /// </summary>
        /// <param name="singlePayLoadList">批量单推的载体列表</param>
        public HttpResponse BatchPushByAlias(List<SinglePayload> singlePayLoadList)
        {
            Task<HttpResponse> task = Task.Run(() => BatchPushByAliasAsync(singlePayLoadList));
            task.Wait();
            return task.Result;
        }

        private async Task<HttpResponse> BatchPushAsync(String url, List<SinglePayload> singlePayLoadList)
        {
            HttpResponse cidResponse = await this.GetCIdListAsync(singlePayLoadList.Count, "push");
            JObject jObject = (JObject) JsonConvert.DeserializeObject(cidResponse.Content);
            JArray jArray = ((JArray) jObject["cidlist"]);
            BatchPushPayload batchPushPayload = new BatchPushPayload();
            batchPushPayload.Pushlist = new Dictionary<String, SinglePayload>();
            for (int i = 0; i < singlePayLoadList.Count; i++)
            {
                batchPushPayload.Pushlist.Add((String) jArray[i], singlePayLoadList[i]);
            }
            HttpContent httpContent = new StringContent(batchPushPayload.ToString(), Encoding.UTF8);
            HttpResponseMessage msg = await HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }
    }
}
