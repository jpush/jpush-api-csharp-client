using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push
{
    class PushPayload
    {
        private static  String PLATFORM = "platform";
        private static  String AUDIENCE = "audience";
        private static  String NOTIFICATION = "notification";
        private static  String MESSAGE = "message";
        private static  String OPTIONS = "options";

        private static  int MAX_GLOBAL_ENTITY_LENGTH = 1200;  // Definition acording to JPush Docs
        private static  int MAX_IOS_PAYLOAD_LENGTH = 220;  // Definition acording to JPush Docs



    }
}
