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
            if (Equals(ResponseResult, HttpStatusCode.OK) && this.errcode == ERROR_CODE_OK)
            {
                return true;            
            }
            return false;
        }
    }
}
