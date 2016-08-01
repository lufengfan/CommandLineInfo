using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.CommandLineInfo.Infrastructure.Text
{
	class Class1
	{
		/// <summary>
		/// This method shows various examples of the <c>list</c> XML comments element.
		/// </summary>
		/// <remarks>
		/// <para>A simple bulleted list.  The <c>term</c> and <c>description</c>
		/// elements are optional for simple string descriptions.</para>
		/// 
		/// <list type="bullet">
		///   <item>First item</item>
		///   <item>Second item</item>
		///   <item>Third item</item>
		/// </list>
		///
		/// <para>Bullet list with terms and definitions.  The term is highligted and
		/// separated from the definition with a dash.</para>
		/// 
		/// <list type="bullet">
		///   <item>
		///     <term>maxLen</term>
		///     <description>field must contain no more than the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>minLen</term>
		///     <description>field must contain at least the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>maxVal</term>
		///     <description>field must contain a number that is no larger than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>minVal</term>
		///     <description>field must contain a number that is no smaller than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>pattern</term>
		///     <description>field must match the specified regular expression
		/// </description>
		///   </item>
		/// </list>
		/// 
		/// <para>A simple numbered list.  The <c>term</c> and <c>description</c>
		/// elements are optional for simple string descriptions.</para>
		/// 
		/// <list type="number">
		///   <item>First item</item>
		///   <item>Second item</item>
		///   <item>Third item</item>
		/// </list>
		/// 
		/// <para>This next numbered list uses the optional <c>start</c> attribute to
		/// continue numbering where the last one left off.</para>
		/// 
		/// <list type="number" start="4">
		///   <item>Fourth item</item>
		///   <item>Fifth item</item>
		///   <item>Sixth item</item>
		/// </list>
		/// 
		/// <para>Numbered list with terms and definitions.</para>
		/// 
		/// <list type="number">
		///   <item>
		///     <term>maxLen</term>
		///     <description>field must contain no more than the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>minLen</term>
		///     <description>field must contain at least the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>maxVal</term>
		///     <description>field must contain a number that is no larger than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>minVal</term>
		///     <description>field must contain a number that is no smaller than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>pattern</term>
		///     <description>field must match the specified regular expression
		/// </description>
		///   </item>
		/// </list>
		/// 
		/// <para>Definition list.</para>
		/// 
		/// <list type="definition">
		///   <item>
		///     <term>maxLen</term>
		///     <description>field must contain no more than the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>minLen</term>
		///     <description>field must contain at least the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>maxVal</term>
		///     <description>field must contain a number that is no larger than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>minVal</term>
		///     <description>field must contain a number that is no smaller than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>pattern</term>
		///     <description>field must match the specified regular expression
		/// </description>
		///   </item>
		/// </list>
		///
		/// <para>Two-column table list with terms and definitions.</para>
		/// 
		/// <list type="table">
		///   <listheader>
		///     <term>Item</term>
		///     <description>Description</description>
		///   </listheader>
		///   <item>
		///     <term>maxLen</term>
		///     <description>field must contain no more than the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>minLen</term>
		///     <description>field must contain at least the specified number
		/// of characters</description>
		///   </item>
		///   <item>
		///     <term>maxVal</term>
		///     <description>field must contain a number that is no larger than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>minVal</term>
		///     <description>field must contain a number that is no smaller than the
		/// specified value</description>
		///   </item>
		///   <item>
		///     <term>pattern</term>
		///     <description>field must match the specified regular expression
		/// </description>
		///   </item>
		/// </list>
		/// 
		/// <para>A table with multiple columns.  <c>term</c> or <c>description</c>
		/// can be used to create the columns in each row.</para>
		/// 
		/// <list type="table">
		///   <listheader>
		///     <term>Column 1</term>
		///     <term>Column 2</term>
		///     <term>Column 3</term>
		///     <term>Column 4</term>
		///   </listheader>
		///   <item>
		///     <term>R1, C1</term>
		///     <term>R1, C2</term>
		///     <term>R1, C3</term>
		///     <term>R1, C4</term>
		///   </item>
		///   <item>
		///     <description>R2, C1</description>
		///     <description>R2, C2</description>
		///     <description>R2, C3</description>
		///     <description>R2, C4</description>
		///   </item>
		/// </list>
		/// </remarks>
		public void VariousListExamples()
		{
		}

		/// <summary>
		/// This shows the result of the various <c>note</c> types.
		/// </summary>
		/// <remarks>
		/// <para>These are various examples of the different note types.</para>
		/// 
		/// <note>
		/// This example demonstrates the handling of a <c>note</c> element with no
		/// defined type.  It defaults to the "note" style.
		/// </note>
		/// 
		/// <note type="tip">
		/// Always document your code to help others understand how it is used.
		/// </note>
		/// 
		/// <note type="implement">
		/// Override this method in a derived class to do something useful
		/// </note>
		/// 
		/// <note type="caller">
		/// Calling this implementation will have no effect at all
		/// </note>
		/// 
		/// <note type="inherit">
		/// Types inheriting this base method will have no use for it as it does nothing
		/// </note>
		/// 
		/// <note type="caution">
		/// Use of this method is not recommended.
		/// </note>
		/// 
		/// <note type="warning">
		/// XML is case-sensitive so the note type must be entered as shown in order for
		/// it to be interpreted correctly.
		/// </note>
		/// 
		/// <note type="important">
		/// Calling this method excessively will only slow down your application.
		/// </note>
		/// 
		/// <note type="security">
		/// It is always safe to call this method.
		/// </note>
		/// 
		/// <note type="security note">
		/// This method requires no special privileges
		/// </note>
		/// 
		/// <note type="C#">
		/// Use parenthesis when calling this method in C#.
		/// </note>
		/// 
		/// <note type="VB">
		/// Parenthesis are not required when calling this method in Visual Basic.
		/// </note>
		/// 
		/// <note type="C++">
		/// Use parenthesis when calling this method in C++.
		/// </note>
		/// 
		/// <note type="J#">
		/// Use parenthesis when calling this method in J#.
		/// </note>
		/// 
		/// <para>See the <conceptualLink target="4302a60f-e4f4-4b8d-a451-5f453c4ebd46" />
		/// topic for a full list of all possible note types.</para>
		/// </remarks>
		public virtual void VariousNoteExamples()
		{
		}
	}
}
