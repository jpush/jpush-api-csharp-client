using cn.jpush.api.common;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    class Platform
    {
        private  const String ALL = "\"all\"";
    
        private  bool all;
        private  HashSet<DeviceType> deviceTypes;

        private Platform(bool all, HashSet<DeviceType> deviceTypes)
        {
            this.all = all;
            this.deviceTypes = deviceTypes;
        }
        public static Platform allPlatform()
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
            return all;
        }
        public string toJSON()
        {
            if (all){ return ALL;}
            StringBuilder json=new StringBuilder();
            foreach (var type in this.deviceTypes)
            {
                json.Append("\"").Append(type.ToString()).Append("\"").Append(",");
            }
            if (json.Length > 0) { json.Remove(json.Length - 1, 1); }

            json.Append("]");
            json.Insert(0,"[");

            return json.ToString();
        }

    }
}
