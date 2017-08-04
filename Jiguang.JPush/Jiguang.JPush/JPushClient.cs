using Jiguang.JPush.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JPush
{
    /// <summary>
    /// SDK 入口。
    /// </summary>
    public class JPushClient
    {
        private const string BASE_URL = "https://api.jpush.cn/v3/push";

        private static string APP_KEY;
        private static string MASTER_SECRET;

        public DeviceClient Device;
        public ScheduleClient Schedule;
        private ReportClient report;

        public ReportClient Report { get => report; set => report = value; }

        public static readonly HttpClient HttpClient;

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

            APP_KEY = appKey;
            MASTER_SECRET = masterSecret;

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(appKey + ":" + masterSecret));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            Report = new ReportClient();
            Device = new DeviceClient();
            Schedule = new ScheduleClient();
        }

        public async Task<HttpResponse> SendPushAsync(string jsonBody)
        {
            if (string.IsNullOrEmpty(jsonBody))
                throw new ArgumentNullException(nameof(jsonBody));

            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8);
            HttpResponseMessage msg = await HttpClient.PostAsync(BASE_URL, httpContent);
            var content = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        public async Task<HttpResponse> SendPushAsync(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            string body = payload.ToString();
            return await SendPushAsync(body);
        }

        public async Task<HttpResponse> IsPushValidAsync(string jsonBody)
        {
            if (string.IsNullOrEmpty(jsonBody))
                throw new ArgumentNullException(nameof(jsonBody));

            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8);
            var url = BASE_URL + "/validate";
            HttpResponseMessage msg = await HttpClient.PostAsync(url, httpContent);
            var content = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        public async Task<HttpResponse> IsPushValidAsync(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            var body = payload.ToString();
            return await IsPushValidAsync(body);
        }

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

            HttpResponseMessage msg = await HttpClient.GetAsync(url);
            var content = await msg.Content.ReadAsStringAsync();
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }
    }
}
