using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Jiguang.JPush.Model
{
    public class HttpResponse
    {
        private Task<string> responseContent;

        public HttpStatusCode StatusCode { get; set; }
        public HttpResponseHeaders Headers { get; set; }
        public string Content { get; set; }

        public HttpResponse(HttpStatusCode statusCode, HttpResponseHeaders headers, string content)
        {
            StatusCode = statusCode;
            Headers = headers;
            Content = content;
        }

        public HttpResponse(HttpStatusCode statusCode, HttpResponseHeaders headers, Task<string> responseContent)
        {
            StatusCode = statusCode;
            Headers = headers;
            this.responseContent = responseContent;
        }
    }
}
