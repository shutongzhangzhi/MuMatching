using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuMatching
{
    /// <summary>
    /// 提供在指定类型的集合上进行异步迭代的功能。
    /// </summary>
    /// <typeparam name="T">要枚举的对象的类型。</typeparam>
    public interface IAsyncEnumerable<out T>
    {
        /// <summary>
        /// 返回一个循环访问集合的异步枚举器。
        /// </summary>
        IAsyncEnumerator<T> GetEnumerator();
    }
}
