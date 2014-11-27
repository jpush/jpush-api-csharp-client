using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using System.IO;
using Newtonsoft.Json;

namespace cn.jpush.api.test.audience
{
    [TestClass]
    public class AudienceTest
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
            //Audience audience = Audience.all();
            //Assert.AreEqual(,);
        }
    }
}
