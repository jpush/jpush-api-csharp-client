using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cn.jpush.api.util
{
    public class StringUtil
    {

        public static bool IsNumber(String strNumber)
        {
                    Regex objNotNumberPattern=new Regex("[^0-9.-]");
                    Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");
                    Regex objTwoMinusPattern=new Regex("[0-9]*[-][0-9]*[-][0-9]*");
                    String strValidRealPattern="^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
                    String strValidIntegerPattern="^([-]|[0-9])[0-9]*$";
                    Regex objNumberPattern =new Regex("(" + strValidRealPattern +")|(" + strValidIntegerPattern + ")");

                    return !objNotNumberPattern.IsMatch(strNumber) &&
                           !objTwoDotPattern.IsMatch(strNumber) &&
                           !objTwoMinusPattern.IsMatch(strNumber) &&
                           objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsNumeric(string value)
        {
                  return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        public static bool IsInt(string value)
        {
                  return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        public static bool IsUnsign(string value)
        {
                  return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }
        public static String arrayToString(String[] values)
        {
            if (null == values) return "";

            StringBuilder buffer = new StringBuilder(values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                buffer.Append(values[i]).Append(",");
            }
            if (buffer.Length > 0)
            {
                return buffer.ToString().Substring(0, buffer.Length - 1);
            }
            return "";
        }

        public static Boolean  IsDateTime(String datetime) {
            Boolean isdatetime = new Boolean();
            DateTime dateTime;
            try
            {              
                dateTime = DateTime.ParseExact(datetime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                isdatetime = true;
            }
            catch
            {
                isdatetime = false;
            }
            return isdatetime;

        }

        public static Boolean IsTime(String time)
        {
            Boolean istime = new Boolean();
            try
            {
                DateTime righttime;
                righttime = DateTime.ParseExact(time,"HH:mm:ss", CultureInfo.InvariantCulture);
                istime = true;
            } 
            catch
            {
                istime = false;
            }
            return istime;

        }

        public static Boolean IsMobile(String mobile)
        {
            Boolean ismobile = new Boolean();
            ismobile =Regex.IsMatch(mobile, @"^(1[34578][0-9])(\\d{4})(\\d{4})$");
            return ismobile;

        }
        public static Boolean IsTimeunit(String time_unit)
        {
            Boolean istime_unit = new Boolean();

            if (String.Equals(time_unit, "day", StringComparison.CurrentCultureIgnoreCase))
            {
                istime_unit = true;
            }
            else if (String.Equals(time_unit, "week", StringComparison.CurrentCultureIgnoreCase))
            {
                istime_unit = true;
            }
            else if (String.Equals(time_unit, "month", StringComparison.CurrentCultureIgnoreCase))
            {
                istime_unit = true;
            }

            else
            {
                istime_unit = false;
            } 

            return istime_unit;

        }

        public static Boolean IsValidName(String name)
        {
            Boolean isname = new Boolean();
            isname = System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            isname= isname&&(name.Length<256);
            return isname;
        }

        public static Boolean IsValidTag(String tag)
        {
            Boolean isname = new Boolean();
            isname = System.Text.RegularExpressions.Regex.IsMatch(tag, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            //最多 40个字节
            isname = isname && (tag.Length < 41);
            return isname;
        }

        public static Boolean IsValidAlias(String alias)
        {
            Boolean isname = new Boolean();
            isname = System.Text.RegularExpressions.Regex.IsMatch(alias, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            //最多 40个字节
            isname = isname&&(alias.Length < 41);
            return isname;
        }
    }
}
