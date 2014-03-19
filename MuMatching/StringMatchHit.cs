namespace MuMatching {
    /// <summary>
    /// 被符串模式匹配命中的结构信息。
    /// </summary>
    public struct StringMatchHit {
        private readonly int _index;
        private readonly string _target;
        private readonly string _parttern;

        public StringMatchHit(int index, string target, string parttern)
            : this() {

            _index = index;
            _target = target;
            _parttern = parttern;
        }

        /// <summary>
        /// 目标串从0开始的绝对索引位置。
        /// </summary>
        public int Index { get { return _index; } }

        /// <summary>
        /// 命中的目标串。
        /// </summary>
        public string Target { get { return _target; } }

        /// <summary>
        /// 命中的模式串。
        /// </summary>
        public string Parttern { get { return _parttern; } }
    }
}