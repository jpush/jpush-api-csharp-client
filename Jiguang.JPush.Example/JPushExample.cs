using Jiguang.JPush.Model;
using System;

namespace Jiguang.JPush.Example
{
    public class JPushExample
    {
        public static void Main(string[] args)
        {
            JPushClient client = new JPushClient("b99f062ffc07bc9b3a4e92d7", "5a30a306ea8096212dc52b30");

            PushPayload notification = new PushPayload
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
                    Title = "message title"
                }
            };

            var response = client.IsPushValid(notification).Result;
            Console.WriteLine(response.Content);

            Console.ReadLine();
        }
    }
}