using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class BaseRemotePushTest:BaseTest
    {
       
        public const String CONTENT_TYPE_JSON = "application/json";
    
        public const int SUCCEED_RESULT_CODE = 0;
        public const int LACK_OF_PARAMS = 1002;
        public const int INVALID_PARAMS = 1003;
        public const int AUTHENTICATION_FAIL = 1004;
        public const int TOO_BIG = 1005;
        public const int APPKEY_NOT_EXIST = 1008;
        public const int NO_TARGET = 1011;

        protected static JPushClient _client= new JPushClient(APP_KEY, MASTER_SECRET);

         
      
        [TestCleanup]
        public void after() {
        
        }
        [ClassCleanup]
        public static void afterClass(TestContext context)
        {
        }
        [ClassInitialize]
        public static void beforeClass(TestContext context)
        {
        }
        public String getResponseOK(int msgid, int sendno) {
            JObject json = new JObject();
            json.Add("msg_id", JToken.FromObject(msgid));
            json.Add("sendno", JToken.FromObject(sendno));
            return json.ToString();
        }

        public String getResponseError(int msgid, int sendno, int errorCode, String errorMessage)
        {

             JObject json = new JObject();
            json.Add("msg_id",JToken.FromObject(msgid));
            json.Add("sendno", JToken.FromObject(sendno));
        
            JObject error = new JObject();
            error.Add("code", JToken.FromObject(errorCode));
            error.Add("message", JToken.FromObject(errorMessage));
        
            json.Add("error", error);
            return json.ToString();
        }

    }
}
