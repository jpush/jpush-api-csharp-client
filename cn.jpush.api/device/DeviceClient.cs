using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using cn.jpush.api.util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.device
{
     class DeviceClient : BaseHttpClient
    {
        public   const String HOST_NAME_SSL = "https://device.jpush.cn";
        public   const String DEVICES_PATH = "/v3/devices";
        public   const String TAGS_PATH = "/v3/tags";
        public   const String ALIASES_PATH = "/v3/aliases";

        private String appKey;
        private String masterSecret;
        public DeviceClient(String appKey, String masterSecret) 
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;        
        }
        public TagAliasResult getDeviceTagAlias(String registrationId)
        {
            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;
            String auth = Base64.getBase64Encode(this.appKey + ":" + this.masterSecret);

            ResponseWrapper response = this.sendGet(url, auth, null);

            return TagAliasResult.fromResponse(response);

        }
        public DefaultResult updateDeviceTagAlias(String registrationId, bool clearAlias, bool clearTag) 
        {
            Preconditions.checkArgument(clearAlias || clearTag, "It is not meaningful to do nothing.");
    	
            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

            JObject top = new JObject();
            if (clearAlias) {
                top.Add("alias", "");
            }
            if (clearTag) {
                top.Add("tags", "");
            }
            ResponseWrapper result = sendPost(url, Authorization(), top.ToString());

            return DefaultResult.fromResponse(result);
        }
        public DefaultResult updateDeviceTagAlias(String registrationId, 
                                                   String alias,  
                                                   HashSet<String> tagsToAdd,
                                                   HashSet<String> tagsToRemove) 
         {
            String url = HOST_NAME_SSL + DEVICES_PATH + "/" + registrationId;

            JObject top = new JObject();
            if (null != alias) {
                top.Add("alias", alias);
            }
            
            JObject tagObject = new JObject();
            if (tagsToAdd!=null)
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
        
            if (tagObject.Count > 0) {
                top.Add("tags", tagObject);
            }
            ResponseWrapper result = sendPost(url, Authorization(), top.ToString());

            return DefaultResult.fromResponse(result);
       }
        public TagListResult getTagList() 
        {
            String url = HOST_NAME_SSL + TAGS_PATH + "/";
            String auth = Base64.getBase64Encode(this.appKey + ":" + this.masterSecret);
            ResponseWrapper response = this.sendGet(url, auth, null);
            return TagListResult.fromResponse(response);
        }

         public BooleanResult isDeviceInTag(String theTag, String registrationID)
         {
            String url = HOST_NAME_SSL + TAGS_PATH + "/" + theTag + "/registration_ids/" + registrationID;
            ResponseWrapper response = this.sendGet(url, Authorization(), null);
            return BooleanResult.fromResponse(response);        
         }
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
         
        public DefaultResult deleteTag(String theTag, String platform) 
        {
            String url = HOST_NAME_SSL + TAGS_PATH + "/" + theTag;
            if (null != platform) {
        	    url += "?platform=" + platform; 
            }

            ResponseWrapper response = this.sendDelete(url, Authorization(), null);
            return DefaultResult.fromResponse(response);        
        }
         // ------------- alias
    
       public AliasDeviceListResult getAliasDeviceList(String alias, String platform)
        {
            String url = HOST_NAME_SSL + ALIASES_PATH + "/" + alias;
            if (null != platform) {
        	    url += "?platform=" + platform; 
            }
            ResponseWrapper response = this.sendGet(url, Authorization(), null);
        
            return AliasDeviceListResult.fromResponse(response);
        }
       public DefaultResult deleteAlias(String alias, String platform)
         {
            String url = HOST_NAME_SSL + ALIASES_PATH + "/" + alias;
            if (null != platform) {
        	    url += "?platform=" + platform; 
            }
            ResponseWrapper response = this.sendDelete(url, Authorization(), null);
            return DefaultResult.fromResponse(response);
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
