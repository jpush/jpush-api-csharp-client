using cn.jpush.api.common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push
{
    public abstract class MessageParams
    {
        
        public const int DEFAULT_TIME_TO_LIVE = 86400;   //s
        public const int NO_TIME_TO_LIVE = -1;
        public const int DEFAULT_ANDROID_BUILDER_ID = 0;

	    /*
	     * 发送编号。由开发者自己维护，标识一次发送请求
	     */
        private int sendNo = 1;

        public int SendNo
        {
            get { return sendNo; }
            set { sendNo = value; }
        }

	    /*
	     * 待覆盖的上一条消息的 ID。
	     * 指明此参数，并且经确认该 msg_id 的确是该 AppKey 之前曾经使用过的，那么：Android 通知展示时会覆盖之前的。
	     */
        private String overrideMsgId;

        public String OverrideMsgId
        {
            get { return overrideMsgId; }
            set { overrideMsgId = value; }
        }

	    /*
	     *  (appKey)，只能填写一个。
	     * 如果不填，则会向所有的应用发送。
	     */
        private String appKey = "";

        public String AppKey
        {
            get { return appKey; }
            set { appKey = value; }
        }

	    /*
	     * 枚举类定义 ReceiverTypeEnum
	     */
        private ReceiverTypeEnum receiverType;

        public ReceiverTypeEnum ReceiverType
        {
            get { return receiverType; }
            set { receiverType = value; }
        }

	    /*
	     * 发送范围值，与 receiverType 相对应。
	     * receiverType = 4 不用设置
	     */
        private String receiverValue = "";

        public String ReceiverValue
        {
            get { return receiverValue; }
            set { receiverValue = value; }
        }

	    /*
	     * 保存离线的时长。秒为单位。最多支持10天（864000秒）。
	     * 0 表示该消息不保存离线。即：用户在线马上发出，当前不在线用户将不会收到此消息。
	     * 此参数不设置则表示默认，默认为保存1天的离线消息（86400秒）。	
	     */
        private long timeToLive = -1;

        public long TimeToLive
        {
            get { return timeToLive; }
            set { timeToLive = value; }
        } 

	    /*
	     * 每个应用对应一个masterSecret，用来校验
	     */
        private String masterSecret;

        public String MasterSecret
        {
            get { return masterSecret; }
            set { masterSecret = value; }
        }

	    /*
	     * 目标用户中断手机的平台类型，如：android, ios
	     */
        private HashSet<DeviceEnum> platform = new HashSet<DeviceEnum>();


        public String getPlatform()
        {
            //Console.WriteLine("platfrom"+this.platform);
            //if (this.platform == null) return "";

            String keys = "";
            if (this.receiverType == ReceiverTypeEnum.APP_KEY)
            {
                foreach (String device in Enum.GetNames(typeof(DeviceEnum)))
                {
                    keys += device.ToLower();
                    keys += ",";
                }
            }
            else
            {
                foreach (DeviceEnum key in this.platform)
                {
                    keys += (key.ToString().ToLower() + ",");
                }
            }
            Debug.Print(keys);

            return keys.Length > 0 ? keys.Substring(0, keys.Length - 1) : "";
        }

        public void addPlatform(DeviceEnum platform)
        {
            this.platform.Add(platform);
        }
	
        // 0: development env  1: production env
        private int apnsProduction;

        public int ApnsProduction
        {
            get { return apnsProduction; }
            set { apnsProduction = value; }
        }

	    /*
	     * 发送消息的内容。
	     * 与 msg_type 相对应的值。
	     */
        private String msgContent = "";

        public String MsgContent
        {
            get { return msgContent; }
            set { msgContent = value; }
        }             

        public abstract void setMsgContent();

	   

    }

    public enum ReceiverTypeEnum
    {
        [Obsolete]
        IMEI = 1,

        TAG = 2,

        ALIAS = 3,

        APP_KEY = 4,

        REGISTRATION_ID = 5
    
    }
}
