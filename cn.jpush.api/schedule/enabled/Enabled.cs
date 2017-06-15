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
