using cn.jpush.api.common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace cn.jpush.api.device
{
    public class TagListResult : BaseResult
    {
        public List<string> tags;

        public TagListResult()
        {
            tags = null;
        }

        public override bool isResultOK()
        {
            return Equals(ResponseResult.responseCode, HttpStatusCode.OK) ? true : false;
        }

        public static TagListResult fromResponse(ResponseWrapper responseWrapper)
        {
            TagListResult tagListResult = new TagListResult();
            if (responseWrapper.isServerResponse())
            {
                tagListResult = JsonConvert.DeserializeObject<TagListResult>(responseWrapper.responseContent);
            }
            tagListResult.ResponseResult = responseWrapper;
            return tagListResult;
        }
    }
}
