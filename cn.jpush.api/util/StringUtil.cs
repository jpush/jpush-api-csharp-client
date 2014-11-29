using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cn.jpush.api.util
{
    class StringUtil
    {

        public bool IsNumber(String strNumber)
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

    }
}
