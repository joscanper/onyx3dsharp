using System;
using System.ComponentModel;
using System.Globalization;


using OpenTK;

namespace Onyx3DEditor
{
	public class Vector3Converter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			/*
			string stringValue;
			object result;

			result = null;
			stringValue = value as string;

			if (!string.IsNullOrEmpty(stringValue))
			{
				int nonDigitIndex;

				nonDigitIndex = stringValue.IndexOf(stringValue.FirstOrDefault(char.IsLetter));

				if (nonDigitIndex > 0)
				{
					result = new Length
					{
						Value = Convert.ToSingle(stringValue.Substring(0, nonDigitIndex)),
						Unit = (Unit)Enum.Parse(typeof(Unit), stringValue.Substring(nonDigitIndex), true)
					};
				}
			}
			*/
			return Vector3.One as object ?? base.ConvertFrom(context, culture, value);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection props = new PropertyDescriptorCollection(null);
			props.Add(TypeDescriptor.GetProperties(value, attributes).Find("X", true));
			props.Add(TypeDescriptor.GetProperties(value, attributes).Find("Y", true));
			props.Add(TypeDescriptor.GetProperties(value, attributes).Find("Z", true));
			return TypeDescriptor.GetProperties(value, attributes);
		}

	}
}
