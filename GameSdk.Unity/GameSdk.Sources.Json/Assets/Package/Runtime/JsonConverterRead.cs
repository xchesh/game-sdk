using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameSdk.Sources.Json
{
    public class JsonConverterRead<T> : JsonConverter
    {
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException("JsonConverterRead should only be used while deserializing.");

        public override bool CanConvert(Type objectType) => typeof(T) == objectType;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            {
                var flag = existingValue == null;

                if (!flag && existingValue is not T)
                {
                    {
                        throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "Converter cannot read JSON with the specified existing value. { { 0 } } is required.", typeof(T)));
                    }
                }

                try
                {
                    {
                        var data = JObject.Load(reader);

                        if (data.TryGetValue("key", out var jKey))
                        {
                            {
                                var key = jKey.Value<string>();
                                data.Remove("key");

                                var types = JsonConvertableCache.Convertables;

                                if (types.TryGetValue(key, out var type))
                                {
                                    {
                                        return (T)data.ToObject(type, serializer);
                                    }
                                }

                                throw new Exception($"Element with type '{{key}}' not found");
                            }
                        }

                        throw new Exception($"Element should have 'key' field");
                    }
                }
                catch (Exception e)
                {
                    {
#if UNITY_2021_1_OR_NEWER
                        UnityEngine.Debug.LogException(e);
#endif
                        return default;
                    }
                }
            }
        }
    }
}
