using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SamLu.CommandLineInfo.Infrastructure.Text;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	/// <summary>
	/// 为创建一个用来定义多行文本的特性提供基本的属性。
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false), ComVisible(false)]
	public class MultilineTextAttribute : Attribute
	{
		/// <summary>
		/// <see cref="MultilineTextAttribute"/> 类对象内部储存文本行的字符串数组。
		/// </summary>
		private string[] lines;

		/// <summary>
		/// 获取所有文本行的字符串表示。
		/// </summary>
		/// <value>所有文本行的字符串表示。</value>
		/// <remarks>处理文本换行的换行符为 <see cref="Environment.NewLine"/> 。</remarks>
		public virtual string Value
		{
			get
			{
				// 将帮助文本字符串数组中第1行到最后一行（可能小于数组长度）的数据连接成一个字符串。
				return string.Join(Environment.NewLine, this.lines, 0, this.GetLastLineWithText(this.lines));
			}
		}

		/// <summary>
		/// 以指定的文本行字符串数组初始化 <see cref="MultilineTextAttribute"/> 类实例。
		/// </summary>
		/// <param name="lines">指定的文本行字符串数组。</param>
		protected MultilineTextAttribute(params string[] lines) {
			if (lines == null) throw new ArgumentNullException(nameof(lines));

			this.lines = lines;
		}

		/// <summary>
		/// 以指定的方法向命令行帮助文本中添加一行。
		/// </summary>
		/// <param name="action">添加帮助文本行的方法。</param>
		internal void AddToHelpText(Action<string> action)
		{
			Array.ForEach(this.lines, line =>
			{
				if (!string.IsNullOrEmpty(line)) action(line);
			});
		}

		/// <summary>
		/// 以指定的方法向命令行帮助文本中添加一行。
		/// </summary>
		/// <param name="helpText">表示命令行帮助文本的 <see cref="HelpText"/> 对象。</param>
		/// <param name="before">
		/// <para>指示添加的帮助文本是否显示在命令行选项前方。</para>
		/// <para>值为 <see langword="true"/> 时显示在前方，为 <see langword="false"/> 时显示在后方。</para>
		/// </param>
		internal void AddToHelpText(HelpText helpText, bool before)
		{
			if (before)
				this.AddToHelpText(helpText.AddPreOptionsLine);
			else
				this.AddToHelpText(helpText.AddPostOptionsLine);
		}

		/// <summary>
		/// 返回文本的最后一行行号。保留文本中的空行。
		/// </summary>
		/// <param name="value">处理的文本（字符串数组）。</param>
		/// <returns>
		/// 最后且不为空白的行的行号。
		/// </returns>
		protected virtual int GetLastLineWithText(string[] value)
		{
			int index = Array.FindLastIndex<string>(value, (string line) => !string.IsNullOrEmpty(line));
			return index + 1;
		}
	}
}
