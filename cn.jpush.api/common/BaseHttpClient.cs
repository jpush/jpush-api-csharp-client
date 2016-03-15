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
using cn.jpush.api.common.resp;

namespace cn.jpush.api.common
{
    public class BaseHttpClient
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

        public ResponseWrapper sendPost(String url, String auth, String reqParams) 
        { 
            return this.sendRequest( "POST",  url,  auth, reqParams);        
        }
        public ResponseWrapper sendDelete(String url, String auth, String reqParams)
        {
            return this.sendRequest("DELETE", url, auth, reqParams);
        }
        public ResponseWrapper sendGet(String url, String auth, String reqParams)
        {
            return this.sendRequest("GET", url, auth, reqParams);
        }

        public ResponseWrapper sendPut(String url, String auth, String reqParams)
        {
            return this.sendRequest("PUT", url, auth, reqParams);
        }
        /**
         *
         * method "POST" or "GET"
         * url
         * auth   可选
         */
        public ResponseWrapper sendRequest(String method, String url, String auth,String reqParams)
        {
            Console.WriteLine("Send request - " + method.ToString() + " " + url + " "+ DateTime.Now);
            if (null != reqParams)
            {
                Console.WriteLine("Request Content - " + reqParams +" "+ DateTime.Now);
            }
            //结果wrap
            ResponseWrapper result = new ResponseWrapper();
            //创建httprequest
            HttpWebRequest myReq = null;
            //创建httpresponse
            HttpWebResponse response = null;
            try
            {
                //利用工厂机制（factory mechanism）通过Create()方法来创建的
                myReq = (HttpWebRequest)WebRequest.Create(url);
                //request类型
                myReq.Method = method;
                myReq.ContentType = "application/json";
                //auth是否为null或者空
                if ( !String.IsNullOrEmpty(auth) )
                {
                    //添加头auth
                    myReq.Headers.Add("Authorization", "Basic " + auth);    
                }
                if (method == "POST")
                {
                    //utf8编码
                    byte[] bs = UTF8Encoding.UTF8.GetBytes(reqParams);
                    myReq.ContentLength = bs.Length;
                    using (Stream reqStream = myReq.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                        reqStream.Close();
                    }
                }

                if (method == "PUT")
                {
                    //utf8编码
                    byte[] bs = UTF8Encoding.UTF8.GetBytes(reqParams);
                    myReq.ContentLength = bs.Length;
                    using (Stream reqStream = myReq.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                        reqStream.Close();
                    }
                }

                //response
                response = (HttpWebResponse)myReq.GetResponse();
                //http status code
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
                    Console.WriteLine("Succeed to get response - 200 OK" +" "+ DateTime.Now);
                    Console.WriteLine("Response Content - {0}", result.responseContent +" "+ DateTime.Now);
                }
            }
            //异常处理
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
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
                    result.setErrorObject();
                    Console.WriteLine(string.Format("fail  to get response - {0}", errorCode) + " "+ DateTime.Now);
                    Console.WriteLine(string.Format("Response Content - {0}", result.responseContent) + " "+ DateTime.Now);

                    throw new APIRequestException(result);
                }
                else
                {//
                    throw new APIConnectionException(e.Message);
                }
               
            }
            //这里不再抓取非http的异常，如果异常抛出交给开发者自行处理
            //catch (System.Exception ex)
            //{
            //     String errorMsg = ex.Message;
            //     Debug.Print(errorMsg);
            //}
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
