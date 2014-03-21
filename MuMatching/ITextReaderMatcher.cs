using System.Collections.Generic;
using System.IO;

namespace MuMatching
{
    /// <summary>
    /// 支持对<see cref="System.IO.TextReader"/>进行字符串模式匹配的功能。
    /// </summary>
    /// <remarks>
    /// <see cref="ITextReaderMatcher"/>从<see cref="System.IO.TextReader"/>中读取字符序列，并从
    /// 字符序列中匹配目标模式串，将符合条件的字符序列及它们的位置等信息以可枚举的<see cref="StringMatchHit"/>
    /// 列表形式返回。
    /// </remarks>
    public interface ITextReaderMatcher
    {
        /// <summary>
        /// 从<see cref="System.IO.TextReader"/>中读取字符序列并匹配目标模式串。
        /// </summary>
        /// <param name="source">输入源。</param>
        /// <returns>命中的模式列表。</returns>
        IEnumerable<StringMatchHit> Execute(TextReader source);
    }
}