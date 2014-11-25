using cn.jpush.api.common;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
   public class Platform 
    {
        private  const String ALL = "all";
        public string all{get;set;}
        public  HashSet<string> deviceTypes;
        public Platform()
        {
            all = ALL;
            deviceTypes = null;
        }
        public Platform(bool all)
        {
            if (all)
            {
                this.all = ALL;
            }
        }
        private Platform(bool all, HashSet<string> deviceTypes)
        {
            //用来判断all=true时deviceTypes必须为空，反之当all=false时deviceTypes有值，不然json序列化会出错
            Debug.Assert(all && deviceTypes == null || !all && deviceTypes != null);
            if (all)
            {
                //string = ALL;
            }
            this.deviceTypes = deviceTypes;
        }
        
        public static Platform ios()
        {
            HashSet<string> types = new HashSet<string>();
            types.Add(DeviceType.ios.ToString());
            return new Platform(false,types);
        }
        public static Platform android()
        {
            HashSet<string> types = new HashSet<string>();
            types.Add(DeviceType.andriod.ToString());
            return new Platform(false, types);
        }
        public static Platform winphone()
        {
            HashSet<string> types = new HashSet<string>();
            types.Add(DeviceType.wp.ToString());
            return new Platform(false, types);
        }
        public static Platform android_ios()
        {
            HashSet<string> types = new HashSet<string>();
            types.Add(DeviceType.andriod.ToString());
            types.Add(DeviceType.ios.ToString());
            return new Platform(false, types);
        }
        public static Platform android_winphone()
        {
            HashSet<string> types = new HashSet<string>();
            types.Add(DeviceType.andriod.ToString());
            types.Add(DeviceType.wp.ToString());
            return new Platform(false, types);
        }
        public static Platform ios_winphone()
        {
            HashSet<string> types = new HashSet<string>();
            types.Add(DeviceType.ios.ToString());
            types.Add(DeviceType.wp.ToString());

            return new Platform(false, types);
        }
        public bool isAll()
        {
            return all!=null;
        }
        public object toJsonObject()
        {
            if (all!=null) 
            { 
                return ALL;
            }
            List<string> jsonList = new List<string>();
            foreach (var type in this.deviceTypes)
            {
                jsonList.Add(type.ToString());
            }
            return jsonList;
        }

    }
}
