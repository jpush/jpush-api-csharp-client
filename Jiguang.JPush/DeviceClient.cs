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
        /// <see cref="GetDeviceInfo(string)"/>
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
        /// 查询指定设备信息。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_1"/>
        /// </summary>
        /// <param name="registrationId">
        ///     客户端初始化 JPush 成功后，JPush 服务端会分配一个 Registration ID，作为此设备的标识（同一个手机不同 APP 的 Registration ID 是不同的）。
        /// </param>
        public HttpResponse GetDeviceInfo(string registrationId)
        {
            Task<HttpResponse> task = Task.Run(() => GetDeviceInfoAsync(registrationId));
            task.Wait();
            return task.Result;
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
        /// <see cref="UpdateDeviceInfo(string, DevicePayload)"/>
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

        /// <summary>
        /// 更新设备信息（目前支持 tag, alias 和 mobile）。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_2"/>
        /// </summary>
        /// <param name="registrationId">
        ///     客户端初始化 JPush 成功后，JPush 服务端会分配一个 Registration ID，作为此设备的标识（同一个手机不同 APP 的 Registration ID 是不同的）。
        /// </param>
        /// <param name="devicePayload">设备信息对象</param>
        public HttpResponse UpdateDeviceInfo(string registrationId, DevicePayload devicePayload)
        {
            Task<HttpResponse> task = Task.Run(() => UpdateDeviceInfoAsync(registrationId, devicePayload));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetDeviceInfo(string)"/>
        /// </summary>
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
        /// 获取指定 alias 下的设备，最多输出 10 个。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_3"/>
        /// </summary>
        /// <param name="alias">要查询的别名（alias）</param>
        /// <param name="platform">"android" 或 "ios", 为 null 则默认为所有平台。</param>
        public HttpResponse GetDeviceByAlias(string alias, string platform)
        {
            Task<HttpResponse> task = Task.Run(() => GetDevicesByAliasAsync(alias, platform));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="DeleteAlias(string, string)"/>
        /// </summary>
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
        /// 删除一个别名，以及该别名与设备的绑定关系。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_4"/>
        /// </summary>
        /// <param name="alias">待删除的别名（alias）</param>
        /// <param name="platform">"android" 或 "ios"，为 null 则默认为所有平台。</param>
        public HttpResponse DeleteAlias(string alias, string platform)
        {
            Task<HttpResponse> task = Task.Run(() => DeleteAliasAsync(alias, platform));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetTags"/>
        /// </summary>
        public async Task<HttpResponse> GetTagsAsync()
        {
            var url = BASE_URL + "/v3/tags/";
            HttpResponseMessage msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取当前应用的所有标签列表，每个平台最多返回 100 个。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_5"/>
        /// </summary>
        public HttpResponse GetTags()
        {
            Task<HttpResponse> task = Task.Run(() => GetTagsAsync());
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="IsDeviceInTag(string, string)"/>
        /// </summary>
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
        /// 查询某个设备是否在某个 tag 下。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_5"/>
        /// </summary>
        /// <param name="registrationId">设备的 registration id</param>
        /// <param name="tag">要查询的 tag</param>
        public HttpResponse IsDeviceInTag(string registrationId, string tag)
        {
            Task<HttpResponse> task = Task.Run(() => IsDeviceInTagAsync(registrationId, tag));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="AddDevicesToTag(string, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> AddDevicesToTagAsync(string tag, List<string> registrationIdList)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));

            if (registrationIdList == null || registrationIdList.Count == 0)
                throw new ArgumentException(nameof(registrationIdList));

            var url = BASE_URL + "/v3/tags/" + tag;

            JObject jObj = new JObject
            {
                ["registration_ids"] = new JObject
                {
                    ["add"] = new JArray(registrationIdList)
                }
            };

            var requestContent = new StringContent(jObj.ToString(), Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent).ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, "");
        }

        /// <summary>
        /// 为一个标签（tag）添加设备，一次最多支持 1000 个。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_7"/>
        /// </summary>
        /// <param name="tag">待操作的标签（tag）</param>
        /// <param name="registrationIdList">设备的 registration id 列表</param>
        public HttpResponse AddDevicesToTag(string tag, List<string> registrationIdList)
        {
            Task<HttpResponse> task = Task.Run(() => AddDevicesToTagAsync(tag, registrationIdList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="RemoveDevicesFromTag(string, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> RemoveDevicesFromTagAsync(string tag, List<string> registrationIdList)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));

            if (registrationIdList == null || registrationIdList.Count == 0)
                throw new ArgumentException(nameof(registrationIdList));

            var url = BASE_URL + "/v3/tags/" + tag;

            JObject jObj = new JObject
            {
                ["registration_ids"] = new JObject
                {
                    ["remove"] = new JArray(registrationIdList)
                }
            };

            var requestContent = new StringContent(jObj.ToString(), Encoding.UTF8);
            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent).ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, "");
        }

        /// <summary>
        /// 为一个标签移除设备。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_7"/>
        /// </summary>
        /// <param name="tag">待操作的标签（tag）</param>
        /// <param name="registrationIdList">设备的 registration id 列表</param>
        public HttpResponse RemoveDevicesFromTag(string tag, List<string> registrationIdList)
        {
            Task<HttpResponse> task = Task.Run(() => RemoveDevicesFromTagAsync(tag, registrationIdList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="DeleteTag(string, string)"/>
        /// </summary>
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
        /// 删除标签，以及标签与其下设备的关联关系。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#_8"/>
        /// </summary>
        /// <param name="tag">待删除标签</param>
        /// <param name="platform">"android" 或 "ios"，如果为 null，则默认为所有平台</param>
        public HttpResponse DeleteTag(string tag, string platform)
        {
            Task<HttpResponse> task = Task.Run(() => DeleteTagAsync(tag, platform));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetUserOnlineStatus(List{string})"/>
        /// </summary>
        public async Task<HttpResponse> GetUserOnlineStatusAsync(List<string> registrationIdList)
        {
            if (registrationIdList == null || registrationIdList.Count == 0)
                throw new ArgumentException(nameof(registrationIdList));

            var url = BASE_URL + "/v3/devices/status/";
            JObject jObj = new JObject
            {
                ["registration_ids"] = new JArray(registrationIdList)

            };

            var requestContent = new StringContent(jObj.ToString(), Encoding.UTF8);

            HttpResponseMessage msg = await JPushClient.HttpClient.PostAsync(url, requestContent).ConfigureAwait(false);
            string responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
        }

        /// <summary>
        /// 获取用户在线状态（VIP only）。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/#vip"/>
        /// </summary>
        /// <param name="registrationIdList">待查询用户设备的 registration id，每次最多支持 1000 个。</param>
        public HttpResponse GetUserOnlineStatus(List<string> registrationIdList)
        {
            Task<HttpResponse> task = Task.Run(() => GetUserOnlineStatusAsync(registrationIdList));
            task.Wait();
            return task.Result;
        }
    }
}