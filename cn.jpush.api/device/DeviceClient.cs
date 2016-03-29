using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using cn.jpush.api.util;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.device
{
    public class DeviceClient : BaseHttpClient
    {
        public   const String HOST_NAME_SSL = "https://device.jpush.cn";
        public   const String DEVICES_PATH = "/v3/devices";
        public   const String TAGS_PATH = "/v3/tags";
        public   const String ALIASES_PATH = "/v3/aliases";
        public   const String STATUS_PATH = "/status/";

        private String appKey;
        private String masterSecret;
        private JsonSerializerSettings jSetting;

        public DeviceClient(String appKey, String masterSecret) 
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;        
        }

        //GET /v3/devices/{registration_id}
        //获取当前设备的所有属性，包含tags, alias，手机号码mobile。
        public TagAliasResult getDeviceTagAlias(String registrationId)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(registrationId), "registrationId should be set");
            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;
            String auth = Base64.getBase64Encode(this.appKey + ":" + this.masterSecret);

            ResponseWrapper response = this.sendGet(url, auth, null);
            return TagAliasResult.fromResponse(response);

        }

        //POST /v3/devices/{registration_id}
        //update   Device  Tag   Alias and the phone number
        //更新当前设备的指定属性，当前支持tags, alias，手机号码mobile。
        public DefaultResult updateDevice(String registrationId,
                                                   String alias,
                                                   String mobile,
                                                   HashSet<String> tagsToAdd,
                                                   HashSet<String> tagsToRemove)
        {
            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

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

        //update    Device   Tag   Alias
        public DefaultResult updateDeviceTagAlias(String registrationId,
                                                  String alias,
                                                  HashSet<String> tagsToAdd,
                                                  HashSet<String> tagsToRemove)
        {
            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

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

        //only add Device Alias,the function will set others to null
        public DefaultResult addDeviceAlias(String registrationId,String alias)
        {
            String mobile = null;
            HashSet<String> tagsToAdd = null;
            HashSet<String> tagsToRemove = null;
            return updateDevice(registrationId,alias, mobile, tagsToAdd, tagsToRemove);
        }

        //only add Device Mobile,the function will set others to null
        public DefaultResult addDeviceMobile(String registrationId, String mobile)
        {
            String alias = null;
            HashSet<String> tagsToAdd = null;
            HashSet<String> tagsToRemove = null;
            return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }

        //only add Device Tags,the function will set others to null
        public DefaultResult addDeviceTags(String registrationId, HashSet<String> tags)
        {
            String alias = null;
            String mobile = null;
            HashSet<String> tagsToAdd = tags;
            HashSet<String> tagsToRemove = null;
            return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }

        //only remove Device Tags,the function will set others to null
        public DefaultResult removeDeviceTags(String registrationId, HashSet<String> tags)
        {
            String alias = null;
            String mobile = null;
            HashSet<String> tagsToAdd = null;
            HashSet<String> tagsToRemove = tags;
            return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }


        //update DeviceTag,add ,remove
        public DefaultResult updateDeviceTags(String registrationId, HashSet<String> tagsToAdd, HashSet<String> tagsToRemove)
        {
            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

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

        //POST /v3/devices/{registration_id}  clear the tags ,alias

        public DefaultResult updateDeviceTagAlias(String registrationId, bool clearAlias, bool clearTag)
        {
            Preconditions.checkArgument(clearAlias || clearTag, "It is not meaningful to do nothing.");

            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

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

        //GET /v3/tags/
        //获取当前应用的所有标签列表。
 
        public TagListResult getTagList() 
        {
            String url = HOST_NAME_SSL + TAGS_PATH + "/";
            String auth = Base64.getBase64Encode(this.appKey + ":" + this.masterSecret);
            ResponseWrapper response = this.sendGet(url, auth, null);
            return TagListResult.fromResponse(response);
        }


        //GET /v3/tags/{tag_value}/registration_ids/{registration_id}
        //查询某个设备是否在 tag 下。
        public BooleanResult isDeviceInTag(String theTag, String registrationID)
         {
            Preconditions.checkArgument(!String.IsNullOrEmpty(theTag), "theTag should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(registrationID), "registrationID should be set");
            String url = HOST_NAME_SSL + TAGS_PATH + "/" + theTag + "/registration_ids/" + registrationID;
            ResponseWrapper response = this.sendGet(url, Authorization(), null);
            return BooleanResult.fromResponse(response);        
         }

        //POST /v3/tags/{tag_value}
        //为一个标签添加或者删除设备。
        public DefaultResult addRemoveDevicesFromTag(String theTag, 
                                                      HashSet<String> toAddUsers, 
                                                      HashSet<String> toRemoveUsers) 
         {
            String url = HOST_NAME_SSL + TAGS_PATH + "/" + theTag;
        
            JObject top = new JObject();
            JObject registrationIds = new JObject();
            if (null != toAddUsers && toAddUsers.Count > 0) 
            {
                JArray array = new JArray();
                foreach (String user in toAddUsers) {
                    array.Add(JToken.FromObject(user));
                }
                registrationIds.Add("add", array);
            }
            if (null != toRemoveUsers && toRemoveUsers.Count > 0) 
            {
                JArray array = new JArray();
                foreach (String user in toRemoveUsers) 
                {
                    array.Add(JToken.FromObject(user));
                }
                registrationIds.Add("remove", array);
            }
            top.Add("registration_ids", registrationIds);
            ResponseWrapper response = this.sendPost(url, Authorization(), top.ToString());
            return DefaultResult.fromResponse(response);
        }

        //POST /v3/tags/{tag_value}
        public DefaultResult addDevicesFromTag(String theTag, HashSet<String> toAddUsers)
        {
            HashSet<String> toRemoveUsers = null;
            return addRemoveDevicesFromTag(theTag, toAddUsers, toRemoveUsers);
        }


        //POST /v3/tags/{tag_value}
        public DefaultResult removeDevicesFromTag(String theTag, HashSet<String> toRemoveUsers)
        {
            HashSet<String> toAddUsers = null;
            return addRemoveDevicesFromTag(theTag, toAddUsers, toRemoveUsers);
        }

        //DELETE /v3/tags/{tag_value}
        //删除一个标签，以及标签与设备之间的关联关系。
        public DefaultResult deleteTag(String theTag, String platform) 
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(theTag), "theTag should be set");
            Preconditions.checkArgument(StringUtil.IsValidTag(theTag), "theTag should be the right format");
            String url = HOST_NAME_SSL + TAGS_PATH + "/" + theTag;
            if (null != platform) {
        	    url += "?platform=" + platform; 
            }

            ResponseWrapper response = this.sendDelete(url, Authorization(), null);
            return DefaultResult.fromResponse(response);        
        }


        // ------------- alias
        //GET /v3/aliases/{alias_value}
        //查询别名 （与设备的绑定关系）
        public AliasDeviceListResult getAliasDeviceList(String alias, String platform)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(alias), "alias should be set");
            Preconditions.checkArgument(StringUtil.IsValidAlias(alias), "the alias should be the right format");
            String url = HOST_NAME_SSL + ALIASES_PATH + "/" + alias;
            if (null != platform) {
        	    url += "?platform=" + platform; 
            }
            ResponseWrapper response = this.sendGet(url, Authorization(), null);
        
            return AliasDeviceListResult.fromResponse(response);
        }


        //DELETE /v3/aliases/{alias_value}
        //删除一个别名，以及该别名与设备的绑定关系。
        public DefaultResult deleteAlias(String alias, String platform)
         {
            Preconditions.checkArgument(StringUtil.IsValidAlias(alias), "the alias should be the right format");
            Preconditions.checkArgument(!String.IsNullOrEmpty(alias), "alias should be set");
            String url = HOST_NAME_SSL + ALIASES_PATH + "/" + alias;
            if (null != platform) {
        	    url += "?platform=" + platform; 
            }
            ResponseWrapper response = this.sendDelete(url, Authorization(), null);
            return DefaultResult.fromResponse(response);
        }

        //  POST /v3/devices/status/ vip
        //获取用户在线状态（VIP专属接口）
        //如需要开通此接口，请联系：商务客服
        public DefaultResult getDeviceStatus(String[] registrationId)
        {
            String url = HOST_NAME_SSL + DEVICES_PATH + STATUS_PATH;
            Dictionary<String, String[]> registration = new Dictionary<String, String[]>();
            registration.Add("registration_ids", registrationId);
            ResponseWrapper result = sendPost(url, Authorization(), ToJson(registration));
            return DefaultResult.fromResponse(result);
        }

        public string ToJson(Dictionary<String, String[]> registration)
        {
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            return JsonConvert.SerializeObject(registration, jSetting);
        }

        private String Authorization()
       {
            Debug.Assert(!string.IsNullOrEmpty(this.appKey));
            Debug.Assert(!string.IsNullOrEmpty(this.masterSecret));
            String origin = this.appKey + ":" + this.masterSecret;
            return Base64.getBase64Encode(origin);
       }
    }

}