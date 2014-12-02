using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using cn.jpush.api.push.mode;

namespace cn.jpush.api.test.remote
{
    [TestClass]
    public class AudienceTest : BaseRemotePushTest
    {
        public const String TAG1 = "audience_tag1";
        public const String TAG2 = "audience_tag2";
        public const String TAG_ALL = "audience_tag_all";
        public const String TAG_NO = "audience_tag_no";
        public const String ALIAS1 = "audience_alias1";
        public const String ALIAS2 = "audience_alias2";
        public const String ALIAS_NO = "audience_alias_no";

        public AudienceTest()
        {
            HashSet<String> tags1 = new HashSet<String>();
    	    tags1.Add(TAG1);
    	    tags1.Add(TAG_ALL);

    	    HashSet<String> tags2 = new HashSet<String>();
    	    tags1.Add(TAG2);
    	    tags1.Add(TAG_ALL);
    	
    	    JPushClient jpushClient = new JPushClient(APP_KEY,MASTER_SECRET);
    	    var result = jpushClient.updateDeviceTagAlias(REGISTRATION_ID1, ALIAS1, tags1, null);
    	    Assert.IsTrue(result.isResultOK());
    	
    	    result = jpushClient.updateDeviceTagAlias(REGISTRATION_ID2, ALIAS2, tags2, null);
    	    Assert.IsTrue(result.isResultOK());

        }

        [TestMethod]
        public void sendByTag()
        {
            //PushPayload payload = PushPayload.newBuilder()
            //        .setPlatform(Platform.all())
            //        .setAudience(Audience.tag(TAG1))
            //        .setNotification(Notification.alert(ALERT))
            //        .build();

            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.s_tag(TAG1);
            payload.notification = new Notification().setAlert("alert");

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
        [TestMethod]
         public void sendByTagAnd() 
        {
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.s_tag_and(ALIAS1);
            payload.notification = new Notification().setAlert(ALERT);

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
        [TestMethod]
        public void sendByAlias() 
        {
           
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.s_alias(ALIAS1);
            payload.notification = new Notification().setAlert(ALERT);

            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
         }
        [TestMethod]
         public void sendByRegistrationID() 
        {
            
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();
            payload.audience = Audience.s_registrationId(REGISTRATION_ID1);
            payload.notification = new Notification().setAlert(ALERT);
            var result = _client.SendPush(payload);
            Assert.IsTrue(result.isResultOK());
        }
       //  @Test
    //public void sendByTagMore() throws Exception {
    //    PushPayload payload = PushPayload.newBuilder()
    //            .setPlatform(Platform.all())
    //            .setAudience(Audience.tag(TAG1, TAG2))
    //            .setNotification(Notification.alert(ALERT))
    //            .build();
    //    PushResult result = _client.sendPush(payload);
    //    assertTrue(result.isResultOK());
    //}
    
    }
}
