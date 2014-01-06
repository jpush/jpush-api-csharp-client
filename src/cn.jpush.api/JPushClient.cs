using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api
{
    public class JPushClient
    {
        //appkey
        private String app_key;
        //密匙
        private String masterSecret;
        //离线时长int    $time_to_live 从消息推送时起，保存离线的时长。秒为单位。最多支持10天（864000秒）。 0 表示该消息不保存离线。
        private int time_to_live;


        public JPushClient(String app_key, String masterSecret, int time_to_live)
        {
            this.app_key = app_key;
            this.masterSecret = masterSecret;
            this.time_to_live = time_to_live;
        }


        /**
        * 通过tag发送通知
        * @param Strng $tag
        * @param Strng $app_key
        * @param int $sendno
        * @param Strng $send_description
        * @param Strng $mes_title
        * @param Strng $mes_content
        * @param Strng $perform
        * @param Strng $extras
        */
        public String sendNotificationByTag(String tag, int sendno, String send_description,
        String mes_title, String mes_content, String platform, String extras, String override_msg_id)
        {
            int mes_type = 1;
            int receiver_type = 2;


            //设置对象参数
            SendVO sendVO = new SendVO(this.app_key, this.masterSecret, this.time_to_live, mes_type, receiver_type, tag,
            sendno, send_description, mes_title, mes_content, platform, extras, override_msg_id);


            //发送通知 Or自定义消息
            BaseClient baseClient = new BaseClient();
            //echo $sendVO->getParams();
            String rsStr = baseClient.send(sendVO);


            return rsStr;
        }


        /**
        * 通过tag发送自定义消息
        * @param Strng $tag
        * @param Strng $app_key
        * @param int $sendno
        * @param Strng $send_description
        * @param Strng $mes_title
        * @param Strng $mes_content
        * @param Strng $perform
        * @param Strng $extras
        */
        public String sendCustomMesByTag(String tag, int sendno, String send_description,
        String mes_title, String mes_content, String platform, String extras, String override_msg_id)
        {
            int mes_type = 2;
            int receiver_type = 2;


            //设置对象参数
            SendVO sendVO = new SendVO(this.app_key, this.masterSecret, this.time_to_live, mes_type, receiver_type, tag,
            sendno, send_description, mes_title, mes_content, platform, extras, override_msg_id);


            //发送通知 Or自定义消息
            BaseClient baseClient = new BaseClient();
            //echo $sendVO->getParams();
            String rsStr = baseClient.send(sendVO);


            return rsStr;


        }


        /**
        * 通过alias发送通知
        * @param Strng $alias
        * @param Strng $app_key
        * @param int $sendno
        * @param Strng $send_description
        * @param Strng $mes_title
        * @param Strng $mes_content
        * @param Strng $perform
        * @param Strng $extras
        */
        public String sendNotificationByAlias(String alias, int sendno, String send_description,
                String mes_title, String mes_content, String platform, String extras, String override_msg_id)
        {
            int mes_type = 1;
            int receiver_type = 3;


            //设置对象参数
            SendVO sendVO = new SendVO(this.app_key, this.masterSecret, this.time_to_live, mes_type, receiver_type, alias,
            sendno, send_description, mes_title, mes_content, platform, extras, override_msg_id);


            //发送通知 Or自定义消息
            BaseClient baseClient = new BaseClient();
            //echo $sendVO->getParams();
            String rsStr = baseClient.send(sendVO);


            return rsStr;


        }


        /**
        * 通过alias发送自定义消息
        * @param Strng $alias
        * @param Strng $app_key
        * @param int $sendno
        * @param Strng $send_description
        * @param Strng $mes_title
        * @param Strng $mes_content
        * @param Strng $perform
        * @param Strng $extras
        */
        public String sendCustomMesByAlias(String alias, int sendno, String send_description,
        String mes_title, String mes_content, String platform, String extras, String override_msg_id)
        {
            int mes_type = 2;
            int receiver_type = 3;




            //设置对象参数
            SendVO sendVO = new SendVO(this.app_key, this.masterSecret, this.time_to_live, mes_type, receiver_type, alias,
            sendno, send_description, mes_title, mes_content, platform, extras, override_msg_id);


            //发送通知 Or自定义消息
            BaseClient baseClient = new BaseClient();
            //echo $sendVO->getParams();
            String rsStr = baseClient.send(sendVO);


            return rsStr;


        }


        /**
        * 发送广播通知
        * @param Strng $app_key
        * @param int $sendno
        * @param Strng $send_description
        * @param Strng $mes_title
        * @param Strng $mes_content
        * @param Strng $perform
        * @param Strng $extras
        */
        public String sendNotificationByAppkey(int sendno, String send_description,
        String mes_title, String mes_content, String platform, String extras, String override_msg_id)
        {
            int mes_type = 1;
            int receiver_type = 4;


            //设置对象参数
            SendVO sendVO = new SendVO(this.app_key, this.masterSecret, this.time_to_live, mes_type, receiver_type, "",
            sendno, send_description, mes_title, mes_content, platform, extras, override_msg_id);


            //发送通知 Or自定义消息
            BaseClient baseClient = new BaseClient();
            //echo $sendVO->getParams();
            String rsStr = baseClient.send(sendVO);
            return rsStr;


        }


        /**
        * 发送广播自定义消息
        * @param Strng $app_key
        * @param int $sendno
        * @param Strng $send_description
        * @param Strng $mes_title
        * @param Strng $mes_content
        * @param Strng $perform
        * @param Strng $extras
        */
        public String sendCustomMesByAppkey(int sendno, String send_description,
                 String mes_title, String mes_content, String platform, String extras, String override_msg_id)
        {
            int mes_type = 2;
            int receiver_type = 4;


            //设置对象参数
            SendVO sendVO = new SendVO(this.app_key, this.masterSecret, this.time_to_live, mes_type, receiver_type, "",
            sendno, send_description, mes_title, mes_content, platform, extras, override_msg_id);


            //发送通知 Or自定义消息
            BaseClient baseClient = new BaseClient();
            //echo $sendVO->getParams();
            String rsStr = baseClient.send(sendVO);
            return rsStr;


        }






        /**
         * 
         * @param String $app_key
         * @param String $msg_ids  msg_id以，连接
         */
        public String getReceivedApi(String msg_ids)
        {
            ReceivedVO receivedVO = new ReceivedVO(this.app_key, this.masterSecret, msg_ids);
            BaseClient baseClient = new BaseClient();
            return baseClient.getReceivedData(receivedVO);
        }

    }
}
