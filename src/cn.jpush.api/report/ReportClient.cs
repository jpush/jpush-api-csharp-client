using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.report
{
    class ReportClient
    {
        private String appKey;

        private String masterSecret;

        public ReportClient(String appKey, String masterSecret) 
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;        
        }

        public ReceivedResult getReceiveds(String msg_ids) 
        {
            ReceivedResult result = new ReceivedResult();

            return result;
        
        }
    }
}
