using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Net;

namespace cn.jpush.api.common
{
    public class ResponseResult
    {
        private const int RESPONSE_CODE_NONE = -1;
    
        //private static Gson _gson = new Gson();

        public HttpStatusCode responseCode = HttpStatusCode.OK;
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
        
            try{
                rateLimitQuota = int.Parse(quota);
                rateLimitRemaining = int.Parse(remaining);
                rateLimitReset = int.Parse(reset);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);        
            }
        }
    
        public void setErrorObject() {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(responseContent)))
            {
                DataContractJsonSerializer serial = new DataContractJsonSerializer(typeof(ErrorObject));
                error = (ErrorObject)serial.ReadObject(ms);
             }
        }


	    public class ErrorObject {
	        public int code;
            public String message;

	    }

    }
}
