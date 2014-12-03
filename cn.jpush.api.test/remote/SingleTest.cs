using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class SingleTest:BaseRemotePushTest
    {
        [TestMethod]
       public void sendSimpleMessageAndNotification_Pall() 
      {
          
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all(); 
            payload.audience = Audience.all();
            payload.notification = new Notification().setAlert("\U0001F604");
            payload.message =  Message.content("Pall Nall Mall alert");

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
    }
}
