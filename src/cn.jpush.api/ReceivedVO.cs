using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api
{
    public class ReceivedVO
    {
        private String app_key;
        private String masterSecret;
        private String msg_ids;
        private String auth;


        public ReceivedVO(String app_key, String masterSecret, String msg_ids)
        {
            this.app_key = app_key;
            this.masterSecret = masterSecret;
            this.msg_ids = msg_ids;
        }
        public String getAuthStr()
        {
            return this.app_key + ":" + this.masterSecret;
        }


        public void setAuth(String auth)
        {
            this.auth = auth;
        }


        public String getAuth()
        {
            return this.auth;
        }


        public String getParams()
        {
            return msg_ids;
        }

    }
}
