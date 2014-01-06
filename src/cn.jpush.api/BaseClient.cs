using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api
{
    public class BaseClient
    {
        //消息发送接口URL
        static String SEND_API_URL = "http://api.jpush.cn:8800/v2/push";


        //Report Received API URL
        static String RECEIVE_API_URL = "https://report.jpush.cn/v2/received";


        /**
         * 发送主体
         */
        public String send(SendVO sendVO)
        {
            String str = "";
            //验证信息MD5加密
            String verification_code = new SecretEncode().getMD5Hash(sendVO.getVerification_code());
            //Console.Write(sendVO.getVerification_code() +  " ****************** "+ verification_code);
            //Console.WriteLine();
            sendVO.setVerification_code(verification_code);
            HttpTools http = new HttpTools();
            String urlParams = sendVO.getParams();
            str = http.toPost(SEND_API_URL, urlParams);
            return str;
        }


        /**
         * received API主体
         */
        public String getReceivedData(ReceivedVO receivedVO)
        {
            //验证信息 base64加密
            String authStr = new SecretEncode().getBase64Encode(receivedVO.getAuthStr());
            receivedVO.setAuth(authStr);
            //Console.Write(receivedVO.getAuthStr() + " ****************** " + authStr);
            //Console.WriteLine();
            String url = RECEIVE_API_URL + "?msg_ids=" + receivedVO.getParams();
            HttpTools http = new HttpTools();
            String str = http.toGet(url, authStr);
            return str;


        }

    }
}
