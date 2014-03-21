using System;
using System.Collections.Generic;
using System.IO;

namespace MuMatching
{
    /// <summary>
    /// 支持对<see cref="System.String"/>进行字符串模式匹配的功能。
    /// </summary>
    /// <remarks>
    /// <see cref="IStringMatcher"/>以<see cref="System.String"/>作为输入源，并从输入中指定位置开始匹配目标模式串，
    /// 将符合条件的子串以及它们的位置等信息以可枚举的<see cref="StringMatchHit"/>列表形式返回。
    /// </remarks>
    public interface IStringMatcher
    {
        /// <summary>
        /// 从<see cref="System.String"/>中匹配目标模式串。
        /// </summary>
        /// <param name="source">输入源。</param>
        /// <param name="startIndex">从输入源开始匹配的位置。</param>
        /// <param name="count">匹配的字符数量。</param>
        /// <returns>命中的模式列表。</returns>
        IEnumerable<StringMatchHit> Execute(string source, int startIndex, int count);

    }
}