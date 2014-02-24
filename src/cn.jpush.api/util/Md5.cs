using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace cn.jpush.api.util
{
    class Md5
    {
        public static String getMD5Hash(String str)
        {
            // MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(str), 0, str.Length);
            //char[] temp = new char[res.Length];
            //System.Array.Copy(res, temp, res.Length);
            //return new String(temp);


            // 创建MD5类的默认实例：MD5CryptoServiceProvider


            MD5 md5 = MD5.Create();


            byte[] bs = Encoding.UTF8.GetBytes(str);


            byte[] hs = md5.ComputeHash(bs);


            StringBuilder sb = new StringBuilder();


            foreach (byte b in hs)
            {


                // 以十六进制格式格式化


                sb.Append(b.ToString("x2"));


            }


            return sb.ToString();


        }
    }
}
