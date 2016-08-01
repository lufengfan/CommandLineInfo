//using CommandLine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using SamLu.Collections.Generic;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ValueListAttribute : Attribute
	{
		private readonly Type _concreteType;

		public int MaximumElements
		{
			get;
			set;
		}

		public Type ConcreteType
		{
			get
			{
				return this._concreteType;
			}
		}

		public ValueListAttribute(Type concreteType) : this()
		{
			if (concreteType == null)
			{
				throw new ArgumentNullException("concreteType");
			}
			if (!typeof(IList<string>).IsAssignableFrom(concreteType))
			{
				throw new ParserException("The types are incompatible.");
			}
			this._concreteType = concreteType;
		}

		private ValueListAttribute()
		{
			this.MaximumElements = -1;
		}

		internal static IList<string> GetReference(object target)
		{
			Type concreteType;
			PropertyInfo property = ValueListAttribute.GetProperty(target, out concreteType);
			IList<string> result;
			if (property == null || concreteType == null)
			{
				result = null;
			}
			else
			{
				property.SetValue(target, Activator.CreateInstance(concreteType), null);
				result = (IList<string>)property.GetValue(target, null);
			}
			return result;
		}

		internal static ValueListAttribute GetAttribute(object target)
		{
			IList<Pair<PropertyInfo, ValueListAttribute>> list = null;// ReflectionHelper.RetrievePropertyList<ValueListAttribute>(target);
			ValueListAttribute result;
			if (list == null || list.Count == 0)
			{
				result = null;
			}
			else
			{
				if (list.Count > 1)
				{
					throw new InvalidOperationException();
				}
				Pair<PropertyInfo, ValueListAttribute> pairZero = list[0];
				result = pairZero.Right;
			}
			return result;
		}

		private static PropertyInfo GetProperty(object target, out Type concreteType)
		{
			concreteType = null;
			IList<Pair<PropertyInfo, ValueListAttribute>> list = null;// ReflectionHelper.RetrievePropertyList<ValueListAttribute>(target);
			PropertyInfo result;
			if (list == null || list.Count == 0)
			{
				result = null;
			}
			else
			{
				if (list.Count > 1)
				{
					throw new InvalidOperationException();
				}
				Pair<PropertyInfo, ValueListAttribute> pairZero = list[0];
				concreteType = pairZero.Right.ConcreteType;
				result = pairZero.Left;
			}
			return result;
		}
	}
}
