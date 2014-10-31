using cn.jpush.api.common;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
   public class Platform : IPushMode
    {
        private  const String ALL = "all";

        private bool allPlatform;
        private  HashSet<DeviceType> deviceTypes;
        public Platform()
        {
            this.allPlatform = true;
            this.deviceTypes = null;
        }
        public Platform(bool all, HashSet<DeviceType> deviceTypes)
        {
            this.allPlatform = all;
            this.deviceTypes = deviceTypes;
        }
        public static Platform all()
        {
            return new Platform(true, null);
        }
        public static Platform ios()
        {
            HashSet<DeviceType> types=new HashSet<DeviceType>();
            types.Add(DeviceType.ios);
            return new Platform(false,types);
        }
        public static Platform android()
        {
            HashSet<DeviceType> types = new HashSet<DeviceType>();
            types.Add(DeviceType.andriod);
            return new Platform(false, types);
        }
        public static Platform winphone()
        {
            HashSet<DeviceType> types = new HashSet<DeviceType>();
            types.Add(DeviceType.wp);
            return new Platform(false, types);
        }
        public static Platform android_ios()
        {
            HashSet<DeviceType> types = new HashSet<DeviceType>();
            types.Add(DeviceType.andriod);
            types.Add(DeviceType.ios);
            return new Platform(false, types);
        }
        public static Platform android_winphone()
        {
            HashSet<DeviceType> types = new HashSet<DeviceType>();
            types.Add(DeviceType.andriod);
            types.Add(DeviceType.wp);
            return new Platform(false, types);
        }
        public static Platform ios_winphone()
        {
            HashSet<DeviceType> types = new HashSet<DeviceType>();
            types.Add(DeviceType.ios);
            types.Add(DeviceType.wp);

            return new Platform(false, types);
        }
        public bool isAll()
        {
            return allPlatform;
        }
        public object toJsonObject()
        {
            if (allPlatform) 
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
