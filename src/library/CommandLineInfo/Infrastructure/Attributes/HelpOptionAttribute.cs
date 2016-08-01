using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SamLu.Collections.Generic;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	/// <summary>
	/// 标识一个实例方法，当显示帮助文本时此方法必须被调用。
	/// </summary>
	/// <remarks>
	/// 此方法的签名必须为实例方法、不含参数且返回 <see cref="string"/> 类型的对象。
	/// </remarks>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HelpOptionAttribute : BaseOptionAttribute
	{
		private const string DefaultHelpText = "Display this help screen.";

		/// <inheritdoc />
		/// <value>
		/// <para>表示此选项是否必须。</para>
		/// <para>对于此选项，永远返回 <see langword="false"/> 。</para>
		/// </value>
		/// <exception cref="InvalidOperationException">试图对此属性进行赋值操作。</exception>
		public override bool Required
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// 初始化 <see cref="HelpOptionAttribute"/> 类的实例。并指定选项的缩写和名称。
		/// </summary>
		/// <param name="alias">
		/// <para>选项的缩写</para>
		/// <para>若不显示指定此参数的值，则将设为默认值 <c>"h"</c> 。</para>
		/// </param>
		/// <param name="name">
		/// <para>选项的名称</para>
		/// <para>若不显示指定此参数的值，则将设为默认值 <c>"help"</c> 。</para>
		/// </param>
		/// <remarks>
		/// <note type="caution">强烈建议不要改变用户调用帮助的方式。因为这可能会造成混淆。</note>
		/// </remarks>
		public HelpOptionAttribute(string alias = "h", string name = "help") : base(alias, name)
		{
			base.HelpText = "Display this help screen.";
		}

		/// <summary>
		/// 调用标记的显示帮助文本的方法。
		/// </summary>
		/// <param name="target">定义标记的方法的类的实例。</param>
		/// <param name="pair">包含相对应的 <see cref="MethodInfo"/> 类对象和 <see cref="HelpOptionAttribute"/> 类对象的耦。</param>
		/// <param name="text">标记的方法返回的帮助文本。</param>
		internal static void InvokeMethod(object target, Pair<MethodInfo, HelpOptionAttribute> pair, out string text)
		{
			text = null;
			MethodInfo method = pair.Left;
			if (!HelpOptionAttribute.CheckMethodSignature(method))
			{
				throw new MemberAccessException();
			}
			text = (string)method.Invoke(target, null);
		}

		/// <summary>
		/// 验证标记的方法是否符合规范。
		/// </summary>
		/// <param name="value">标记的方法的反射对象。</param>
		/// <returns>表示标记的方法是否符合规范。</returns>
		private static bool CheckMethodSignature(MethodInfo value)
		{
			return value.ReturnType == typeof(string) && value.GetParameters().Length == 0;
		}
	}
}
