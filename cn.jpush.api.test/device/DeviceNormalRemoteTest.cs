using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using cn.jpush.api.common.resp;
using cn.jpush.api.device;

namespace cn.jpush.api.test.device
{
    [TestClass]
    public class DeviceNormalRemoteTest : BaseTest
    {

        [TestMethod]
        [Priority(99)]
        [TestInitialize]
        public void testUpdateDeviceTagAlias_add_tags()
        {
            HashSet<String> tagsToAdd = new HashSet<String>();
            tagsToAdd.Add("tag1");
            tagsToAdd.Add("tag2");
            tagsToAdd.Add("tag3");
            tagsToAdd.Add("tag4");
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);

            DefaultResult result = pushClient.updateDeviceTagAlias(REGISTRATION_ID1, "alias1", MOBILE, tagsToAdd, null);
            Assert.IsTrue(result.isResultOK());
        }
        [TestMethod]
        [Priority(100)]
        public void testUpdateDeviceTagAlias_add_remove_tags() 
        {
            HashSet<String> tagsToAdd = new HashSet<String>(); 
		    tagsToAdd.Add("tag1");
            tagsToAdd.Add("tag2");
            HashSet<String> tagsToRemove = new HashSet<String>();
            tagsToRemove.Add("tag3");
            tagsToRemove.Add("tag4");
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            
            DefaultResult result = pushClient.updateDeviceTagAlias(REGISTRATION_ID1, "alias1", MOBILE, tagsToAdd, tagsToRemove);
            Assert.IsTrue(result.isResultOK());
	    }
        [TestMethod]
        [Priority(110)]
        public void testGetDeviceTagAlias_1()
        {
            testUpdateDeviceTagAlias_add_remove_tags();

            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            TagAliasResult result = pushClient.getDeviceTagAlias(REGISTRATION_ID1);

            Assert.IsTrue(result.isResultOK());
            Assert.AreEqual("alias1", result.alias);
            Assert.IsTrue(result.tags.Contains("tag1"));
            Assert.IsTrue(result.tags.Contains("tag2"));
            Assert.IsTrue(!result.tags.Contains("tag3"));
            Assert.IsTrue(!result.tags.Contains("tag4"));

	    }
        [TestMethod]
        [Priority(111)]
        public void testGetAliasDeviceList_1()
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            AliasDeviceListResult result = pushClient.getAliasDeviceList("alias1", null);
            Assert.IsTrue(result.registration_ids.Contains(REGISTRATION_ID1));
	    }
        [TestMethod]
        [Priority(120)]
        public void testUpdateDeviceTagAlias_clear() 
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            DefaultResult result = pushClient.updateDeviceTagAlias(REGISTRATION_ID1, true, true);
            Assert.IsTrue(result.isResultOK());
	    }
        [TestMethod]
        [Priority(130)]
        public void testGetDeviceTagAlias_cleard() 
        {
            testUpdateDeviceTagAlias_clear();
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            TagAliasResult result = pushClient.getDeviceTagAlias(REGISTRATION_ID1);

            Assert.IsTrue(result.isResultOK());
            Assert.AreEqual(null, result.alias);
            Assert.AreEqual( 0, result.tags.Count);

	    }
        [TestMethod]
        [Priority(203)]
        public void testAddRemoveDevicesFromTag() 
         {
		   HashSet<String> toAddUsers  = new HashSet<String>();
		   toAddUsers.Add(REGISTRATION_ID1);
		   HashSet<String> toRemoveUsers  = new HashSet<String>();
           toRemoveUsers.Add(REGISTRATION_ID2);

           JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
           DefaultResult result = pushClient.addRemoveDevicesFromTag("tag3", toAddUsers, toRemoveUsers);
           Assert.IsTrue(result.isResultOK());
	    }
        [TestMethod]
        [Priority(210)]
        public void testIsDeviceInTag()  
        {
            testAddRemoveDevicesFromTag();
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            BooleanResult result = pushClient.isDeviceInTag("tag3", REGISTRATION_ID1);
            Assert.IsTrue( result.result);
            result = pushClient.isDeviceInTag("tag3", REGISTRATION_ID2);
            Assert.IsTrue( !result.result);
	    }
        [TestMethod]
        [Priority(211)] 
	    public void testAddRemoveDevicesFromTagResult() 
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            TagListResult result = pushClient.getTagList();
            Assert.IsTrue(result.tags.Contains("tag3"));
	    }
        [TestMethod]
        [Priority(220)] 
        public void testGetTagList_1()
         {
             JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
             TagListResult result = pushClient.getTagList();
             Assert.IsTrue(result.tags.Count>0);
	    }

        [TestMethod]
        [Priority(230)] 
        public void testGetAliasDevices_1()
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            AliasDeviceListResult result = pushClient.getAliasDeviceList("alias1", null);
            Assert.IsTrue(result.isResultOK());
        }
        [TestMethod]
        [Priority(250)] 
        public void testDeleteTag() 
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            DefaultResult result = pushClient.deleteTag("tag3", null);
            Assert.IsTrue(result.isResultOK());
	    }
        [TestMethod]
        [Priority(251)] 
        public void testDeleteResult() 
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            TagListResult result = pushClient.getTagList();
            Assert.IsTrue(result.isResultOK());
	    }
        [TestMethod]
        [Priority(260)] 
        public void testDeletetag2() 
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            DefaultResult result = pushClient.deleteTag("tag2", null);
            Assert.IsTrue(result.isResultOK());
	    }
        [TestMethod]
        [Priority(300)] 
        public void testGetAliasDeviceList() 
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            AliasDeviceListResult result = pushClient.getAliasDeviceList("alias1", "android");
            Assert.IsTrue(result.isResultOK());
        }
        [TestMethod]
        [Priority(310)]  
	    public void testGetAliasDeviceList_2()  
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            AliasDeviceListResult result = pushClient.getAliasDeviceList("alias2", null);
            Assert.IsTrue(result.registration_ids.Count == 0);
	    }
        [TestMethod]
        [Priority(320)]
        public void testDeleteAlias() 
         {
             JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
             DefaultResult result = pushClient.deleteAlias("alias1", "android");
             Assert.IsTrue(result.isResultOK());
	    }
        [TestMethod]
        [Priority(330)]
        public void testDeleteAlias_2() 
        {
            JPushClient pushClient = new JPushClient(APP_KEY, MASTER_SECRET);
            DefaultResult result = pushClient.deleteAlias("alias2", null);
            Assert.IsTrue(result.isResultOK());
        }
    }
}
