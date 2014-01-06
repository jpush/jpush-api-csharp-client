using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api
{
    /**
     * 
     * 发送信息对象
     */
    public class SendVO
    {
        //appkey
        private String app_key;
        //密匙
        private String masterSecret;
        //离线时长int    $time_to_live 从消息推送时起，保存离线的时长。秒为单位。最多支持10天（864000秒）。 0 表示该消息不保存离线。
        private int time_to_live;
        //send no
        private int sendno;
        //int    $receiver_type * 接收者类型。value: 2、指定的 tag。3、指定的 alias。4、广播：对 app_key 下的所有用户推送消息。
        private int receiver_type;
        // String $receiver_value   发送范围值  tag:支持多达 10 个，使用 "," 间隔。alias:支持多达 1000 个，使用 "," 间隔。广播：不需要填
        private String receiver_value;
        //String $verification_code * 验证串，用于校验发送的合法性
        private String verification_code;
        //int    $msg_type * 发送消息的类型：１、通知２、自定义消息（只有 Android 支持）
        private int msg_type;
        //String $msg_content * 发送消息的内容
        private String msg_content;
        // String $send_description 描述此次发送调用。
        private String send_description;
        //String $platform  * 目标用户终端手机的平台类型，如： android, ios 多个请使用逗号分隔。
        private String platform;
        //String $override_msg_id 待覆盖的上一条消息的 ID
        private String override_msg_id;


        public SendVO(String app_key, String masterSecret, int timeToLive, int mes_type, int receiver_type, String receiver_value, int sendno, String send_description,
            String mes_title, String mes_content, String platform, String extras, String override_msg_id)
        {
            this.app_key = app_key;
            this.masterSecret = masterSecret;
            this.time_to_live = timeToLive;
            this.receiver_type = receiver_type;
            this.receiver_value = receiver_value;


            String content = "";
            if (mes_type == 1)
            {
                content = "{\"n_title\":\"" + mes_title + "\",\"n_content\":\"" + mes_content + "\",\"n_extras\":\"" + extras + "\"}";
                Console.Write(content);
                Console.WriteLine();
            }
            else if (mes_type == 2)
            {
                content = "{\"title\":\"" + mes_title + "\",\"message\":\"" + mes_content + "\",\"extras\":\"" + extras + "\"}";
            }
            this.msg_content = content;
            this.msg_type = mes_type;
            this.sendno = sendno;
            this.send_description = send_description;
            this.platform = platform;
            this.override_msg_id = override_msg_id;


        }


        public String getVerification_code()
        {
            String verification_code = this.sendno +""+ this.receiver_type + this.receiver_value + this.masterSecret;
            return verification_code;
        }


        public void setVerification_code(String verification_code)
        {
            this.verification_code = verification_code;
        }


        public String getParams()
        {
            String str = "app_key=" + this.app_key + "&receiver_type=" + this.receiver_type +
                "&receiver_value=" + this.receiver_value + "&verification_code=" + this.verification_code +
                "&msg_type=" + this.msg_type + "&msg_content=" + this.msg_content + "&send_description=" + this.send_description +
                "&send_description=" + this.send_description + "&platform=" + this.platform +
                "&time_to_live=" + this.time_to_live + "&override_msg_id=" + this.override_msg_id + "&sendno=" + this.sendno;
            return str;
        }


    }

}
