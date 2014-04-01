using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace MuMatching.WuManber
{
    /// <summary>
    /// 代表某目标字符串的子串。
    /// </summary>
    internal struct Substring : IEquatable<Substring> {

        internal readonly string    Target;         // 目标字符串
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

        public unsafe override int GetHashCode() {

            const int SEED  = 131;
            var hash = 0;

            fixed (char* p_char = Target) {

                char* p_target_start    = p_char + StartIndex;
                char* p_target_end      = p_target_start + Length;

                while (p_target_start < p_target_end) {
                    hash = hash * SEED + *p_target_start++;
                }
            }

            return hash;
        }

        public override bool Equals(object obj) {

            if (obj == null) { return false; }
            if (obj.GetType() != typeof(Substring)) { return false; }
            return Equals((Substring)obj);
        }

        public override string ToString() { return Target.Substring(StartIndex, Length); }

        #endregion

        #region IEquatable<Substring> Members
        public bool Equals(Substring other) {

            if (other.Length != Length) { return false; }
            return String.CompareOrdinal(
                Target, StartIndex, other.Target, other.StartIndex, Length) == 0;
        }
        #endregion

        internal static Substring Create(string target, int startIndex, int length) {
            return new Substring(target, startIndex, length);
        }

        internal static Substring Create(string target) {
            return new Substring(target, 0, target.Length);
        }
    }
}

