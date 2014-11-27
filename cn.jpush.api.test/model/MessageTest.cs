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
            Message message = new Message(null);
            message.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegalOfEmpty()
        {
            Message message = new Message("");
            message.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegalOfConstructNull()
        {
            Message message = new Message(null,null,null);
            message.Check();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegalOfConstructEmpty()
        {
            Message message = new Message("",null,null);
            message.Check();
        }
        [TestMethod]
        public void testMsgContent() 
        {
            Message message = new Message("msg content");
        
            JObject json = new JObject();
            json.Add("msg_content",JToken.FromObject("msg content"));

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(message, jSetting).Replace("\r\n", "").Replace(" ", ""); ;
            var jsonObject = json.ToString().Replace("\r\n", "").Replace(" ", "");

            Assert.AreEqual(jsonObject, jsonString);
            
        }
        [TestMethod]
        public void testMsgContentAndExtras()
        {
            Message message = new Message("msgContent");
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
            var jsonString = JsonConvert.SerializeObject(message, jSetting).Replace("\r\n", "").Replace(" ", ""); ;
            var jsonObject = json.ToString().Replace("\r\n", "").Replace(" ", "");
            var fromJson = JsonConvert.DeserializeObject<Message>(jsonObject);
            Assert.AreEqual(jsonObject, jsonString);
     }
          
    
    }
}
