using System;

namespace cn.jpush.api.common.resp
{
    public class APIConnectionException : Exception
    {
        public string message;
        public string info;

        public APIConnectionException(string message, string info) : base(message)
        {
            this.message = message;
            this.info = info;
        }
    }
}
