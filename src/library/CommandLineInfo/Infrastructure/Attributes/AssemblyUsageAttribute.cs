using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	/// <summary>
	/// 表示多行的程序集用法文本。
	/// </summary>
	public class AssemblyUsageAttribute : MultilineTextAttribute
	{
		/// <summary>
		/// 以指定的文本行字符串数组初始化 <see cref="AssemblyUsageAttribute"/> 类实例。
		/// </summary>
		/// <param name="lines">指定的文本行字符串数组</param>
		public AssemblyUsageAttribute(params string[] lines) : base(lines) { }
	}
}
