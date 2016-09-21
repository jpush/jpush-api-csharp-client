using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.example
{
    class ReportsExample
    {
         public static String app_key = "cb5029879e49566e2bb30e8a";
         public static String master_secret = "0aec091b50bea4970e2650c4";

         public static void testGetReport()
         {
             JPushClient jpushClient = new JPushClient(app_key, master_secret);
             try
             {
                 var result = jpushClient.getReceivedApi("991969761");
                 Console.WriteLine("Got result - " + result.ToString());

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
         }
         /*用户统计vip专用接口*/
         public static void testGetUsers()
         {
             JPushClient jpushClient = new JPushClient(app_key, master_secret);
             try
             {
                 var result = jpushClient.getReportUsers(TimeUnit.DAY, "2016-06-10", 3);
                 Console.WriteLine("Got result - " + result.ToString());

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
         }
         /*消息统计vip专用接口*/
         public static void testGetMessages()
         {
             JPushClient jpushClient = new JPushClient(app_key, master_secret);
             try
             {
                 var result = jpushClient.getReportMessages("991969761");
                 Console.WriteLine("Got result - " + result.ToString());

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

         }
        public static void Main(string[] args)
        {
            ReportsExample.testGetReport();
            ReportsExample.testGetMessages();
            ReportsExample.testGetUsers();
        }

    }
}
