using System;
using Jiguang.JPush;
using Jiguang.JPush.Model;
using System.Collections.Generic;

namespace Example
{
    class Example
    {
        private static JPushClient client = new JPushClient("Your AppKey", "Your MasterSecret");

        public static void Main(string[] args)
        {
            ExecutePushExample();
            ExecuteDeviceEample();
            ExecuteReportExample();
            ExecuteScheduleExample();

            Console.ReadLine();
        }

        private static void ExecutePushExample()
        {
            PushPayload pushPayload = new PushPayload()
            {
                Platform = "android",
                Audience = "all",
                Notification = new Notification()
                {
                    Alert = "hello jpush",
                    Android = new Android()
                    {
                        Alert = "android alert",
                        Title = "title"
                    },
                    IOS = new IOS()
                    {
                        Alert = "ios alert",
                        Badge = 1
                    }
                },
                Message = new Message()
                {
                    Title = "message title",
                    Content = "message content",
                    Extras = new Dictionary<string, string>()
                    {
                        ["key1"] = "value1"
                    }
                }
            };
            var response = client.SendPushAsync(pushPayload).Result;
            Console.WriteLine(response.Content);
        }

        private static void ExecuteDeviceEample()
        {
            var registrationId = "12145125123151";
            var devicePayload = new DevicePayload()
            {
                Alias = "alias1",
                Mobile = "12300000000",
                Tags = new Dictionary<string, object>()
                {
                    { "add", new List<string>() { "tag1", "tag2" } },
                    { "remove", new List<string>() { "tag3", "tag4" } }
                }
            };
            var response = client.Device.UpdateDeviceInfoAsync(registrationId, devicePayload).Result;
            Console.WriteLine(response.Content);
        }

        private static void ExecuteReportExample()
        {
            var response = client.Report.GetMessageReportAsync(new List<string> { "1251231231" }).Result;
            Console.WriteLine(response.Content);
        }

        private static void ExecuteScheduleExample()
        {
            var pushPayload = new PushPayload
            {
                Platform = "all",
                Notification = new Notification()
                {
                    Alert = "Hello JPush"
                }
            };
            var trigger = new Trigger()
            {
                StartDate = "2017-08-03 12:00:00",
                EndDate = "2017-12-30 12:00:00",
                TriggerTime = "12:00:00",
                TimeUnit = "week",
                Frequency = 2,
                TimeList = new List<string>()
                {
                    "wed", "fri"
                }
            };
            var response = client.Schedule.CreatePeriodicalScheduleTaskAsync("task1", pushPayload, trigger).Result;
            Console.WriteLine(response.Content);
        }
    }
}