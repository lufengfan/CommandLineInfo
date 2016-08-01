using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	/// <summary>
	/// 为创建一个用来定义命令行解析规则的特性提供基本的属性。
	/// </summary>
	public class BaseOptionAttribute : Attribute
	{
		internal const string DefaultMutuallyExclusiveSet = "Default";

		/// <summary>
		/// 获取选项的别名。
		/// </summary>
		/// <value>
		/// <para>选项的别名。</para>
		/// <para>当选项的别名不存在（默认）时，此属性的值为 <see langword="null"/> 或 <see cref="string.Empty"/> 。</para>
		/// <para>相应的，可以将此属性的值赋值为 <see langword="null"/> 或 <see cref="string.Empty"/> ，表示选项的别名不存在。</para>
		/// </value>
		public virtual string Abbreviation { get; internal set; }

		/// <summary>
		/// 获取一个值，表示选项是否含有别名。
		/// </summary>
		/// <value>
		/// <para>表示选项是否含有别名。</para>
		/// <para>当属性 <see cref="Abbreviation"/> 的值为 <see langword="null"/> 或 <see cref="string.Empty"/> 时，将返回 <see langword="false"/>，否则返回 <see langword="true"/> 。</para>
		/// </value>
		public bool HasAbbreviation
		{
			get
			{
				return string.IsNullOrEmpty(this.Abbreviation);
			}
		}

		/// <summary>
		/// 获取选项的名称。
		/// </summary>
		/// <value>选项的名称</value>
		public virtual string Name { get; internal set; }

		/// <summary>
		/// 获取一个值，表示选项是否含有名称。
		/// </summary>
		/// <value>
		/// <para>表示选项是否含有名称。</para>
		/// <para>当属性 <see cref="Name"/> 的值为 <see langword="null"/> 或 <see cref="string.Empty"/> 时，将返回 <see langword="false"/>，否则返回 <see langword="true"/> 。</para>
		/// </value>
		public bool HasName
		{
			get
			{
				return string.IsNullOrEmpty(this.Name);
			}
		}

		private object defaultValue;

		/// <summary>
		/// 获取选项的默认值。
		/// </summary>
		/// <value>选项的默认值</value>
		/// <remarks>当第一次对此属性赋值时，会同时更改属性 <see cref="HasDefaultValue"/> 的值为 <see langword="true"/> 。</remarks>
		public object DefaultValue
		{
			get
			{
				return defaultValue;
			}
			set
			{
				this.defaultValue = value;
				this.HasDefaultValue = true;
			}
		}

		/// <summary>
		/// 获取一个值，表示选项是否含有默认值。
		/// </summary>
		/// <value>表示选项是否含有默认值。默认为 <see langword="false"/> 。</value>
		/// <remarks>当第一次对属性 <see cref="DefaultValue"/> 赋值时，会同时更改此属性的值为 <see langword="true"/> 。</remarks>
		public bool HasDefaultValue { get; private set; }

		private string metaValue;

		public string MetaValue
		{
			get
			{
				return metaValue;
			}
			set
			{
				this.metaValue = value;
				this.HasMetaValue = !string.IsNullOrEmpty(this.MetaValue);
			}
		}

		public bool HasMetaValue { get; private set; }

		private string mutuallyExclusiveSet;

		public string MutuallyExclusiveSet
		{
			get
			{
				return this.mutuallyExclusiveSet;
			}
			set
			{
				this.mutuallyExclusiveSet = value;
			}
		}

		/// <summary>
		/// 获取一个值表示此选项是否必须。
		/// </summary>
		/// <value>表示此选项是否必须</value>
		public virtual bool Required { get; set; }

		/// <summary>
		/// 获取或设置一个值，表示是否自动设置选项的名称。
		/// </summary>
		/// <value>表示如果不指定选项的名称或选项的名称不规范，则是否自动设置选项的名称。</value>
		internal bool AutoName { get; set; }

		/// <summary>
		/// 获取或设置一段描述此选项的简洁的文本。
		/// </summary>
		/// <value>一段描述此选项的简洁的文本。</value>
		public string HelpText { get; set; }

		/// <summary>
		/// 获取或设置此选项的全局唯一名称。
		/// </summary>
		/// <value>此选项的全局唯一名称。</value>
		internal string UniqueName { get; private set; }

		/// <summary>
		/// 以指定的缩写和名称初始化 <see cref="BaseOptionAttribute"/> 类实例。
		/// </summary>
		/// <param name="abbreviation">
		/// <para>选项的缩写</para>
		/// <para>若不显示指定此参数的值，则将设为默认值 <see langword="null"/> ，表示此选项不含有缩写。</para>
		/// </param>
		/// <param name="name">
		/// <para>选项的名称</para>
		/// <para>若不显示指定此参数的值，则将设为默认值 <see langword="null"/> ，表示此选项不含有名称。</para>
		/// </param>
		public BaseOptionAttribute(string abbreviation = null, string name = null) { }
	}
}
