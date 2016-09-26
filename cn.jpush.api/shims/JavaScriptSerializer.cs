#if DOTNETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace System.Web.Script.Serialization {
    public class JavaScriptSerializer {
        public T Deserialize<T>(string jsonString) {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string Serialize(object obj) {
            return JsonConvert.SerializeObject(obj, Formatting.Indented,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
    }
}
#endif
