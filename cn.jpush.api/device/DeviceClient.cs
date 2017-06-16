using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using cn.jpush.api.util;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace cn.jpush.api.device
{
    public class DeviceClient : BaseHttpClient
    {
        public const string HOST_NAME_SSL = "https://device.jpush.cn";
        public const string DEVICES_PATH = "/v3/devices";
        public const string TAGS_PATH = "/v3/tags";
        public const string ALIASES_PATH = "/v3/aliases";
        public const string STATUS_PATH = "/status/";

        private string appKey;
        private string masterSecret;
        private JsonSerializerSettings jSetting;

        public DeviceClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        // GET /v3/devices/{registration_id}
        // 获取当前设备的所有属性，包含 tags, alias，手机号码 mobile。
        public TagAliasResult getDeviceTagAlias(string registrationId)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(registrationId), "registrationId should be set");
            string url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;
            string auth = Base64.getBase64Encode(appKey + ":" + masterSecret);

            ResponseWrapper response = sendGet(url, auth, null);
            return TagAliasResult.fromResponse(response);
        }

        // POST /v3/devices/{registration_id}
        // Update device tag, alias and the phone number.
        // 更新当前设备的指定属性，当前支持 tags, alias，手机号码 mobile。
        public DefaultResult updateDevice(string registrationId, string alias, string mobile,
            HashSet<string> tagsToAdd, HashSet<string> tagsToRemove)
        {
            string url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

            JObject top = new JObject();
            if (null != alias)
            {
                top.Add("alias", alias);
            }
            if (null != mobile)
            {
                top.Add("mobile", mobile);
            }

            JObject tagObject = new JObject();
            if (tagsToAdd != null)
            {
                JArray tagsAdd = JArray.FromObject(tagsToAdd);
                if (tagsAdd.Count > 0)
                {
                    tagObject.Add("add", tagsAdd);
                }
            }
            if (tagsToRemove != null)
            {
                JArray tagsRemove = JArray.FromObject(tagsToRemove);
                if (tagsRemove.Count > 0)
                {
                    tagObject.Add("remove", tagsRemove);
                }
            }
            if (tagObject.Count > 0)
            {
                top.Add("tags", tagObject);
            }
            ResponseWrapper result = sendPost(url, Authorization(), top.ToString());
            return DefaultResult.fromResponse(result);
        }

        // Update tag and alias.
        public DefaultResult updateDeviceTagAlias(string registrationId, string alias,
            HashSet<string> tagsToAdd, HashSet<string> tagsToRemove)
        {
            string url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

            JObject top = new JObject();
            if (null != alias)
            {
                top.Add("alias", alias);
            }

            JObject tagObject = new JObject();
            if (tagsToAdd != null)
            {
                JArray tagsAdd = JArray.FromObject(tagsToAdd);
                if (tagsAdd.Count > 0)
                {
                    tagObject.Add("add", tagsAdd);
                }
            }
            if (tagsToRemove != null)
            {
                JArray tagsRemove = JArray.FromObject(tagsToRemove);
                if (tagsRemove.Count > 0)
                {
                    tagObject.Add("remove", tagsRemove);
                }
            }
            if (tagObject.Count > 0)
            {
                top.Add("tags", tagObject);
            }

            ResponseWrapper result = sendPost(url, Authorization(), top.ToString());
            return DefaultResult.fromResponse(result);
        }

        // Only add device alias, the function will set others to null.
        public DefaultResult addDeviceAlias(string registrationId, string alias)
        {
            string mobile = null;
            HashSet<string> tagsToAdd = null;
            HashSet<string> tagsToRemove = null;
            return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }

        // Only add device mobile phone, the function will set others to null.
        public DefaultResult addDeviceMobile(string registrationId, string mobile)
        {
            string alias = null;
            HashSet<string> tagsToAdd = null;
            HashSet<string> tagsToRemove = null;
            return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }

        // Only add device tags, the function will set others to null.
        public DefaultResult addDeviceTags(string registrationId, HashSet<string> tags)
        {
            string alias = null;
            string mobile = null;
            HashSet<string> tagsToAdd = tags;
            HashSet<string> tagsToRemove = null;
            return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }

        // Only remove device tags, the function will set others to null.
        public DefaultResult removeDeviceTags(string registrationId, HashSet<string> tags)
        {
            string alias = null;
            string mobile = null;
            HashSet<string> tagsToAdd = null;
            HashSet<string> tagsToRemove = tags;
            return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }

        public DefaultResult updateDeviceTags(string registrationId, HashSet<string> tagsToAdd, HashSet<string> tagsToRemove)
        {
            string url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

            JObject top = new JObject();
            JObject tagObject = new JObject();
            if (tagsToAdd != null)
            {
                JArray tagsAdd = JArray.FromObject(tagsToAdd);
                if (tagsAdd.Count > 0)
                {
                    tagObject.Add("add", tagsAdd);
                }
            }

            if (tagsToRemove != null)
            {
                JArray tagsRemove = JArray.FromObject(tagsToRemove);
                if (tagsRemove.Count > 0)
                {
                    tagObject.Add("remove", tagsRemove);
                }
            }

            if (tagObject.Count > 0)
            {
                top.Add("tags", tagObject);
            }

            ResponseWrapper result = sendPost(url, Authorization(), top.ToString());
            return DefaultResult.fromResponse(result);
        }

        // POST /v3/devices/{registration_id}. Clear the tags and alias.
        public DefaultResult updateDeviceTagAlias(string registrationId, bool clearAlias, bool clearTag)
        {
            Preconditions.checkArgument(clearAlias || clearTag, "It is not meaningful to do nothing.");

            string url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;
            JObject top = new JObject();
            if (clearAlias)
            {
                top.Add("alias", "");
            }
            if (clearTag)
            {
                top.Add("tags", "");
            }
            ResponseWrapper result = sendPost(url, Authorization(), top.ToString());
            return DefaultResult.fromResponse(result);
        }

        // GET /v3/tags/
        // 获取当前应用的所有标签列表。
        public TagListResult getTagList()
        {
            string url = HOST_NAME_SSL + TAGS_PATH + "/";
            string auth = Base64.getBase64Encode(appKey + ":" + masterSecret);
            ResponseWrapper response = sendGet(url, auth, null);
            return TagListResult.fromResponse(response);
        }

        // GET /v3/tags/{tag_value}/registration_ids/{registration_id}
        // 查询某个设备是否在 tag 下。
        public BooleanResult isDeviceInTag(string tag, string registrationID)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(tag), "theTag should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(registrationID), "registrationID should be set");

            string url = HOST_NAME_SSL + TAGS_PATH + "/" + tag + "/registration_ids/" + registrationID;
            ResponseWrapper response = sendGet(url, Authorization(), null);
            return BooleanResult.fromResponse(response);
        }

        // POST /v3/tags/{tag_value}
        // 为一个标签添加或者删除设备。
        public DefaultResult addRemoveDevicesFromTag(string tag, HashSet<string> toAddUsers, HashSet<string> toRemoveUsers)
        {
            string url = HOST_NAME_SSL + TAGS_PATH + "/" + tag;
            JObject top = new JObject();
            JObject registrationIds = new JObject();

            if (null != toAddUsers && toAddUsers.Count > 0)
            {
                JArray array = new JArray();
                foreach (string user in toAddUsers)
                {
                    array.Add(JToken.FromObject(user));
                }
                registrationIds.Add("add", array);
            }

            if (null != toRemoveUsers && toRemoveUsers.Count > 0)
            {
                JArray array = new JArray();
                foreach (string user in toRemoveUsers)
                {
                    array.Add(JToken.FromObject(user));
                }
                registrationIds.Add("remove", array);
            }
            top.Add("registration_ids", registrationIds);

            ResponseWrapper response = sendPost(url, Authorization(), top.ToString());
            return DefaultResult.fromResponse(response);
        }

        // POST /v3/tags/{tag_value}
        public DefaultResult addDevicesFromTag(string tag, HashSet<string> toAddUsers)
        {
            HashSet<string> toRemoveUsers = null;
            return addRemoveDevicesFromTag(tag, toAddUsers, toRemoveUsers);
        }

        // POST /v3/tags/{tag_value}
        public DefaultResult removeDevicesFromTag(string tag, HashSet<string> toRemoveUsers)
        {
            HashSet<string> toAddUsers = null;
            return addRemoveDevicesFromTag(tag, toAddUsers, toRemoveUsers);
        }

        // DELETE /v3/tags/{tag_value}
        // 删除一个标签，以及标签与设备之间的关联关系。
        public DefaultResult deleteTag(string tag, string platform)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(tag), "tag should be set");
            Preconditions.checkArgument(StringUtil.IsValidTag(tag), "tag should be the right format");

            string url = HOST_NAME_SSL + TAGS_PATH + "/" + tag;
            if (null != platform)
            {
                url += "?platform=" + platform;
            }
            ResponseWrapper response = sendDelete(url, Authorization(), null);
            return DefaultResult.fromResponse(response);
        }

        // GET /v3/aliases/{alias_value}
        // 查询别名（与设备的绑定关系）
        public AliasDeviceListResult getAliasDeviceList(string alias, string platform)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(alias), "alias should be set");
            Preconditions.checkArgument(StringUtil.IsValidAlias(alias), "alias should be the right format");

            string url = HOST_NAME_SSL + ALIASES_PATH + "/" + alias;
            if (null != platform)
            {
                url += "?platform=" + platform;
            }
            ResponseWrapper response = sendGet(url, Authorization(), null);
            return AliasDeviceListResult.fromResponse(response);
        }

        // DELETE /v3/aliases/{alias_value}
        // 删除一个别名，以及该别名与设备的绑定关系。
        public DefaultResult deleteAlias(string alias, string platform)
        {
            Preconditions.checkArgument(StringUtil.IsValidAlias(alias), "alias should be the right format");
            Preconditions.checkArgument(!String.IsNullOrEmpty(alias), "alias should be set");

            string url = HOST_NAME_SSL + ALIASES_PATH + "/" + alias;
            if (null != platform)
            {
                url += "?platform=" + platform;
            }
            ResponseWrapper response = sendDelete(url, Authorization(), null);
            return DefaultResult.fromResponse(response);
        }

        // POST /v3/devices/status/
        // 获取用户在线状态（VIP 专属接口）
        // 如需要开通此接口，请联系：商务客服。
        public DefaultResult getDeviceStatus(string[] registrationId)
        {
            string url = HOST_NAME_SSL + DEVICES_PATH + STATUS_PATH;
            Dictionary<string, string[]> registration = new Dictionary<string, string[]>
            {
                { "registration_ids", registrationId }
            };
            ResponseWrapper result = sendPost(url, Authorization(), ToJson(registration));
            return DefaultResult.fromResponse(result);
        }

        public string ToJson(Dictionary<string, string[]> registration)
        {
            jSetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(registration, jSetting);
        }

        private string Authorization()
        {
            Debug.Assert(!string.IsNullOrEmpty(appKey));
            Debug.Assert(!string.IsNullOrEmpty(masterSecret));
            string origin = appKey + ":" + masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }
}