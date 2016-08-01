using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel2
{
	/// <summary>
	/// 表示命令行选项的基类。
	/// </summary>
	public class BaseOption
	{
		/// <summary>
		/// 获取选项的名称。
		/// </summary>
		/// <value>选项的名称</value>
		public string Name { get; private set; }

		/// <summary>
		/// 获取选项的别名。
		/// </summary>
		/// <value>
		/// <para>选项的别名。</para>
		/// <para>当选项的别名不存在（默认）时，此属性的值为 <see langword="null"/> 或 <see cref="string.Empty"/> 。</para>
		/// <para>相应的，可以将此属性的值赋值为 <see langword="null"/> 或 <see cref="string.Empty"/> ，表示选项的别名不存在。</para>
		/// </value>
		public string Abbreviation { get; private set; }
	}
}
