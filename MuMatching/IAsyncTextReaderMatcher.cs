using System.IO;

namespace MuMatching
{
    /// <summary>
    /// 以<see cref="System.IO.TextReader"/>作为输入源的异步字符串模式匹配器。
    /// </summary>
    public interface IAsyncTextReaderMatcher
    {
        /// <summary>
        /// 从<see cref="System.IO.TextReader"/>中异步读取字符序列并匹配目标模式串。
        /// </summary>
        /// <param name="source">输入源。</param>
        /// <returns>命中的模式列表。</returns>
        IAsyncEnumerator<StringMatchHit> ExecuteAsync(TextReader source);
    }
}