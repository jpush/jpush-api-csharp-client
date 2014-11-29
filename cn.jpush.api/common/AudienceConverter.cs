using cn.jpush.api.push.mode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.common
{
   public  class AudienceConverter : JsonConverter
    {
        /// <summary>
        /// Platform whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Audience))
                return true;
            return false;
        }
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Audience audience = value as Audience;
            if (audience == null)
            {
                return;
            }
            audience.Check();
            if (audience.isAll())
            {
                writer.WriteValue(audience.allAudience);
                //writer.WriteValue("alll");
            }
            else
            {
               var json = JsonConvert.SerializeObject(audience.dictionary);
               writer.WriteRawValue(json);
            }
        }
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing property value of the JSON that is being converted.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Audience audience = Audience.all();
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                audience.allAudience = reader.Value.ToString();
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                audience.allAudience = null;
                Dictionary<string, HashSet<string>> dictionary=new Dictionary<string,HashSet<string>>();
                string key="key";
                HashSet<string> value=null;
                while (reader.Read())
                {
                    Debug.WriteLine("Type:{0},Path:{1}", reader.TokenType, reader.Path);
                    switch (reader.TokenType)
                    {
                        case JsonToken.StartObject:
                            break;
                        case JsonToken.PropertyName:
                            key = reader.Value.ToString();
                            break;
                        case JsonToken.StartArray:
                            value = new HashSet<string>();
                            break;
                        case JsonToken.String:
                            value.Add(reader.Value.ToString()); 
                            break;
                        case JsonToken.EndArray:
                            {
                                dictionary.Add(key,value);
                            }
                            break;
                        case JsonToken.EndObject:
                            return audience;
                    }
                }
                audience.dictionary = dictionary;
            }
            return audience;
        }
    }
}
