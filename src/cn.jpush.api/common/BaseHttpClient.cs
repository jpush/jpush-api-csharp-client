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
            //Console.WriteLine("begin send" + reqParams);
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

                if (method == "POST")
                {
                    byte[] bs = Encoding.ASCII.GetBytes(reqParams);
                    myReq.ContentLength = bs.Length;
                    using (Stream reqStream = myReq.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                        reqStream.Close();
                    }
                }
               // Console.WriteLine("begin responese");

                response = (HttpWebResponse)myReq.GetResponse();
                HttpStatusCode statusCode = response.StatusCode;
                result.responseCode = statusCode;
                //    Console.WriteLine("prepare");
                if (Equals(response.StatusCode, HttpStatusCode.OK))
                {
                    //Console.WriteLine("enter");
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        result.responseContent = reader.ReadToEnd();
                       // Console.WriteLine(result.responseContent);
                    }
                    result.setErrorObject();
                }
                    //Console.WriteLine("end");
                //Console.WriteLine("response = " + response.Headers + "  contet=" + result.responseContent + "   status=" + response.StatusCode);
                //Console.WriteLine("remaining = " + remaining);
               // Console.WriteLine("code = " + result.error.errcode);
                

                if (statusCode == HttpStatusCode.OK)
                {
                    String limitQuota = response.GetResponseHeader(RATE_LIMIT_QUOTA);
                    String limitRemaining = response.GetResponseHeader(RATE_LIMIT_Remaining);
                    String limitReset = response.GetResponseHeader(RATE_LIMIT_Reset);
                    result.setRateLimit(limitQuota, limitRemaining, limitReset);
                    //Console.WriteLine("send success  ");
                }
                else if (statusCode == HttpStatusCode.NotFound)
                {
                    Debug.Print("error is 404");
                }
                else if (statusCode == HttpStatusCode.Forbidden)
                {
                    Debug.Print("error is 403");
                }
                else if (statusCode == HttpStatusCode.Unauthorized)
                {
                    Debug.Print("error is 401");
                }
                else if (statusCode == HttpStatusCode.InternalServerError)
                {
                    Debug.Print("error is 500");
                }
                else 
                {
                    Debug.Print("error is " + statusCode.ToString());
                }

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
            //Console.WriteLine("sssssssssssssss======="+result.responseCode);
            return result;
        }
    }
}
