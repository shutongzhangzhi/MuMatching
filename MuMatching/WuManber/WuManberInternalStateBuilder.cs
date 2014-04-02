using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MuMatching.WuManber {

    internal sealed class WuManberInternalStateBuilder {

        private const int DEFAULT_BLOCK_LENGTH = 2;
        private readonly IEnumerable<string>                    _initPatterns;
        private int                                             _minPatternLength;
        private int                                             _blockLength;
        private int                                             _prefixLength;
        private int                                             _maxSubStartIndex;
        private Dictionary<Substring, int>                      _shiftTable;
        private List<Dictionary<Substring, List<string>>>       _prefixTables;

        internal WuManberInternalStateBuilder(int minPatternLength)
        {
            Debug.Assert(minPatternLength > 0);

            _minPatternLength = minPatternLength;
        }

        internal WuManberInternalStateBuilder(IEnumerable<string> initPatterns)
        {
            Debug.Assert(initPatterns != null);

            _initPatterns = initPatterns;
        }

        #region Initialize

        internal void Initialize() {
            
            var patternsCount       = 16;
            if (_minPatternLength == 0) {
                // 从初始模式列表中获得最小模式长度
                _minPatternLength = GetMinPatternLength(_initPatterns, ref patternsCount);
            }

            _blockLength            = Math.Min(DEFAULT_BLOCK_LENGTH, _minPatternLength);
            _prefixLength           = _blockLength;
            _maxSubStartIndex       = _minPatternLength - _blockLength;

            // 根据模式规模大小初始化ShiftTable 和PrefixTables
            const float FORECAST_RATIO  = 1.0F;  // 根据一定样本测试得出，可能并不适用任何情况
            var capacity            = (int) ((_minPatternLength - _blockLength + 1) * patternsCount * FORECAST_RATIO);
            _shiftTable             = new Dictionary<Substring, int>(capacity);
            _prefixTables           = new List<Dictionary<Substring, List<string>>>(capacity);

            // 添加初始模式列表
            if (_initPatterns != null) { AddPatterns(_initPatterns); }
        }

        #endregion

        #region Private Helpers

        private static int GetMinPatternLength(IEnumerable<string> patterns, ref int initPatternCount) {

            int minLength = 0;
            int count = 0;

            foreach (var pattern in patterns) {
                if (!String.IsNullOrEmpty(pattern)) {
                    if (minLength == 0) {
                        minLength = pattern.Length;
                    }

                    if (pattern.Length < minLength) {
                        minLength = pattern.Length;
                    }
                    count++;
                }
            }

            if (count > initPatternCount) {
                initPatternCount = count;
            }

            return minLength == 0 ? 1 : minLength;
        }

        private void AddPattern(string pattern) {
            for (int start = 0; start <= _maxSubStartIndex; start++) {

                /*
                 * 设所有模式中最短模式的长度为m，对每个模式的前m个字符
                 * 所有长度为b的字符块进行处理，计算每个字符块尾字符与m
                 * 之间的距离n，n为该字符块的shift值；当遇到相同字符块
                 * 时n取最小值。
                 * 
                 * 假设当前模式为：abcdef，m=5，b=2 则：
                 * shift["ab"] = 3
                 * shift["bc"] = 2
                 * shift["cd"] = 1
                 * shift["de"] = 0
                 * 
                 * 假设另有模式：decabf 则：shift表更新为：
                 * shift["ab"] = 0  !! ab在decabf中shift值更小
                 * shift["bc"] = 2
                 * shift["cd"] = 1
                 * shift["de"] = 0
                 * shift["ec"] = 2
                 * shift["ca"] = 1
                 * 
                 */
                var block = Substring.Create(pattern, start, _blockLength);
                var distance = _maxSubStartIndex - start;
                int minDistance;

                if (!_shiftTable.TryGetValue(block, out minDistance) || distance < minDistance) {
                    _shiftTable[block] = distance;
                }

                /*
                 * 当字符块n值为0时将当前模式的前几个字符与当前模式加入
                 * 字符块的前缀关系表中，最后把字符块的shift值n更新为前
                 * 缀关系表的索引。
                 * 
                 * 假设现有模式串列表：
                 * ["abcdef", "decabf", "eecab", "abbde"] 且m=5, b=2，那么生成数据结构如：
                 * 
                 * -------------+------------------------
                 *  ShiftTable  | PrefixTables            
                 * -------------+------------------------
                 *              +----+      +--------+
                 * ["ab"=0]---->|"de"|----->|"decabf"|
                 * ["bc"=2]     |"ee"|--\   +--------+
                 * ["cd"=1]     +----+   \->+--------+
                 * ["de"=0]---->+----+      |"eecab" |
                 * ["ec"=2]     |"ab"|--\   +--------+
                 * ["ca"=1]     +----+   \->+--------+
                 * ["ee"=3]                 |"abcdef"|
                 * ["bb"=2]                 |"abbde" |
                 *                          +--------+
                 *                    
                 * ab,de 实际不等于0
                 *                          
                 * 注意：这与Wu-Manber原始论文结构是有差异的
                 * 
                 */
                if (distance == 0) {

                    var prefix = Substring.Create(pattern, 0, _prefixLength);
                    int prefixTableIndex;

                    if (minDistance < 0) {
                        // 当前字符块已经存在前缀表直接取出表索引
                        prefixTableIndex = WuManberInternalState.UnmaskPrefixTableIndex(minDistance);
                    }
                    else {
                        // 创建前缀关系表并更新字符块shift值
                        prefixTableIndex = CreatePrefixTable();
                        _shiftTable[block] = WuManberInternalState.MaskPrefixTableIndex(prefixTableIndex);
                    }

                    AddPrefix(prefixTableIndex, prefix, pattern);
                }
            }
        }

        private int CreatePrefixTable() {

            // TODO: capacity应该可预测集合大小
            _prefixTables.Add(new Dictionary<Substring, List<string>>());
            return _prefixTables.Count - 1;

        }

        private void AddPrefix(int prefixTableIndex, Substring prefix, string pattern) {

            var prefixTable = _prefixTables[prefixTableIndex];
            List<string> patterns;

            if (prefixTable.TryGetValue(prefix, out patterns)) {
                patterns.Add(pattern);
            }
            else {
                prefixTable[prefix] = new List<string> { pattern };
            }
        }

        #endregion

        #region Public APIs

        /// <summary>
        /// 向生成器添加模式列表。
        /// </summary>
        /// <param name="patterns">模式列表。</param>
        internal void AddPatterns(IEnumerable<string> patterns) {
            Debug.Assert(patterns != null);

            foreach (var pattern in patterns) {
                if (!String.IsNullOrEmpty(pattern)) {

                    if (pattern.Length < _minPatternLength) {
                        throw new ArgumentException(
                            String.Format("pattern:{0} length less than MinPatternLength:{1}.",
                                pattern, _minPatternLength.ToString()), "patterns");
                    }

                    AddPattern(pattern);
                }
            }
        }

        /// <summary>
        /// 生成内部状态。
        /// </summary>
        internal WuManberInternalState Build() {
            return new WuManberInternalState( _prefixLength, _blockLength, _minPatternLength,
                _prefixTables.ToArray(), _shiftTable);
        }

        #endregion

    }
}
