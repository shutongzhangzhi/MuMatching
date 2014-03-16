using System.Collections.Generic;
using System.IO;

namespace MuMatching
{
    /// <summary>
    /// 字符串模式匹配器，提供字符串模式匹配的功能。
    /// </summary>
    /// <remarks>
    /// <see cref="StringMatcher"/>是不同算法模型匹配器的抽象基类，<c>Execute</c>是执行具体匹配过程的核心方法，
    /// 它接受来自<see cref="System.String"/>或<see cref="System.IO.TextReader"/>类型的字符序列输入，
    /// 并尝试从输入中匹配目标模式串，最终以可枚举的<see cref="StringMatchHit"/>列表形式返回命中的模式串
    /// 及出现的位置、长度等信息。
    /// </remarks>
    public interface IStringMatcher
    {
        /// <summary>
        /// 从<see cref="System.IO.TextReader"/>类型的输入中匹配目标模式串。
        /// </summary>
        /// <param name="source">输入源。</param>
        /// <returns>命中的模式列表。</returns>
        IEnumerable<StringMatchHit> Execute(TextReader source);

        /// <summary>
        /// 从<see cref="System.String"/>类型的输入中匹配目标模式串。
        /// </summary>
        /// <param name="source">输入源。</param>
        /// <param name="startIndex">开始匹配位置。</param>
        /// <param name="count">匹配的字符数量。</param>
        /// <returns>命中的模式列表。</returns>
        IEnumerable<StringMatchHit> Execute(string source, int startIndex, int count);
    }
}