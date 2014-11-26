using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using cn.jpush.api.common;

namespace cn.jpush.api.test.model
{
    [TestClass]
    public class PlatformTest
    {
        [TestMethod]
       
        public void testAll()
        {
           
            Platform all = Platform.all();

            JToken jobject = JToken.FromObject("all");
           
            var jSetting = new JsonSerializerSettings();
            //jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(all, Formatting.None,new PlatformConverter()).Replace("\r\n", "").Replace(" ", ""); ;
            var jsonObject = jobject.ToString().Replace("\r\n", "").Replace(" ", "");

            Assert.AreEqual(jsonObject, jsonString);
        }
    }
}
