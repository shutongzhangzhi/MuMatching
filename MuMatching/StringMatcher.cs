using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MuMatching
{
    /// <summary>
    /// 抽象的字符串模式匹配器。
    /// </summary>
    public abstract class StringMatcher : IStringMatcher
    {
        #region Abstract Members

        /// <summary>
        /// 子类重写时实现从<see cref="System.IO.TextReader"/>中匹配模式的核心过程。
        /// </summary>
        protected abstract IEnumerable<StringMatchHit> ExecuteCore(TextReader source);

        /// <summary>
        /// 子类重写时实现从<see cref="System.String"/>中匹配模式的核心过程。
        /// </summary>
        protected abstract IEnumerable<StringMatchHit> ExecuteCore(string source, int startIndex, int count);

        #endregion

        #region IStringMatcher Members

        /// <inheritdoc />
        public IEnumerable<StringMatchHit> Execute(TextReader source)
        {
            Contract.Requires<ArgumentNullException>(source != null);

            return ExecuteCore(source);
        }

        /// <inheritdoc />
        public IEnumerable<StringMatchHit> Execute(string source, int startIndex, int count)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentOutOfRangeException>(startIndex >= 0 && startIndex < source.Length);
            Contract.Requires<ArgumentOutOfRangeException>(count >= 0 && startIndex + count <= source.Length);

            if (source.Length == 0 || count == 0) { return Enumerable.Empty<StringMatchHit>(); }

            return ExecuteCore(source, startIndex, count);
        }

        #endregion
    }
}