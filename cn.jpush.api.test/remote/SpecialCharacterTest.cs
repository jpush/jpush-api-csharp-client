using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using cn.jpush.api.common;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class SpecialCharacterTest:BaseRemotePushTest
    {
        public SpecialCharacterTest()
        {
            JPushClient jpushClient = new JPushClient(APP_KEY,MASTER_SECRET);
    	    var result = jpushClient.updateDeviceTagAlias(REGISTRATION_ID1, "special_c", MOBILE, null, null);
    	    Assert.IsTrue (result.isResultOK());
        }
        public static  char[] SPECIAL_CHARS = new char[] {'`', '~', '!', '@', '#', '$', '%', 
	    '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '{', '}', '[', ']', 
	    '|', '\\', ':', ';', '"', '\'', '<', '>', ',', '.', '?', '/','［'};
        
        public int sendMessage(String content)
        {
            Message message = Message.content(content).setTitle("title");
            PushPayload payload =new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.s_alias("special_c");   
            payload.message  = message;
            try
            {
                _client.SendPush(payload);
            }
            catch (APIRequestException e)
            {
                return e.ErrorCode;
            }
            return 0;
        }
        public int sendNotification(String alert)
        {
      
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.s_alias("special_c");
            payload.notification = new Notification().setAlert(alert); 
            try
            {
                _client.SendPush(payload);
            }
            catch (APIRequestException e)
            {
                return e.ErrorCode;
            }
            return 0;
        }
        [TestMethod]
         public void testCharacters() 
         {
		    String prefix = "JPush Special Character tests - ";
		
		    foreach (char c in SPECIAL_CHARS)
            {
		        String msgContent = prefix + c;
	            Assert.AreEqual(0, sendNotification(msgContent));
		    }
            foreach (char c in SPECIAL_CHARS)
            {
		        String msgContent = prefix + c;
	            Assert.AreEqual(0, sendMessage(msgContent));
		    }

	    }
    }
}
