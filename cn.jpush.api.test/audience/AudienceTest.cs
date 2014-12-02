using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using cn.jpush.api.common;

namespace cn.jpush.api.test.audience
{
    [TestClass]
    public class AudienceTest
    {
        [TestMethod]
        public void testAll()
        {
            Audience audience = Audience.all();
            string jsonText;
            using (StringWriter sw = new StringWriter())
            {
                JsonWriter writer = new JsonTextWriter(sw);
                writer.WriteValue("all");
                writer.Flush();
                jsonText= sw.GetStringBuilder().ToString();
            }
            var jsonString = JsonConvert.SerializeObject(audience, new AudienceConverter());

            Assert.AreEqual(jsonText, jsonString);
        }
        [TestMethod]
        public void testAudience()
        {
            JObject json = new JObject();
            JArray arrary = new JArray();
            arrary.Add(JToken.FromObject("aaaa"));
            json.Add("alias", arrary);

            var audience = Audience.s_alias("aaaa");
            var jsonString = JsonConvert.SerializeObject(audience, new AudienceConverter()).Replace("\r\n", "").Replace(" ", ""); ;
            var jsonObject = json.ToString().Replace("\r\n", "").Replace(" ", "");

            Assert.AreEqual(jsonObject, jsonString);
        }

    }
}
