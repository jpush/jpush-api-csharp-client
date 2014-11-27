using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using cn.jpush.api.common;
using System.IO;

namespace cn.jpush.api.test.model
{
    [TestClass]
    public class PlatformTest
    {
        [TestMethod]
        public void testAll()
        {
           
            Platform all = Platform.all();
            string jsonText;
            using (StringWriter sw = new StringWriter())
            {
                JsonWriter writer = new JsonTextWriter(sw);
                writer.WriteValue("all");
                writer.Flush();
                jsonText= sw.GetStringBuilder().ToString();
            }
            var jsonString = JsonConvert.SerializeObject(all, Formatting.None,new PlatformConverter()).Replace("\r\n", "").Replace(" ", ""); ;


            Assert.AreEqual(jsonText, jsonString);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testNotAll() 
        {
            Platform notPlatfom = new Platform(false);
            var jsonString = JsonConvert.SerializeObject(notPlatfom, Formatting.None, new PlatformConverter()).Replace("\r\n", "").Replace(" ", ""); ;

        }
         [TestMethod]
        public void testAndroid() 
        {
            Platform android=new Platform();
            android.deviceTypes = new System.Collections.Generic.HashSet<string>();
            android.deviceTypes.Add(DeviceType.android.ToString());


            JArray array = new JArray();
            array.Add(JToken.FromObject("android"));

            var jsonString = JsonConvert.SerializeObject(android, Formatting.None, new PlatformConverter()).Replace("\r\n", "").Replace(" ", ""); ;

            var jsonArray  = array.ToString().Replace("\r\n", "").Replace(" ", ""); 

            Assert.AreEqual(jsonArray, jsonString);

       }
    }
}
