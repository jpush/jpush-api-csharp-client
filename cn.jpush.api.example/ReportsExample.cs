using cn.jpush.api.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.example
{
    class ReportsExample
    {
         public static String app_key = "997f28c1cea5a9f17d82079a";
         public static String master_secret = "47d264a3c02a6a5a4a256a45";

         public static void testGetReport()
         {
             JPushClient jpushClient = new JPushClient(app_key, master_secret);
             try
             {
                 var result = jpushClient.getReceivedApi("1942377665");
                 Console.WriteLine("Got result - " + result.ToString());

             }
             catch (APIRequestException e)
             {
                 Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                 Console.WriteLine("HTTP Status: " + e.Status);
                 Console.WriteLine("Error Code: " + e.ErrorCode);
                 Console.WriteLine("Error Message: " + e.ErrorCode);
             }
         }
         public static void testGetUsers()
         {
             JPushClient jpushClient = new JPushClient(app_key, master_secret);
             try
             {
                 var result = jpushClient.getReportUsers(TimeUnit.DAY, "2014-06-10", 3);
                 Console.WriteLine("Got result - " + result.ToString());

             }
             catch (APIRequestException e)
             {
                 Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                 Console.WriteLine("HTTP Status: " + e.Status);
                 Console.WriteLine("Error Code: " + e.ErrorCode);
                 Console.WriteLine("Error Message: " + e.ErrorCode);
             }
         }
         public static void testGetMessages()
         {
             JPushClient jpushClient = new JPushClient(app_key, master_secret);
             try
             {
                 var result = jpushClient.getReportMessages("269978303");
                 Console.WriteLine("Got result - " + result.ToString());

             }
             catch (APIRequestException e)
             {
                 Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                 Console.WriteLine("HTTP Status: " + e.Status);
                 Console.WriteLine("Error Code: " + e.ErrorCode);
                 Console.WriteLine("Error Message: " + e.ErrorCode);
             }
         }

    }
}
