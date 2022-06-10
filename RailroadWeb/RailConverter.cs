using Newtonsoft.Json;
using RailroadWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadWeb
{
    public class RailConverter: JsonConverter<Rail>
    {
        public override Rail ReadJson(JsonReader reader, Type objectType, Rail existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var s = ((string)reader.Value).Split(new char[] { '-' },StringSplitOptions.RemoveEmptyEntries);

            return new Rail(s[0], s[1]);

            //throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Rail value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
            
        }
    }
}
