using System;
using Newtonsoft.Json;

namespace CaseService.Services.Utils {
    public class EpochDateTimeConverter: JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            long seconds;
            if (value is DateTime) {
                DateTime dt = (DateTime)value;
                if (!dt.Equals(DateTime.MinValue)) {
                    seconds = dt.ToEpoch();
                } else {
                    seconds = long.MinValue;
                }
            } else {
                throw new Exception("Expected date object value.");
            }

            writer.WriteValue(seconds);
        }

        public override object ReadJson(JsonReader reader, Type type, object value, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.None || reader.TokenType == JsonToken.Null)  {
                return null;
            }

            if (reader.TokenType != JsonToken.Integer) {
                throw new Exception(
                    String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
                    reader.TokenType));
            }

            if(((long)reader.Value) < 0) {
                return null;
            }

            long seconds = (long)reader.Value;
            return new DateTime(1970, 1, 1).AddSeconds(seconds);
        }

        public override bool CanConvert(Type objectType) {
            //TODO figure this out
            return true;
        }
    }
}