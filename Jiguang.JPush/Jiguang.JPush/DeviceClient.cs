using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jiguang.JPush
{
    public class DeviceClient
    {
        private const string BASE_URL = "https://deivce.jpush.cn";

        /// <summary>
        /// 查询指定设备的别名和标签。
        /// </summary>
        public async Task<HttpResponseMessage> GetDevice(string registrationId)
        {
            if (string.IsNullOrEmpty(registrationId))
                throw new ArgumentNullException(registrationId);

            var url = BASE_URL + "/v3/devices/" + registrationId;
            var resultTask = JPushClient.HttpClient.GetAsync(url);
            return await resultTask;
        }
    }
}
