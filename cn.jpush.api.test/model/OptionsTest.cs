using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.mode;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using cn.jpush.api.common;

namespace cn.jpush.api.test.model
{
    [TestClass]
    public class OptionsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegalSendno()
        {

            Options options = new Options();
            options.sendno = -1;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegalOverrideMsgId()
        {
            Options options = new Options();
            options.override_msg_id = -1;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegalTimeToLive()
        {
            Options options = new Options();
            options.time_to_live = -2;
        }
        [TestMethod]
        public void testSendno() 
        {

            var json = new JObject();
            json.Add("sendno",JToken.FromObject(111));
            json.Add("apns_production", JToken.FromObject(false));

            Options options = new Options();
            options.sendno = 111;
            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(options, jSetting);
            var jsonObject = json.ToString(Formatting.None); 
            Assert.AreEqual(jsonObject, jsonString);
       }
        [TestMethod]
        public void testTimeToLive_int() 
        {
            var json = new JObject();
            json.Add("sendno",JToken.FromObject(111));
            json.Add("time_to_live", JToken.FromObject(640));
            json.Add("apns_production", JToken.FromObject(false));
        
            Options options = new Options();
            options.sendno = 111;
            options.time_to_live=640;

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(options, jSetting);
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);
         }
        [TestMethod]
        public void testTimeToLive_0()
        {


            var json = new JObject();
            json.Add("sendno",JToken.FromObject(111));
            json.Add("time_to_live", JToken.FromObject(0));
            json.Add("apns_production", JToken.FromObject(false));
           

            Options options = new Options();
            options.sendno = 111;
            options.time_to_live=0;

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(options, jSetting);
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);
        }
        [TestMethod]
        public void testTimeToLive_default() 
        {
            JObject json = new JObject();
            json.Add("sendno", JToken.FromObject(111));
            json.Add("apns_production", JToken.FromObject(false));
        
            Options options = new Options();
            options.sendno=111;
            options.time_to_live=-1;

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(options, jSetting);
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);

        }
        [TestMethod]
        public void testApnsProduction_defaultFalse() 
         {
            int sendno = ServiceHelper.generateSendno();

            JObject json = new JObject();
            json.Add("sendno", JToken.FromObject(sendno));
            json.Add("apns_production", JToken.FromObject(false));
           

            Options options = new Options();
            options.sendno = sendno;

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(options, jSetting);
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);
        }
        [TestMethod]
        public void testApnsProduction_True()
        {
            int sendno = ServiceHelper.generateSendno();

            JObject json = new JObject();
            json.Add("sendno", JToken.FromObject(sendno));
            json.Add("apns_production", JToken.FromObject(true));
         
        
            Options options = new Options();
            options.sendno = sendno;
            options.apns_production=true;

            var jSetting = new JsonSerializerSettings();
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(options, jSetting);
            var jsonObject = json.ToString(Formatting.None);

            Assert.AreEqual(jsonObject, jsonString);
     }
        [TestMethod]
        public void testBigPushDuration() 
      {
             int sendno = ServiceHelper.generateSendno();
             JObject json = new JObject();
             json.Add("sendno", JToken.FromObject(sendno));
             json.Add("big_push_duration", JToken.FromObject(10));
             json.Add("apns_production", JToken.FromObject(false));
       
             Options options = new Options();
             options.sendno = sendno;
             options.big_push_duration=10;

             var jSetting = new JsonSerializerSettings();
             jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
             var jsonString = JsonConvert.SerializeObject(options, jSetting);
             var jsonObject = json.ToString(Formatting.None);

             Assert.AreEqual(jsonString, jsonObject);

    }

    }
}
