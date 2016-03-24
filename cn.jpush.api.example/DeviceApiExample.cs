using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.device;
using cn.jpush.api.report;
using cn.jpush.api.common;
using cn.jpush.api.util;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using cn.jpush.api.common.resp;

namespace cn.jpush.api.example
{
    class DeviceApiExample
    {
        public static String TITLE = "Test from C# v3 sdk";
        public static String ALERT = "Test from  C# v3 sdk - alert";
        public static String MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        
        public static String REGISTRATION_ID = "1507bfd3f7c466c355c";
        public static String TAG = "tag_api";
        public static HashSet<String> TAG_HASHSET =new HashSet<string> { "tag_api" };
        public static HashSet<String> TAG_HASHSET_REMOVE = new HashSet<string> { "tag1", "tag" };
        public static String ALIAS = "alias";
        //your test phone number
        public static string MOBILE = "18888888888";
        public static string INVALID_MOBILE = "18888888888";
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "8aae478411e89f7682ed5af6";


        static void Main(string[] args)
                {
                    Console.WriteLine("*****开始获取设备信息******");
                    DeviceClient client = new DeviceClient(app_key, master_secret);
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
                        Console.WriteLine("Error Message: " + e.ErrorCode);
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
                        Console.WriteLine("Error Message: " + e.ErrorCode);
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
                        Console.WriteLine("Error Message: " + e.ErrorCode);
                    }
                    catch (APIConnectionException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                    var result = client.updateDeviceTagAlias(REGISTRATION_ID,
                                                           ALIAS,
                                                           MOBILE,
                                                           TAG_HASHSET,
                                                           TAG_HASHSET);
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
                        Console.WriteLine("Error Message: " + e.ErrorCode);
                    }
                    catch (APIConnectionException e)
                    {
                        Console.WriteLine(e.Message);
                    }
            //
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
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            //update the device  set the alias,mobile
            try
            {
                var result = client.updateDeviceTagAlias(REGISTRATION_ID,
                                                       ALIAS,
                                                       MOBILE,
                                                       TAG_HASHSET,
                                                       TAG_HASHSET);
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
                Console.WriteLine("Error Message: " + e.ErrorCode);
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
                Console.WriteLine("Error Message: " + e.ErrorCode);
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

