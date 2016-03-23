using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api.util;
using Newtonsoft.Json;
namespace cn.jpush.api.schedule
{
    public class Enabled
    {
        [JsonProperty]
        private bool enable;

        public void setEnable(bool enable) { 
            this.enable = enable;  
        }
        public bool getEnable()
        {
            return enable;
        }
    }
}
