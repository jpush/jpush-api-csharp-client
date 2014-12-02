using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class AlertOverrideTest:BaseRemotePushTest
    {

       
        [TestMethod]
       public void sendAlert_all()
       {
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.all();

            payload.notification = new Notification()
                               .setAlert("alert")
                               .setAndroid(new AndroidNotification().setAlert("android alert"))
                               .setIos(new IosNotification().setAlert("ios alert"))
                               .setWinphone(new WinphoneNotification().setAlert("winphone alert"));

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
        
        
    }
}
