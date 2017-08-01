using Jiguang.JPush.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
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

        internal ReportClient Report { get => report; set => report = value; }

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

        public async Task<HttpResponse> Send(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            string body = payload.ToString();

            Console.WriteLine(body);

            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage msg = await HttpClient.PostAsync(BASE_URL, httpContent);
            string content = await msg.Content.ReadAsStringAsync();
            HttpResponse response = new HttpResponse(msg.StatusCode, msg.Headers, content);
            return response;
        }

        public async Task SendAsync(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            string body = payload.ToString();
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await HttpClient.PostAsync(BASE_URL, httpContent);
        }

        public async Task<HttpResponseMessage> IsPushValid(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            string body = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            var url = BASE_URL + "/validate";
            var resultTask = HttpClient.PostAsync(url, httpContent);
            return await resultTask;
        }

        public async Task<HttpResponseMessage> GetCIdList(int? count, string type)
        {
            if (count != null && count < 1 && count > 1000)
                throw new ArgumentOutOfRangeException(nameof(count));

            var url = BASE_URL + "/cid";

            if (count != null)
            {
                url += ("?count=" + count);
                if (!string.IsNullOrEmpty(type))
                {
                    url += ("&type=" + type);
                }
            }

            var resultTask = HttpClient.GetAsync(url);
            return await resultTask;
        }
    }
}
