namespace cn.jpush.api.common
{
    public abstract class BaseResult
    {
        public const int ERROR_CODE_NONE = -1;
        public const int ERROR_CODE_OK = 0;
        public const string ERROR_MESSAGE_NONE = "None error message";

        public const int RESPONSE_OK = 200;

        private ResponseWrapper responseResult;

        public ResponseWrapper ResponseResult
        {
            get { return responseResult; }
            set { responseResult = value; }
        }

        public abstract bool isResultOK();

        public int getRateLimitQuota()
        {
            if (null != responseResult)
            {
                return responseResult.rateLimitQuota;
            }
            return 0;
        }

        public int getRateLimitRemaining()
        {
            if (null != responseResult)
            {
                return responseResult.rateLimitRemaining;
            }
            return 0;
        }

        public int getRateLimitReset()
        {
            if (null != responseResult)
            {
                return responseResult.rateLimitReset;
            }
            return 0;
        }
    }
}
