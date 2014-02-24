using cn.jpush.api.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

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
            ResponseResult result = new ResponseResult();
            HttpWebRequest myReq = null;
            HttpWebResponse response = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Method = method;
                myReq.Accept = "text/html, application/xhtml+xml, */*";
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.Headers.Add("Charset", "UTF-8");
                if (!Equals("", auth))
                {
                    myReq.Headers.Add("Authorization", "Basic " + auth);
                }


                response = (HttpWebResponse)myReq.GetResponse();
                result.responseCode = response.StatusCode;
                if (Equals(response.StatusCode, HttpStatusCode.OK))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        result.responseContent = reader.ReadToEnd();
                    }
                }
                String limitQuota = response.GetResponseHeader(RATE_LIMIT_QUOTA);
                String limitRemaining = response.GetResponseHeader(RATE_LIMIT_Remaining);
                String limitReset = response.GetResponseHeader(RATE_LIMIT_Reset);
                result.setRateLimit(limitQuota, limitRemaining, limitReset);

            }
            catch (System.Exception ex)
            {
                String errorMsg = ex.Message;
                Console.Write(errorMsg);
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
