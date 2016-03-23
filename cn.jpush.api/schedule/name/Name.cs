using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using cn.jpush.api.util;

namespace cn.jpush.api.schedule
{
    public class Name
    {

        [JsonProperty]
        private String name;

        public void setName(String name)
        {
            
            Preconditions.checkArgument(!String.IsNullOrEmpty(name), "The name must not be empty.");
            Preconditions.checkArgument(StringUtil.IsValidName(name), "The name must be the right format.");
            Preconditions.checkArgument((name.Length<255), "The name must be less than 255 bytes.");
            this.name = name;
        }
        public String getName()
        {
          return name;
        }
    }
}

