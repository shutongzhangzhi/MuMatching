using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MuMatching.WuManber {
    internal sealed class WuManberInternalStateBuilder {
        private const int DEFAULT_BLOCK_LENGTH = 2;
        private int _minPatternLength;
        private int _blockLength;
        private int _prefixLength;
        private int _maxSubStartIndex;
        private Dictionary<Substring, int> _shiftTable;
        private List<Dictionary<Substring, List<string>>> _prefixTables;

        #region Initizlize

        public void Initialize(int minPatternLength) {
            Debug.Assert(minPatternLength > 0);

            _minPatternLength = minPatternLength;
            _blockLength = Math.Min(DEFAULT_BLOCK_LENGTH, minPatternLength);
            _prefixLength = _blockLength;
            _maxSubStartIndex = _minPatternLength - _blockLength;
            _shiftTable = new Dictionary<Substring, int>();
            _prefixTables = new List<Dictionary<Substring, List<string>>>();
        }

        #endregion
        public void AddPatterns(IEnumerable<string> patterns) {
            Debug.Assert(patterns != null);

            foreach (var pattern in patterns) {
                if (!String.IsNullOrEmpty(pattern)) {
                    AddPattern(pattern);
                }
            }
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

        private int CreatePrefixTable() {
            _prefixTables.Add(new Dictionary<Substring, List<string>>());
            return _prefixTables.Count - 1;
        }


        internal WuManberInternalState Build() {
            return new WuManberInternalState(
                _prefixLength, _blockLength, _prefixTables.ToArray(),
                _shiftTable, _minPatternLength);
        }
    }
}
