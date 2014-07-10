using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using cn.jpush.api.common;
using cn.jpush.api.push;

namespace cn.jpush.api.example
{
    class JPushExample
    {
        static void Main(string[] args)
        {
            HashSet<DeviceEnum> hs = new HashSet<DeviceEnum>();
            hs.Add(DeviceEnum.Android);
            hs.Add(common.DeviceEnum.IOS);
            JPushClient jc = new JPushClient( "your app_key", "your API MasterSecret", 0, hs, false);
            NotificationParams np = new NotificationParams();
            np.ReceiverType = ReceiverTypeEnum.APP_KEY;

            String extras = "{\"n_builder_id\":1}";
            MessageResult mr = jc.sendNotification("teasdfdasfadst", np, extras);
            Console.WriteLine(mr.errcode);
            Console.WriteLine(mr.errmsg);
            
        }
    }
}
