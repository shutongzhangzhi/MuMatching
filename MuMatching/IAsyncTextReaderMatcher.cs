using System.IO;

namespace MuMatching
{
    /// <summary>
    /// 支持对<see cref="System.IO.TextReader"/>进行异步的字符串模式匹配器。
    /// </summary>
    public interface IAsyncTextReaderMatcher
    {
        /// <summary>
        /// 从<see cref="System.IO.TextReader"/>中异步读取字符序列并完成异步匹配。
        /// </summary>
        /// <param name="source">输入源。</param>
        /// <returns>命中的模式列表。</returns>
        IAsyncEnumerable<StringMatchHit> ExecuteAsync(TextReader source);
    }
}