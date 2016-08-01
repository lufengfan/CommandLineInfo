using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	/// <summary>
	/// 表示多行的程序集许可文本。
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false), ComVisible(false)]
	public class AssemblyLicenseAttribute : MultilineTextAttribute
	{
		/// <summary>
		/// 以指定的文本行字符串数组初始化 <see cref="AssemblyLicenseAttribute"/> 类实例。
		/// </summary>
		/// <param name="lines">指定的文本行字符串数组。</param>
		public AssemblyLicenseAttribute(params string[] lines) : base(lines) { }
	}
}
