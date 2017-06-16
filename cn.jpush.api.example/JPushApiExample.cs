using System;
using System.Collections;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using cn.jpush.api.common.resp;

namespace cn.jpush.api.example
{
    public class JPushApiExample
    {
        // 首先运行 DeviceApiExample，为设备添加手机号码，标签别名，再运行 JPushApiExample, ScheduleApiExample，步骤如下：
        // 1.设置 cn.jpush.api.example 为启动项
        // 2.在 cn.jpush.api.example 项目，右键选择属性，然后选择应用程序，最后在启动对象下拉框中选择 DeviceApiExample
        // 3.按照 2 的步骤设置，运行 JPushApiExample, ScheduleApiExample。

        public static string TITLE = "Test from C# v3 sdk";
        public static string ALERT = "Test from C# v3 sdk - alert";
        public static string MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        public static string REGISTRATION_ID = "0900e8d85ef";
        public static string SMSMESSAGE = "Test from C# v3 sdk - SMSMESSAGE";
        public static int DELAY_TIME = 1;
        public static string TAG = "tag_api";
        public static string app_key = "e0eb67483f04f4425667d817";
        public static string master_secret = "1ea950a19875a6e2693ae312";

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始发送******");
            JPushClient client = new JPushClient(app_key, master_secret);

            DateTime dt1 = DateTime.Now;

            PushPayload payload = PushObject_All_All_Alert();
            try
            {
                var result = client.SendPush(payload);
                DateTime dt2 = DateTime.Now;

                TimeSpan ts = dt2.Subtract(dt1);
                Console.WriteLine("example1 time {0}", ts.TotalMilliseconds);

                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法。
                System.Threading.Thread.Sleep(10000);

                // 如需查询上次推送结果执行下面的代码。
                var apiResult = client.getReceivedApi(result.msg_id.ToString());
                var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());

                // 如需查询某个 messageId 的推送结果执行下面的代码。
                var queryResultWithV2 = client.getReceivedApi("1739302794");
                var querResultWithV3 = client.getReceivedApi_v3("1739302794");
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            // Send sms message
            PushPayload pushsms = PushSendSmsMessage();
            try
            {
                var result = client.SendPush(pushsms);

                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法。
                System.Threading.Thread.Sleep(10000);

                // 如需查询上次推送结果执行下面的代码。
                var apiResult = client.getReceivedApi(result.msg_id.ToString());
                var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            PushPayload payload_alias = PushObject_all_alias_alert();
            try
            {
                var result = client.SendPush(payload_alias);

                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法。
                System.Threading.Thread.Sleep(10000);

                // 如需查询上次推送结果执行下面的代码。
                var apiResult = client.getReceivedApi(result.msg_id.ToString());
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it.");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("*****结束发送******");

            // Android 平台上的通知，JPush SDK 按照一定的通知栏样式展示。
            PushPayload payload_options = PushObject_android_with_options();
            try
            {
                var result = client.SendPush(payload_options);

                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法。
                System.Threading.Thread.Sleep(10000);

                // 如需查询上次推送结果执行下面的代码。
                var apiResult = client.getReceivedApi(result.msg_id.ToString());
                var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("*****结束发送******");

            PushPayload payload_all_alias = PushObject_all_alias_alert();
            try
            {
                var result = client.SendPush(payload_alias);

                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法。
                System.Threading.Thread.Sleep(10000);

                // 如需查询上次推送结果执行下面的代码。
                var apiResult = client.getReceivedApi(result.msg_id.ToString());
                var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("*****结束发送******");

            PushPayload payload_apns_production_options = PushObject_apns_production_options();
            try
            {
                var result = client.SendPush(payload_apns_production_options);

                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法。
                System.Threading.Thread.Sleep(10000);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("*****结束发送******");

            PushPayload payload_ios_alert_json = PushObject_ios_alert_json();
            try
            {
                var result = client.SendPush(payload_ios_alert_json);
                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("*****结束发送******");

            PushPayload PushObject = PushObjectWithExtrasAndMessage();
            try
            {
                var result = client.SendPush(PushObject);
                // 由于统计数据并非即时的，所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("*****结束发送******");
        }

        public static PushPayload PushObject_All_All_Alert()
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.all(),
                audience = Audience.all(),
                notification = new Notification().setAlert(ALERT)
            };
            return pushPayload;
        }

        public static PushPayload PushObject_all_alia_alert()
        {
            PushPayload pushPayload_alias = new PushPayload()
            {
                platform = Platform.android(),
                audience = Audience.s_alias("alias1"),
                notification = new Notification().setAlert(ALERT)
            };
            return pushPayload_alias;
        }

        public static PushPayload PushObject_all_alias_alert()
        {
            PushPayload pushPayload_alias = new PushPayload()
            {
                platform = Platform.android()
            };
            string[] alias = new string[] { "alias1", "alias2", "alias3" };
            pushPayload_alias.audience = Audience.s_alias(alias);
            pushPayload_alias.notification = new Notification().setAlert(ALERT);
            return pushPayload_alias;
        }

        public static PushPayload PushObject_registrationId()
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.all()
            };
            string[] rId = new string[] { "registrationId1" };
            pushPayload.audience = Audience.s_registrationId(rId);
            pushPayload.notification = new Notification().setAlert(ALERT);
            return pushPayload;
        }

        public static PushPayload PushObject_Android_Tag_AlertWithTitle()
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.android(),
                audience = Audience.s_tag("tag1"),
                notification = Notification.android(ALERT, TITLE)
            };
            return pushPayload;
        }

        public static PushPayload PushObject_android_and_ios()
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.android_ios()
            };
            pushPayload.audience = Audience.s_tag("tag1");

            var notification = new Notification().setAlert("alert content");
            notification.AndroidNotification = new AndroidNotification().setTitle("Android Title");
            notification.IosNotification = new IosNotification();
            notification.IosNotification.incrBadge(1);
            notification.IosNotification.setMutableContent(true);
            notification.IosNotification.AddExtra("extra_key", "extra_value");
            pushPayload.notification = notification.Check();
            return pushPayload;
        }

        public static PushPayload PushObject_android_with_options()
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.android_ios()
            };
            pushPayload.audience = Audience.s_registrationId();

            AndroidNotification androidnotification = new AndroidNotification();
            androidnotification.setAlert("Push Object android with options");
            androidnotification.setBuilderID(3);
            androidnotification.setStyle(1);
            androidnotification.setAlert_type(1);
            androidnotification.setBig_text("big text content");
            androidnotification.setInbox("JSONObject");
            androidnotification.setBig_pic_path("picture url");
            androidnotification.setPriority(0);
            androidnotification.setCategory("category str");

            var notification = new Notification().setAlert("alert content");
            notification.AndroidNotification = androidnotification;
            notification.IosNotification = new IosNotification();
            notification.IosNotification.incrBadge(1);
            notification.IosNotification.AddExtra("extra_key", "extra_value");
            pushPayload.notification = notification.Check();
            return pushPayload;
        }

        public static PushPayload PushObjectWithExtrasAndMessage()
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.android_ios(),
                audience = Audience.all()
            };

            pushPayload.notification = new Notification()
            {
                IosNotification = new IosNotification()
                    .setAlert("sound default")
                    .setBadge(5)
                    .setSound("default")
                    .AddExtra("from", "JPush")
            };
            pushPayload.message = Message.content(MSG_CONTENT);
            return pushPayload;
        }

        public static PushPayload PushObject_ios_alert_json()
        {
            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.all(),
                audience = Audience.all()
            };

            Hashtable alert = new Hashtable
            {
                ["title"] = "JPush Title",
                ["subtitle"] = "JPush Subtitle",
                ["body"] = "JPush Body"
            };

            pushPayload.notification = new Notification()
            {
                IosNotification = new IosNotification()
                    .setAlert(alert)
                    .setBadge(5)
                    .setSound("happy")
                    .AddExtra("from", "JPush")
            };
            pushPayload.message = Message.content(MSG_CONTENT);
            return pushPayload;
        }

        public static PushPayload PushObject_ios_audienceMore_messageWithExtras()
        {
            var pushPayload = new PushPayload()
            {
                platform = Platform.android_ios(),
                audience = Audience.s_tag("tag1", "tag2"),
                message = Message.content(MSG_CONTENT).AddExtras("from", "JPush")
            };
            return pushPayload;
        }

        public static PushPayload PushObject_apns_production_options()
        {
            var pushPayload = new PushPayload()
            {
                platform = Platform.android_ios(),
                audience = Audience.s_tag("tag1", "tag2"),
                message = Message.content(MSG_CONTENT).AddExtras("from", "JPush")
            };
            pushPayload.options.apns_production = false;
            return pushPayload;
        }

        public static PushPayload PushSendSmsMessage()
        {
            var pushPayload = new PushPayload()
            {
                platform = Platform.all(),
                audience = Audience.all(),
                notification = new Notification().setAlert(ALERT)
            };

            SmsMessage sms_message = new SmsMessage();
            sms_message.setContent(SMSMESSAGE);
            sms_message.setDelayTime(DELAY_TIME);

            pushPayload.sms_message = sms_message;
            return pushPayload;
        }
    }
}