using cn.jpush.api.common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.device
{
    public  class AliasDeviceListResult:BaseResult
    {
        public List<String> registration_ids ;
        public AliasDeviceListResult()
        {
            registration_ids = null;
        }
        public override bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }
        public static AliasDeviceListResult fromResponse(ResponseWrapper responseWrapper)
        {
            AliasDeviceListResult aliasDeviceListResult = new AliasDeviceListResult();
            if (responseWrapper.isServerResponse())
            {
                aliasDeviceListResult = JsonConvert.DeserializeObject<AliasDeviceListResult>(responseWrapper.responseContent);
            }
            aliasDeviceListResult.ResponseResult = responseWrapper;
            return aliasDeviceListResult;
        }
    }
}
