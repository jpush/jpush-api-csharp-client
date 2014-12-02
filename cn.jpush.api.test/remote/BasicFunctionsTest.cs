using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class BasicFunctionsTest:BaseRemotePushTest
    {
        [TestMethod]
        public void sendSimpleNotification_Pall_Ndefault() 
        {
	        PushPayload payload = PushPayload.AlertAll("Pall Nall default alert");
		    var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
	    }
        //[TestMethod]
        // public void sendSimpleNotification_Pandroid_Nandroid()
        //{
        //    //PushPayload payload = PushPayload.newBuilder()
        //    //        .setPlatform(Platform.android())
        //    //        .setAudience(Audience.all())
        //    //        .setNotification(Notification.newBuilder()
        //    //                .addPlatformNotification(AndroidNotification.alert("Pandroid Nandroid alert"))
        //    //                .build())
        //    //        .build();
        //    PushPayload payload = new PushPayload();
        //    payload.platform = Platform.android();

        //    var result = _client.SendPush(payload);
        //    Assert.IsTrue(result.isResultOK());
        //}
    }
}
