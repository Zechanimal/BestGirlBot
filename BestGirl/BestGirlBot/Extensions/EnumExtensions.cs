using System;
using System.ComponentModel;
using System.Reflection;

namespace BestGirlBot.Extensions
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
					var descriptionAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
					return descriptionAttribute == null ? null : descriptionAttribute.Description;
				}
			}

			return null;
		}
	}
}
