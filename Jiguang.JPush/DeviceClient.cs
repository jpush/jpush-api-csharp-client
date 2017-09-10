using Jiguang.JPush.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jiguang.JPush
{
    public class DeviceClient
    {
        private const string BASE_URL = "https://device.jpush.cn";

        /// <summary>
        /// 查询指定设备信息。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_1"/>
        /// </summary>
        public async Task<HttpResponse> GetDeviceInfoAsync(string registrationId)
        {
            if (string.IsNullOrEmpty(registrationId))
                throw new ArgumentNullException(registrationId);

            var url = BASE_URL + "/v3/devices/" + registrationId;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 更新设备信息（如设备 tag, alias, mobile）。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_2"/>
        /// </summary>
        public async Task<HttpResponse> UpdateDeviceInfoAsync(string registrationId, DevicePayload devicePayload)
        {
            if (string.IsNullOrEmpty(registrationId))
                throw new ArgumentNullException(nameof(registrationId));

            if (devicePayload == null)
                throw new ArgumentNullException(nameof(devicePayload));

            var json = devicePayload.ToString();
            return await UpdateDeviceInfoAsync(registrationId, json);
        }

        public async Task<HttpResponse> UpdateDeviceInfoAsync(string registrationId, string json)
        {
            if (string.IsNullOrEmpty(registrationId))
                throw new ArgumentNullException(nameof(registrationId));

            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json));

            var url = BASE_URL + "/v3/devices/" + registrationId;
            HttpContent requestContent = new StringContent(json, Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取指定 alias 下的设备，最多输出 10 个。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_3"/>
        /// </summary>
        /// <param name="alias">要查询的别名（alias）</param>
        /// <param name="platform">"android" / "ios", 为 null 则默认为所有平台。</param>
        public async Task<HttpResponse> GetDevicesByAliasAsync(string alias, string platform)
        {
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));

            var url = BASE_URL + "/v3/aliases/" + alias;

            if (!string.IsNullOrEmpty(platform))
                url += "?platform=" + platform;

            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string responseConetent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseConetent);
        }

        /// <summary>
        /// 删除一个别名，以及该别名与设备的绑定关系。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_4"/>
        /// </summary>
        /// <param name="alias">待删除的别名（alias）</param>
        /// <param name="platform">"android" / "ios"，为 null 则默认为所有平台。</param>
        public async Task<HttpResponse> DeleteAliasAsync(string alias, string platform)
        {
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(alias);

            var url = BASE_URL + "/v3/aliases/" + alias;

            if (!string.IsNullOrEmpty(platform))
                url += "?platform=" + platform;

            HttpResponseMessage msg = await JPushClient.HttpClient.DeleteAsync(url).ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, "");
        }

        /// <summary>
        /// 获取当前应用的所有标签列表，每个平台最多返回 100 个。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_5"/>
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponse> GetTagsAsync()
        {
            var url = BASE_URL + "/v3/tags/";
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 查询某个设备是否在某个 tag 下。
        /// </summary>
        /// <param name="registrationId">设备的 registration id</param>
        /// <param name="tag">要查询的 tag</param>
        public async Task<HttpResponse> IsDeviceInTagAsync(string registrationId, string tag)
        {
            if (string.IsNullOrEmpty(registrationId))
                throw new ArgumentNullException(nameof(registrationId));

            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));

            var url = BASE_URL + "/v3/tags/" + tag + "/registration_ids/" + registrationId;
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 为一个标签（tag）添加设备，一次最多支持 1000 个。
        /// </summary>
        public async Task<HttpResponse> AddDevicesToTagAsync(string tag, List<string> registrationIds)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));

            if (registrationIds == null || registrationIds.Count == 0)
                throw new ArgumentException(nameof(registrationIds));

            var url = BASE_URL + "/v3/tags/" + tag;

            JObject jObj = new JObject
            {
                ["registration_ids"] = new JObject
                {
                    ["add"] = new JArray(registrationIds)
                }
            };

            var requestContent = new StringContent(jObj.ToString(), Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent).ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, "");
        }

        /// <summary>
        /// 为一个标签移除设备。
        /// </summary>
        /// <param name="tag">待操作的标签（tag）</param>
        /// <param name="registrationIds">设备的 registration id 列表</param>
        /// <returns></returns>
        public async Task<HttpResponse> RemoveDevicesFromTagAsync(string tag, List<string> registrationIds)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));

            if (registrationIds == null || registrationIds.Count == 0)
                throw new ArgumentException(nameof(registrationIds));

            var url = BASE_URL + "/v3/tags/" + tag;

            JObject jObj = new JObject
            {
                ["registration_ids"] = new JObject
                {
                    ["remove"] = new JArray(registrationIds)
                }
            };

            var requestContent = new StringContent(jObj.ToString(), Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent).ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, "");
        }

        /// <summary>
        /// 删除标签，以及标签与其下设备的关联关系。
        /// </summary>
        /// <param name="tag">待删除标签</param>
        /// <param name="platform">"android" / "ios"，如果为 null，则默认为所有平台</param>
        public async Task<HttpResponse> DeleteTagAsync(string tag, string platform)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));

            var url = BASE_URL + "/v3/tags/" + tag;

            if (!string.IsNullOrEmpty(platform))
                url += "?platform=" + platform;

            HttpResponseMessage msg = await JPushClient.HttpClient.DeleteAsync(url).ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, "");
        }

        /// <summary>
        /// 获取用户在线状态（VIP only）。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#vip"/>
        /// </summary>
        /// <param name="registrationIds">待查询用户设备的 registration id，每次最多支持 1000 个。</param>
        public async Task<HttpResponse> GetUserOnlineStatusAsync(List<string> registrationIds)
        {
            if (registrationIds == null || registrationIds.Count == 0)
                throw new ArgumentException(nameof(registrationIds));

            var url = BASE_URL + "/v3/devices/status/";

            var requestJson = JsonConvert.SerializeObject(registrationIds);
            HttpContent requestContent = new StringContent(requestJson, Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }
    }
}