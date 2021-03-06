﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace BestGirl.Core.Extensions
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum value)
		{
			var enumType = value.GetType();
			string enumName = Enum.GetName(enumType, value);
			if (enumName != null)
			{
				var field = enumType.GetField(enumName);
				if (field != null)
				{
					var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
					return descriptionAttribute?.Description;
				}
			}

			return null;
		}

		public static T ToEnumFromDescription<T>(this string value)
		{
			var fields = typeof(T).GetFields();
			foreach (var field in fields)
			{
				var descriptionAttributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (descriptionAttributes != null && descriptionAttributes.Length > 0 && descriptionAttributes[0].Description == value)
					return (T)Enum.Parse(typeof(T), field.Name);
			}

			return (T)Enum.Parse(typeof(T), value);
		}

		public static T ToEnumFromId<T>(this string id)
		{
			int enumId = int.Parse(id);
			return (T)Enum.ToObject(typeof(T), enumId);
		}
	}
}
