using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuMatching
{
    /// <summary>
    /// 支持在泛型集合上进行异步迭代。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    public interface IAsyncEnumerator<out T> : IDisposable
    {
        /// <summary>
        /// 尝试将枚举数推进到集合的下一个元素。
        /// </summary>
        /// <returns>如果枚举数成功地推进到下一个元素，则为 <c>true</c>；如果枚举数越过集合的结尾，则为 <c>false</c>。</returns>
        Task<bool> MoveNextAsync();

        /// <summary>
        /// 获取集合中位于枚举数当前位置的元素。
        /// </summary>
        T Current { get; }
    }

}