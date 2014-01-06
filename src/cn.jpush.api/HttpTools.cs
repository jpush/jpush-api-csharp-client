using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace cn.jpush.api
{
    class HttpTools
    {
        public String toPost(String url, String urlParams)
        {
            byte[] postData = Encoding.UTF8.GetBytes(urlParams);
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            return srcString;
        }


        public String toGet(String url, String auth)
        {
            auth = "Basic " + auth;


            try
            {


                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Method = "GET";
                myReq.Accept = "text/html, application/xhtml+xml, */*";
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.Headers.Add("Charset", "UTF-8");
                myReq.Headers.Add("Authorization", auth);


                HttpWebResponse response = (HttpWebResponse)myReq.GetResponse();
                //res.            
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    return reader.ReadToEnd();


                }


            }
            catch (System.Exception ex)
            {


                String errorMsg = ex.Message;
                Console.Write(errorMsg);



            }

            return "";


        }

    }
}
