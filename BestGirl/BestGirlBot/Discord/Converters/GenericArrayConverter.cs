using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BestGirlBot.Discord.Converters
{
	public class GenericArrayConverter<T> : JsonConverter where T : JsonConverter, new()
	{
		protected static T InnerConverter = new T();

		public override bool CanConvert(Type objectType)
		{
			var innerParamType = objectType.GetInterfaces()
				.Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				.Select(t => t.GetGenericArguments()[0])
				.FirstOrDefault();
			return InnerConverter.CanConvert(innerParamType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var innerParamType = objectType.GetInterfaces()
				.Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				.Select(t => t.GetGenericArguments()[0])
				.FirstOrDefault();

			Type listType = typeof(List<>).MakeGenericType(innerParamType);
			var result = (IList)Activator.CreateInstance(listType);

			if (reader.TokenType == JsonToken.StartArray)
			{
				reader.Read();
				while (reader.TokenType != JsonToken.EndArray)
				{
					result.Add(InnerConverter.ReadJson(reader, innerParamType, null, serializer));
					reader.Read();
				}
			}

			var array = Array.CreateInstance(innerParamType, result.Count);
			result.CopyTo(array, 0);
			return array;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var innerParamType = value.GetType().GetInterfaces()
				.Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				.Select(t => t.GetGenericArguments()[0])
				.FirstOrDefault();

			if (value == null) writer.WriteNull();
			else
			{
				writer.WriteStartArray();
				foreach (var i in (IList)value)
				{
					InnerConverter.WriteJson(writer, i, serializer);
				}
				writer.WriteEndArray();
			}
		}
	}
}
