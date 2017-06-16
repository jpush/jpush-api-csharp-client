using System;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using cn.jpush.api.common.resp;

namespace cn.jpush.api.common
{
    public class BaseHttpClient
    {
        private const string CHARSET = "UTF-8";
        private const string RATE_LIMIT_QUOTA = "X-Rate-Limit-Limit";
        private const string RATE_LIMIT_Remaining = "X-Rate-Limit-Remaining";
        private const string RATE_LIMIT_Reset = "X-Rate-Limit-Reset";

        protected const int RESPONSE_OK = 200;

        // 设置连接超时时间
        private const int DEFAULT_CONNECTION_TIMEOUT = (100 * 1000); // milliseconds

        // 设置读取超时时间
        private const int DEFAULT_SOCKET_TIMEOUT = (100 * 1000); // milliseconds

        public ResponseWrapper sendPost(string url, string auth, string reqParams)
        {
            return sendRequest("POST", url, auth, reqParams);
        }

        public ResponseWrapper sendDelete(string url, string auth, string reqParams)
        {
            return sendRequest("DELETE", url, auth, reqParams);
        }

        public ResponseWrapper sendGet(string url, string auth, string reqParams)
        {
            return sendRequest("GET", url, auth, reqParams);
        }

        public ResponseWrapper sendPut(string url, string auth, string reqParams)
        {
            return sendRequest("PUT", url, auth, reqParams);
        }

        public ResponseWrapper sendRequest(string method, string url, string auth, string reqParams)
        {
            Console.WriteLine("Send request - " + method.ToString() + " " + url + " " + DateTime.Now);
            if (null != reqParams)
            {
                Console.WriteLine("Request Content - " + reqParams + " " + DateTime.Now);
            }

            ResponseWrapper result = new ResponseWrapper();
            HttpWebRequest myReq = null;
            HttpWebResponse response = null;

            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Method = method;
                myReq.ContentType = "application/json";
                myReq.KeepAlive = false;

                if (!String.IsNullOrEmpty(auth))
                {
                    myReq.Headers.Add("Authorization", "Basic " + auth);
                }

                if (method == "POST" || method == "PUT")
                {
                    byte[] bs = Encoding.UTF8.GetBytes(reqParams);
                    myReq.ContentLength = bs.Length;
                    using (Stream reqStream = myReq.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                        reqStream.Close();
                    }
                }

                response = (HttpWebResponse)myReq.GetResponse();
                HttpStatusCode statusCode = response.StatusCode;
                result.responseCode = statusCode;
                if (Equals(response.StatusCode, HttpStatusCode.OK))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        result.responseContent = reader.ReadToEnd();
                    }
                    string limitQuota = response.GetResponseHeader(RATE_LIMIT_QUOTA);
                    string limitRemaining = response.GetResponseHeader(RATE_LIMIT_Remaining);
                    string limitReset = response.GetResponseHeader(RATE_LIMIT_Reset);
                    result.setRateLimit(limitQuota, limitRemaining, limitReset);
                    Console.WriteLine("Succeed to get response - 200 OK" + " " + DateTime.Now);
                    Console.WriteLine("Response Content - {0}", result.responseContent + " " + DateTime.Now);
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpStatusCode errorCode = ((HttpWebResponse)e.Response).StatusCode;
                    string statusDescription = ((HttpWebResponse)e.Response).StatusDescription;
                    using (StreamReader sr = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream(), Encoding.UTF8))
                    {
                        result.responseContent = sr.ReadToEnd();
                    }
                    result.responseCode = errorCode;
                    result.exceptionString = e.Message;
                    string limitQuota = ((HttpWebResponse)e.Response).GetResponseHeader(RATE_LIMIT_QUOTA);
                    string limitRemaining = ((HttpWebResponse)e.Response).GetResponseHeader(RATE_LIMIT_Remaining);
                    string limitReset = ((HttpWebResponse)e.Response).GetResponseHeader(RATE_LIMIT_Reset);
                    result.setRateLimit(limitQuota, limitRemaining, limitReset);
                    Debug.Print(e.Message);
                    result.setErrorObject();
                    Console.WriteLine(string.Format("fail  to get response - {0}", errorCode) + " " + DateTime.Now);
                    Console.WriteLine(string.Format("Response Content - {0}", result.responseContent) + " " + DateTime.Now);

                    throw new APIRequestException(result);
                }
                else
                {
                    string info = method + url + auth + reqParams;
                    Console.WriteLine(info);
                    throw new APIConnectionException(e.Message, info);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (myReq != null)
                {
                    myReq.Abort();
                }
            }
            return result;
        }
    }
}
