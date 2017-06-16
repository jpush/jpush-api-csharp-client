using cn.jpush.api.common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace cn.jpush.api.report
{
    public class UsersResult : BaseResult
    {
        public TimeUnit time_unit;
        public string start;
        public int duration;
        public List<User> items = new List<User>();

        public UsersResult()
        {
            time_unit = TimeUnit.DAY;
            start = null;
            duration = 0;
        }

        public static UsersResult fromResponse(ResponseWrapper responseWrapper)
        {
            UsersResult usersResult = new UsersResult();
            if (responseWrapper.isServerResponse())
            {
                usersResult = JsonConvert.DeserializeObject<UsersResult>(responseWrapper.responseContent);
            }
            usersResult.ResponseResult = responseWrapper;
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
            public string time;
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
