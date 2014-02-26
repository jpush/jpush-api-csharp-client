using cn.jpush.api.common;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.report
{
    class ReportClient:BaseHttpClient
    {
        private  const String REPORT_HOST_NAME = "https://report.jpush.cn";
        private  const String REPORT_RECEIVE_PATH = "/v2/received";
        private String appKey;

        private String masterSecret;

        public ReportClient(String appKey, String masterSecret) 
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;        
        }

        public ReceivedResult getReceiveds(String msg_ids) 
        {

            String url = REPORT_HOST_NAME + REPORT_RECEIVE_PATH + "?msg_ids=" + msg_ids;
            String auth = Base64.getBase64Encode(this.appKey+":"+this.masterSecret);
            ResponseResult rsp = this.sendGet(url, auth, null);
            ReceivedResult result = new ReceivedResult();
            List<ReceivedResult.Received> list = new List<ReceivedResult.Received>();

            //Console.WriteLine("recie content=="+rsp.responseContent);
            if (rsp.responseCode == System.Net.HttpStatusCode.OK)
            {
                //Console.WriteLine("responseContent===" + result.responseContent);
                list = (List<ReceivedResult.Received>)JsonTool.JsonToObject(rsp.responseContent,list);
                //Console.WriteLine(obj);
                String content = rsp.responseContent;
            }
            result.ResponseResult = rsp;
            result.ReceivedList = list;
            return result;
        
        }
    }
}
