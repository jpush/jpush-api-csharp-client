using cn.jpush.api.common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.report
{
   public class UsersResult : BaseResult
    {
        public TimeUnit time_unit;
        public String start;
        public int duration;
        public UsersResult()
        {
            time_unit = TimeUnit.DAY;
            start = null;
            duration = 0;
        }
        public List<User> items = new List<User>();

        public static UsersResult fromResponse(ResponseWrapper responseWrapper)
        {
            UsersResult usersResult = new UsersResult();
            if (responseWrapper.isServerResponse()) {
                usersResult = JsonConvert.DeserializeObject<UsersResult> (responseWrapper.responseContent);
            }
            usersResult.ResponseResult=responseWrapper;
            return usersResult;
	    }
        public override bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }
        public class User
        {
            
            public String time;
            public Android android;
            public Ios ios;
            public User()
            {
                time = null;
                android = null;
                ios = null;
            }
       }

        public class Android
        {

            [JsonProperty(PropertyName = "new")]
            public long add;
            public int online;
            public int active;
            public Android()
            {
                add = 0;
                online = 0;
                active = 0;
            }
        }

        public class Ios
        {
            [JsonProperty(PropertyName = "new")]
            public long add;
            public int online;
            public int active;
            public Ios()
            {
                add = 0;
                online = 0;
                active = 0;
            }
        }
    }
   
     
    
}
