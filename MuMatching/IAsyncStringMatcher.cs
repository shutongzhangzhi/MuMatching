namespace MuMatching
{

    /// <summary>
    /// 支持对<see cref="System.String"/>进行异步的字符串模式匹配的功能。
    /// </summary>
    public interface IAsyncStringMatcher
    {
        /// <summary>
        /// 执行异步模式匹配。
        /// </summary>
        /// <param name="source">输入源。</param>
        /// <param name="startIndex">从输入源开始匹配的位置。</param>
        /// <param name="count">匹配的字符数量。</param>
        /// <returns>异步获取的命中模式列表。</returns>
        IAsyncEnumerable<StringMatchHit> ExecuteAsync(string source, int startIndex, int count);
    }
}