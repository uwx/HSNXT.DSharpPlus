using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using HSNXT.SuccincT.Unions;
using static HSNXT.SuccincT.Functional.Unit;
using static HSNXT.SuccincT.Unions.None;
using NJsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace HSNXT.SuccincT.JSON
{
    public class NoneAndUnitConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(None);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        NJsonSerializer serializer)
        {
            JObject.Load(reader);
            return objectType.Name == "None" ? (object)none : unit;
        }

        public override void WriteJson(JsonWriter writer, object value, NJsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WriteEndObject();
        }
    }
}
