using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuMatching
{
    /// <summary>
    /// 异步枚举器。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    public interface IAsyncEnumerator<out T> : IEnumerator<T>
    {
        /// <summary>
        /// 尝试异步获取下一个元素。
        /// </summary>
        /// <returns><c>true</c>： 获取成功，<c>false</c>： 获取失败。</returns>
        Task<bool> MoveNextAsync();
    }
}