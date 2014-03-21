using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace MuMatching.WM {
    /// <summary>
    /// 代表某字符串的子串。
    /// </summary>
    internal struct Substring {

        internal readonly string    Target;         // 目标字符串。 
        internal readonly int       StartIndex;     // 子串位于目标串的开始位置
        internal readonly int       Length;         // 子串长度

        public Substring(string target, int startIndex, int length)
            : this() {

            Debug.Assert(target != null);
            Debug.Assert(startIndex >= 0 && startIndex < target.Length);
            Debug.Assert(length >= 0 && (startIndex + length) <= target.Length);

            Target          = target;
            StartIndex      = startIndex;
            Length          = length;
        }

        #region Override Members

        public override string ToString() { return Target.Substring(StartIndex, Length); }

        #endregion
    }
}

