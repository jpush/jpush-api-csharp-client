using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace cn.jpush.api.test.model
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegalOfNull()
        {
            Message message = Message.content(null);
        }
        [TestMethod]
        public void testIllegalOfEmpty()
        {
            Message message = Message.content("");
        }
        [TestMethod]
        public void testMsgContent() 
        {
            Message message = Message.content("msg content");
        
            JObject json = new JObject();
            json.Add("msg_content",JToken.FromObject("msg content"));

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(message, jSetting);
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);
            
        }
        [TestMethod]
        public void testMsgContentAndExtras()
        {
            Message message = Message.content("msgContent");
            message.AddExtras("key1", "value1");
            message.AddExtras("key2", 222);
            message.AddExtras("key3", false);
            message.Check();

            JObject json = new JObject();
            json.Add("msg_content",JToken.FromObject("msgContent"));

            JObject extras = new JObject();
            extras.Add("key1", JToken.FromObject("value1"));
            extras.Add("key2", JToken.FromObject(222));
            extras.Add("key3", JToken.FromObject(false));

            json.Add("extras", extras);

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(message, jSetting);
            var jsonObject = json.ToString(Formatting.None);
            var fromJson = JsonConvert.DeserializeObject<Message>(jsonObject);
            Assert.AreEqual(jsonObject, jsonString);
     }
          
    
    }
}
