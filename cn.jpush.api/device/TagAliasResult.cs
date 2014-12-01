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
  public  class TagAliasResult:BaseResult
    {
        public List<String> tags;
        public String alias;

        public TagAliasResult()
        {
            tags = null;
            alias = null;
        }
        public override bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }
        public static TagAliasResult fromResponse(ResponseWrapper responseWrapper)
        {
            TagAliasResult tagAliasResult = new TagAliasResult();
            if (responseWrapper.isServerResponse())
            {
                tagAliasResult = JsonConvert.DeserializeObject<TagAliasResult>(responseWrapper.responseContent);
            }
            tagAliasResult.ResponseResult = responseWrapper;
            return tagAliasResult;
        }

    }
}
