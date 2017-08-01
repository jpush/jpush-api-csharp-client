using Jiguang.JPush.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiguang.JPush.Example
{
    public class JPushExample
    {
        public static void Main(string[] args)
        {
            JPushClient client = new JPushClient("b99f062ffc07bc9b3a4e92d7", "5a30a306ea8096212dc52b30");

            //Audience =
            //{
            //    Tag = new List<string> { "1", "2" }
            //}
            PushPayload notification = new PushPayload
            {
                Platform = "android",
                Notification = {
                    Alert = "hello world"
                }
            };

            //Task.Run(async () =>
            //{
            //    client.SendAsync(notification)
            //});

            var response = client.Send(notification).Result;
            Console.WriteLine(response.Content);

            Console.ReadLine();
        }
    }
}