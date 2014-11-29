using cn.jpush.api.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace cn.jpush.api.common
{
    class BaseHttpClient
    {
        private const String CHARSET = "UTF-8";
	    private const String RATE_LIMIT_QUOTA = "X-Rate-Limit-Limit";
	    private const String RATE_LIMIT_Remaining = "X-Rate-Limit-Remaining";
	    private const String RATE_LIMIT_Reset = "X-Rate-Limit-Reset";
	
	    protected const int RESPONSE_OK = 200;
	
	    //设置连接超时时间
	    private const int DEFAULT_CONNECTION_TIMEOUT = (20 * 1000); // milliseconds
	    //设置读取超时时间
	    private const int DEFAULT_SOCKET_TIMEOUT = (30 * 1000); // milliseconds

        public ResponseResult sendPost(String url, String auth, String reqParams) 
        { 
            return this.sendRequest( "POST",  url,  auth, reqParams);        
        }
        public ResponseResult sendGet(String url, String auth, String reqParams)
        {
            return this.sendRequest("GET", url, auth, reqParams);
        }
        /**
         *
         * method "POST" or "GET"
         * url
         * auth   可选
         */
        public ResponseResult sendRequest(String method, String url, String auth,String reqParams)
        {
            Console.WriteLine("Send request - " + method.ToString()+ " " + url);
            if (null != reqParams)
            {
                Console.WriteLine("Request Content - " + reqParams);
            }
            ResponseResult result = new ResponseResult();
            HttpWebRequest myReq = null;
            HttpWebResponse response = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Method = method;
                myReq.ContentType = "application/json";
                if ( !String.IsNullOrEmpty(auth) )
                {
                    myReq.Headers.Add("Authorization", "Basic " + auth);    
                }
                if (method == "POST")
                {
                    byte[] bs = UTF8Encoding.UTF8.GetBytes(reqParams);
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
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        result.responseContent = reader.ReadToEnd();
                    }
                    String limitQuota = response.GetResponseHeader(RATE_LIMIT_QUOTA);
                    String limitRemaining = response.GetResponseHeader(RATE_LIMIT_Remaining);
                    String limitReset = response.GetResponseHeader(RATE_LIMIT_Reset);
                    result.setRateLimit(limitQuota, limitRemaining, limitReset);
                }
            }
            catch (WebException e)
            {
                HttpStatusCode errorCode = ((HttpWebResponse)e.Response).StatusCode;
                string statusDescription = ((HttpWebResponse)e.Response).StatusDescription;
                using (StreamReader sr = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    result.responseContent = sr.ReadToEnd();
                }
                result.responseCode = errorCode;
                result.exceptionString = e.Message;
                String limitQuota = ((HttpWebResponse)e.Response).GetResponseHeader(RATE_LIMIT_QUOTA);
                String limitRemaining = ((HttpWebResponse)e.Response).GetResponseHeader(RATE_LIMIT_Remaining);
                String limitReset = ((HttpWebResponse)e.Response).GetResponseHeader(RATE_LIMIT_Reset);
                result.setRateLimit(limitQuota, limitRemaining, limitReset);

                Debug.Print(e.Message);
            }
            catch (System.Exception ex)
            {
                 String errorMsg = ex.Message;
                 Debug.Print(errorMsg);
            }
            finally 
            {
                if (response != null)
                {
                    response.Close();                
                }
                if(myReq != null)
                {
                    myReq.Abort();
                }            
            }
            return result;
        }

    }
}
