using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.common.resp
{
   public class APIConnectionException:Exception
    {
        public String message;
        public String info;
        public APIConnectionException(String message,String info):base(message)
        {
            this.message = message;
            this.info = info;
        }
    }
}
