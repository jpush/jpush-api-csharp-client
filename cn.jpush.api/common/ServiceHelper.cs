using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.common
{
    public class ServiceHelper
    {
        private const  int MAX_BADGE_NUMBER = 99999;

        public static bool isValidIntBadge(int intBadge)
        {
            if (intBadge >= 0 && intBadge <= MAX_BADGE_NUMBER)
            {
                return true;
            }
            return false;
        }
    }
}
