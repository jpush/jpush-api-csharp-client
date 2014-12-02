using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class MessageTest:BaseRemotePushTest
    {
        
         [TestMethod]
        public void sendMessageContentOnly()   
         {
           
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.message = Message.content(MSG_CONTENT);

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
    
       [TestMethod]
        public void sendMessageContentAndTitle() 
       {
            //PushPayload payload = PushPayload.newBuilder()
            //        .setAudience(Audience.all())
            //        .setPlatform(Platform.all())
            //        .setMessage(Message.newBuilder()
            //                .setTitle("message title")
            //                .setContentType("content type")
            //                .setMsgContent(MSG_CONTENT).build())
            //        .build();
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.message = Message.content(MSG_CONTENT).setTitle("message title").setContentType("ontent type");

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
    
       [TestMethod]
        public void sendMessageContentAndExtras() 
       {
            
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.message = Message.content(MSG_CONTENT).AddExtras("key1", "value1").AddExtras("key2", 222).AddExtras("key3", false);

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
    
    }
}
