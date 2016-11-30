using System;
using System.Collections.Generic;
using cn.jpush.api.device;
using cn.jpush.api.common;
using cn.jpush.api.common.resp;

namespace cn.jpush.api.example
{
    class DeviceApiExample
    {
        //
        //run the DeviceApiExample first,it will add mobile,tags,alias to the device:
        //首先运行DeviceApiExample，为设备添加手机号码，标签，别名，再运行JPushApiExample,ScheduleApiExample。
        //步骤如下：
        //1.设置cn.jpush.api.example为启动项
        //2.在cn.jpush.api.example项目，右键选择属性，然后选择应用程序，最后在启动对象下拉框中选择DeviceApiExample
        //3.按照2的步骤设置，运行JPushApiExample,ScheduleApiExample.

        public static String TITLE = "Test from C# v3 sdk";
        public static String ALERT = "Test from  C# v3 sdk - alert";
        public static String MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        
        public static String REGISTRATION_ID = "1507bfd3f7c466c355c";
        public static String TAG = "tag_api";
        public static HashSet<String> TAG_HASHSET =new HashSet<string> { TAG1, TAG2, TAG3, TAG_ALL, TAG_NO };
        public static HashSet<String> TAG_HASHSET_REMOVE = new HashSet<string> { TAG_NO };
        public static String ALIAS = ALIAS1;
        //your test phone number
        public static string MOBILE = "18688888888";
        public static string INVALID_MOBILE = "1868888888888";

        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a564b268ba23631a8a34e687";


        public const String TAG1 = "audience_tag1";
        public const String TAG2 = "audience_tag2";
        public const String TAG3 = "@!#$&*+=.|";
        public const String TAG_ALL = "audience_tag_all";
        public const String TAG_NO = "audience_tag_no";
        public const String ALIAS1 = "audience_alias1";
        public const String ALIAS2 = "audience_alias2";
        public const String ALIAS_NO = "audience_alias_no";

        public const String REGISTRATION_ID1 = "1507bfd3f7c466c355c";
        public const String REGISTRATION_ID2 = "1a0018970aa5d80f3b8";

        public static String[] REGISTRATION_IDS = { REGISTRATION_ID1, REGISTRATION_ID2};

        public static void Main(string[] args)
         {
             Console.WriteLine("*****开始获取设备信息******");
             DeviceClient client = new DeviceClient(app_key, master_secret);
            //get device tag alias
            try
            {
                var result = client.getDeviceTagAlias(REGISTRATION_ID);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                //如需查询上次推送结果执行下面的代码
                Console.WriteLine(result);
                //如需查询某个messageid的推送结果执行下面的代码
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

           // update  Device ALIAS,MOBILE,TAG
            try
            {
                var result = client.updateDevice(REGISTRATION_ID,
                                                       ALIAS,
                                                       MOBILE,
                                                       TAG_HASHSET,
                                                       TAG_HASHSET_REMOVE
                                                       );
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                //如需查询上次推送结果执行下面的代码
                Console.WriteLine(result);
                //如需查询某个messageid的推送结果执行下面的代码


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

            //get tag list demo
            try
            {
                        var result = client.getTagList();
                        //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                        System.Threading.Thread.Sleep(10000);
                        //如需查询上次推送结果执行下面的代码
                        Console.WriteLine(result);
                       //如需查询某个messageid的推送结果执行下面的代码


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

                    //get device tag alias
                    try
                    {
                        var result = client.getDeviceTagAlias(REGISTRATION_ID);
                        //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                        System.Threading.Thread.Sleep(10000);
                        //如需查询上次推送结果执行下面的代码
                        Console.WriteLine(result);
                        //如需查询某个messageid的推送结果执行下面的代码


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
                        //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                        System.Threading.Thread.Sleep(10000);
                        //如需查询上次推送结果执行下面的代码
                        Console.WriteLine(result);
                        //如需查询某个messageid的推送结果执行下面的代码
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

            //get the device tag alias mobile
            try
            {
                var result = client.getDeviceTagAlias(REGISTRATION_ID);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                //如需查询上次推送结果执行下面的代码
                Console.WriteLine(result);
                //如需查询某个messageid的推送结果执行下面的代码
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

            //TagAliasResult getDeviceTagAlias(String registrationId)
            Console.WriteLine("*****结束获取设备信息******");


            //get the device tag alias mobile
            try
            {
                var result = client.getDeviceStatus(REGISTRATION_IDS);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                //如需查询上次推送结果执行下面的代码
                Console.WriteLine(result);
                //如需查询某个messageid的推送结果执行下面的代码
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

            //TagAliasResult getDeviceTagAlias(String registrationId)
            Console.WriteLine("*****结束获取设备信息******");
        }
    }
}

