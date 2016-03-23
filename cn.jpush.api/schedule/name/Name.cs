using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace cn.jpush.api.schedule
{
    public class Name
    {

        [JsonProperty]
        private String name;

        public void setName(String name)
        {
            this.name = name;
        }
        public String getName()
        {

          return name;

        }
    }
}

