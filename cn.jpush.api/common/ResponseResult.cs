using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Net;
using cn.jpush.api.util;
using System.Diagnostics;

namespace cn.jpush.api.common
{
    public class ResponseResult
    {
        private const int RESPONSE_CODE_NONE = -1;
    
        //private static Gson _gson = new Gson();

        public HttpStatusCode responseCode = HttpStatusCode.BadRequest;
        public String responseContent;
    
        public ErrorObject error;     // error for non-200 response, used by new API
    
        public int rateLimitQuota;
        public int rateLimitRemaining;
        public int rateLimitReset;
    
        public String exceptionString;

	    public ResponseResult() {
	    }
	
        public void setRateLimit(String quota, String remaining, String reset) {
            if (null == quota) return;

            //Console.WriteLine(quota);    
            try
            {
                //Console.WriteLine("1" + quota);   
                if (quota != "" && StringUtil.IsInt(quota))
                {
                    //Console.WriteLine("2");
                    rateLimitQuota = int.Parse(quota);
                    //Console.WriteLine("2" + quota);   
                }
                //Console.WriteLine("3" + remaining);   
                if (remaining!="" && StringUtil.IsInt(remaining))
                {
                    //Console.WriteLine("4");   
                    rateLimitRemaining = int.Parse(remaining);
                }
               // Console.WriteLine("5" + reset);   
                if (reset!="" && StringUtil.IsInt(reset))
                {
                    rateLimitReset = int.Parse(reset);
                }
            }
            catch(Exception e)
            {
                Debug.Print(e.Message);
            }
            //Console.WriteLine("end");    
        }

        public void setErrorObject()
        {
            this.error = (ErrorObject)JsonTool.JsonToObject(responseContent, new ErrorObject());
            //Console.WriteLine(this.error.errcode);
        }


	    public class ErrorObject {
	        public int errcode;
            public String errmsg;

	    }

    }
}
