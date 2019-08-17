using System;
using Jiguang.JPush;
using Jiguang.JPush.Model;
using System.Collections.Generic;

namespace Example
{
    class Example
    {
        private static JPushClient client = new JPushClient(ExampleConfig.APP_KEY, ExampleConfig.MASTER_SECRET);

        public static void Main(string[] args)
        {
            ExecutePushExample();
            ExecuteBatchPushExample();
            ExecuteDeviceExample();
            ExecuteReportExample();
            ExecuteReceivedDetailReportExample();
            ExecuteMessagesDetialReportExample();
            ExecuteScheduleExample();

            Console.ReadLine();
        }

        private static void ExecutePushExample()
        {
            PushPayload pushPayload = new PushPayload()
            {
                Platform = new List<string> { "android", "ios" },
                Audience = "all",
                Notification = new Notification
                {
                    Alert = "hello jpush",
                    Android = new Android
                    {
                        Alert = "android alert",
                        Title = "title"
                    },
                    IOS = new IOS
                    {
                        Alert = "ios alert",
                        Badge = "+1"
                    }
                },
                Message = new Message
                {
                    Title = "message title",
                    Content = "message content",
                    Extras = new Dictionary<string, string>
                    {
                        ["key1"] = "value1"
                    }
                },
                Options = new Options
                {
                    IsApnsProduction = true // 设置 iOS 推送生产环境。不设置默认为开发环境。
                }
            };
            var response = client.SendPush(pushPayload);
            Console.WriteLine(response.Content);
        }

        private static void ExecuteBatchPushExample()
        {
            SinglePayload singlePayload = new SinglePayload()
            {
                Platform = new List<string> { "android", "ios" },
                Target = "flink",
                Notification = new Notification
                {
                    Alert = "hello jpush",
                    Android = new Android
                    {
                        Alert = "android alert",
                        Title = "title"
                    },
                    IOS = new IOS
                    {
                        Alert = "ios alert",
                        Badge = "+1"
                    }
                },
                Message = new Message
                {
                    Title = "message title",
                    Content = "message content",
                    Extras = new Dictionary<string, string>
                    {
                        ["key1"] = "value1"
                    }
                },
                Options = new Options
                {
                    IsApnsProduction = true // 设置 iOS 推送生产环境。不设置默认为开发环境。
                }
            };
            List<SinglePayload> singlePayloads = new List<SinglePayload>();
            singlePayloads.Add(singlePayload);
            Console.WriteLine("start send");
            var response = client.BatchPushByAlias(singlePayloads);
            Console.WriteLine(response.Content);
        }

        private static void ExecuteDeviceExample()
        {
            var registrationId = "12145125123151";
            var devicePayload = new DevicePayload
            {
                Alias = "alias1",
                Mobile = "12300000000",
                Tags = new Dictionary<string, object>
                {
                    { "add", new List<string>() { "tag1", "tag2" } },
                    { "remove", new List<string>() { "tag3", "tag4" } }
                }
            };
            var response = client.Device.UpdateDeviceInfo(registrationId, devicePayload);
            Console.WriteLine(response.Content);
        }

        private static void ExecuteReportExample()
        {
            var response = client.Report.GetMessageReport(new List<string> { "1251231231" });
            Console.WriteLine(response.Content);
        }

        private static void ExecuteReceivedDetailReportExample()
        {
            var response = client.Report.GetReceivedDetailReport(new List<string> { "1251231231" });
            Console.WriteLine(response.Content);
        }

        private static void ExecuteMessagesDetialReportExample()
        {
            var response = client.Report.GetMessagesDetailReport(new List<string> { "1251231231" });
            Console.WriteLine(response.Content);
        }

        private static void ExecuteScheduleExample()
        {
            var pushPayload = new PushPayload
            {
                Platform = "all",
                Notification = new Notification
                {
                    Alert = "Hello JPush"
                }
            };
            var trigger = new Trigger
            {
                StartDate = "2017-08-03 12:00:00",
                EndDate = "2017-12-30 12:00:00",
                TriggerTime = "12:00:00",
                TimeUnit = "week",
                Frequency = 2,
                TimeList = new List<string>
                {
                    "wed", "fri"
                }
            };
            var response = client.Schedule.CreatePeriodicalScheduleTask("task1", pushPayload, trigger);
            Console.WriteLine(response.Content);
        }
    }
}