using System;
using System.Collections.Generic;
using cn.jpush.api.device;
using cn.jpush.api.common;
using cn.jpush.api.common.resp;

namespace cn.jpush.api.example
{
    class DeviceApiExample
    {
        // 首先运行DeviceApiExample，为设备添加手机号码，标签，别名，再运行 JPushApiExample, ScheduleApiExample
        // 步骤如下：
        // 1.设置 cn.jpush.api.example 为启动项
        // 2.在 cn.jpush.api.example 项目，右键选择属性，然后选择应用程序，最后在启动对象下拉框中选择 DeviceApiExample
        // 3.按照 2 的步骤设置，运行 JPushApiExample,ScheduleApiExample.

        public static string TITLE = "Test from C# v3 sdk";
        public static string ALERT = "Test from  C# v3 sdk - alert";
        public static string MSG_CONTENT = "Test from C# v3 sdk - msgContent";

        public static string REGISTRATION_ID = "1507bfd3f7c466c355c";
        public static string TAG = "tag_api";
        public static HashSet<String> TAG_HASHSET = new HashSet<string> { TAG1, TAG2, TAG3, TAG_ALL, TAG_NO };
        public static HashSet<String> TAG_HASHSET_REMOVE = new HashSet<string> { TAG_NO };
        public static string ALIAS = ALIAS1;

        // Your test phone number.
        public static string MOBILE = "18688888888";
        public static string INVALID_MOBILE = "1868888888888";

        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a564b268ba23631a8a34e687";

        public const string TAG1 = "audience_tag1";
        public const string TAG2 = "audience_tag2";
        public const string TAG3 = "@!#$&*+=.|";
        public const string TAG_ALL = "audience_tag_all";
        public const string TAG_NO = "audience_tag_no";
        public const string ALIAS1 = "audience_alias1";
        public const string ALIAS2 = "audience_alias2";
        public const string ALIAS_NO = "audience_alias_no";

        public const string REGISTRATION_ID1 = "1507bfd3f7c466c355c";
        public const string REGISTRATION_ID2 = "1a0018970aa5d80f3b8";

        public static string[] REGISTRATION_IDS = { REGISTRATION_ID1, REGISTRATION_ID2 };

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始获取设备信息******");

            DeviceClient client = new DeviceClient(app_key, master_secret);
            try
            {
                var result = client.getDeviceTagAlias(REGISTRATION_ID);
                // 由于统计数据并非即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
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

            // Update device's alias, mobile, tag.
            try
            {
                var result = client.updateDevice(REGISTRATION_ID, ALIAS, MOBILE, TAG_HASHSET, TAG_HASHSET_REMOVE);
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

            // Get tag list.
            try
            {
                var result = client.getTagList();
                // 由于统计数据并非即时的,所以等待一小段时间再执行下面的获取结果方法
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

            // Get device's tag and alias.
            try
            {
                var result = client.getDeviceTagAlias(REGISTRATION_ID);
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

            try
            {
                var result = client.updateDeviceTagAlias(REGISTRATION_ID, true, true);
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

            try
            {
                var result = client.getDeviceTagAlias(REGISTRATION_ID);
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
            Console.WriteLine("*****结束获取设备信息******");

            try
            {
                var result = client.getDeviceStatus(REGISTRATION_IDS);
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
            Console.WriteLine("*****结束获取设备信息******");
        }
    }
}

