using System;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
using cn.jpush.api.util;
using cn.jpush.api.push;

namespace cn.jpush.api.common
{
    public class ResponseWrapper
    {
        private const int RESPONSE_CODE_NONE = -1;
        private string _responseContent;

        public JpushError jpushError;
        public HttpStatusCode responseCode = HttpStatusCode.BadRequest;
        public string responseContent
        {
            get
            {
                return _responseContent;
            }
            set
            {
                _responseContent = value;
            }
        }

        public void setErrorObject()
        {
            if (!string.IsNullOrEmpty(_responseContent))
            {
                jpushError = JsonConvert.DeserializeObject<JpushError>(_responseContent);
            }
        }

        public int rateLimitQuota;
        public int rateLimitRemaining;
        public int rateLimitReset;

        public bool isServerResponse()
        {
            return responseCode == HttpStatusCode.OK;
        }

        public string exceptionString;

        public ResponseWrapper()
        {

        }

        public void setRateLimit(string quota, string remaining, string reset)
        {
            if (null == quota) return;

            try
            {
                if (quota != "" && StringUtil.IsInt(quota))
                {
                    rateLimitQuota = int.Parse(quota);
                }
                if (remaining != "" && StringUtil.IsInt(remaining))
                {
                    rateLimitRemaining = int.Parse(remaining);
                }
                if (reset != "" && StringUtil.IsInt(reset))
                {
                    rateLimitReset = int.Parse(reset);
                }
                Console.WriteLine(string.Format("JPush API Rate Limiting params - quota:{0}, remaining:{1}, reset:{2} ", quota, remaining, reset) + " " + DateTime.Now);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }
    }
}
