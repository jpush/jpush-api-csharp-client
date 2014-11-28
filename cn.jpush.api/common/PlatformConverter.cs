using cn.jpush.api.push.mode;
using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.common
{
  public  class PlatformConverter:JsonConverter
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
            if (objectType == typeof(Platform))
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
            Platform platform = value as Platform;
            if (platform == null)
            {
                return;
            }
            platform.Check();
            if (platform.isAll())
            {
                writer.WriteValue(platform.allPlatform);
            }
            else
            {
                writer.WriteStartArray();
                foreach (var item in platform.deviceTypes)
                {
                    writer.WriteValue(item);
                }
                writer.WriteEndArray();
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
            Platform platform =  Platform.all();
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if(reader.TokenType==JsonToken.StartArray)
            {
                platform.allPlatform = null;
                platform.deviceTypes = ReadArray(reader);
            }
            else if (reader.TokenType==JsonToken.String)
            {
                platform.allPlatform = reader.Value.ToString();
            }
            else
            {
                return null;
            }
            return platform;
        }

        private HashSet<string> ReadArray(JsonReader reader)
        {
            HashSet<string> list = new HashSet<string>();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.String:
                        list.Add(Convert.ToString(reader.Value, CultureInfo.InvariantCulture));
                        break;
                    case JsonToken.EndArray:
                        return list;
                    case JsonToken.Comment:
                        // skip
                        break;
                    default:
                        return null;
                }
            }
            return null;
        }
       
    }
}
