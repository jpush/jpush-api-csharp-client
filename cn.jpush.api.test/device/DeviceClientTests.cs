using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.test;
using cn.jpush.api.common;

namespace cn.jpush.api.device.Tests
{
    [TestClass()]
    public class DeviceClientTests : BaseTest
    {
        [TestMethod()]
        public void DeviceClientValidTest()
        {
            DeviceClient client = new DeviceClient(APP_KEY, MASTER_SECRET);
            Assert.IsInstanceOfType(client,typeof(DeviceClient));
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void DeviceClientEmptyTest()
        {
            DeviceClient client = new DeviceClient("", "");
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void getDeviceTagAliasEmptyTest()
        {
            DeviceClient client = new DeviceClient(APP_KEY, MASTER_SECRET);
            client.getDeviceTagAlias("");
        }

        [TestMethod()]
        public void getDeviceTagAliasTest()
        {
            DeviceClient client = new DeviceClient(APP_KEY, MASTER_SECRET);
            client.getDeviceTagAlias(REGISTRATION_ID1);
        }

        [TestMethod()]
        public void updateDeviceNullTest()
        {
            DeviceClient client = new DeviceClient(APP_KEY, MASTER_SECRET);
            client.updateDevice(REGISTRATION_ID1,null,null, TAG_HASHSET_EMPTY, TAG_HASHSET_EMPTY);
        }

        [TestMethod()]
        public void updateDeviceEmptyTest()
        {
            DeviceClient client = new DeviceClient(APP_KEY, MASTER_SECRET);
            client.updateDevice(REGISTRATION_ID1, "", "13888888888", TAG_HASHSET_EMPTY, TAG_HASHSET_EMPTY);
        }

        [TestMethod()]
        [ExpectedException(typeof(APIRequestException))]
        public void updateDeviceInvalidMobileTest()
        {
            DeviceClient client = new DeviceClient(APP_KEY, MASTER_SECRET);
            client.updateDevice(REGISTRATION_ID1, "alias", "13", TAG_HASHSET_EMPTY, TAG_HASHSET_EMPTY);
        }
    }
}