using cn.jpush.api.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace cn.jpush.api.push
{
    public class MessageResult : BaseResult
    {
        public int msg_id;
        public int sendno;
        public int errcode = ERROR_CODE_NONE;
        public String errmsg;
        public long getMessageId() 
        {
            return this.msg_id;
        }
        public int getSendNo() 
        {
            return this.sendno;
        }
        override public int getErrorCode() 
        {
            return this.errcode;
        }
    
        override public String getErrorMessage() 
        {
            return this.errmsg;
        }

        override public bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;            
            }
            return false;
        }
        public override string ToString()
        {
            if (errcode == ERROR_CODE_NONE)
            {
                return string.Format("sendno:{0},message_id:{1}", sendno, msg_id);
            }
            else
            {
                return string.Format("errcode:{0},errmsg:{1}", errcode, errmsg);
            }
        }
    }
    //"{\"sendno\":\"0\",\"msg_id\":\"1704649583\"}"
    public class JpushSuccess
    {
        public string sendno;
        public string msg_id;
    }
    public class JpushError
    {
       public JpushErrorDetail error;
    }
    public class JpushErrorDetail
    {
       public int     code;
       public String  message;
    }
}
