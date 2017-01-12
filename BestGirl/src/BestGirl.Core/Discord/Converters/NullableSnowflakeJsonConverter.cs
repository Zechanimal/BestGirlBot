using System;
using Newtonsoft.Json;
using System.Globalization;

namespace BestGirl.Core.Discord.Converters
{
	class NullableSnowflakeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ulong?);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return reader.Value == null ? (ulong?)null : ulong.Parse(((string)reader.Value), NumberStyles.None, CultureInfo.InvariantCulture);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(value == null ? null : ((ulong)value).ToString(CultureInfo.InvariantCulture));
		}
	}
}
