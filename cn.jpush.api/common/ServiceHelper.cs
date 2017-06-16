using System;

namespace cn.jpush.api.common
{
    public class ServiceHelper
    {
        private const int MAX_BADGE_NUMBER = 99999;
        private const int MIN = 100000;
        private const int MAX = int.MaxValue;

        public static int generateSendno()
        {
            Random random = new Random();
            return random.Next((MAX - MIN) + 1) + MIN;
        }

        public static bool isValidIntBadge(int intBadge)
        {
            return (intBadge >= 0 && intBadge <= MAX_BADGE_NUMBER) ? true : false;
        }
    }
}
