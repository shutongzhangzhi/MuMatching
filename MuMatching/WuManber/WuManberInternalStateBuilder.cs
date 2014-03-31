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


        #region Initizlize

        public void Initialize(int minPatternLength) {
            Debug.Assert(minPatternLength > 0);

            _minPatternLength = minPatternLength;
            _blockLength = Math.Min(DEFAULT_BLOCK_LENGTH, minPatternLength);
            _prefixLength = _blockLength;
            _maxSubStartIndex = _minPatternLength - _blockLength;
            _shiftTable = new Dictionary<Substring, int>();
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
                var span = _maxSubStartIndex - start;
                int minSpan;

                if (!_shiftTable.TryGetValue(block, out minSpan) || span < minSpan) {
                    _shiftTable[block] = span;
                }

                //if (span == 0) {
                //    var prefix = Substring.Create(pattern, 0, _prefixLength);
                //    int prefixTableIndex;

                //    if ((minSpan & WMInternalState.PREFIX_TABLE_MASK) == WMInternalState.PREFIX_TABLE_MASK) {
                //        prefixTableIndex = minSpan & WMInternalState.PREFIX_TABLE_UNMASK;
                //    }
                //    else {
                //        prefixTableIndex = CreatePrefixTable();
                //        _shiftTable[block] = prefixTableIndex | WMInternalState.PREFIX_TABLE_MASK;
                //    }

                //    AddPrefix(prefixTableIndex, prefix, pattern);
                //}
            }
        }

        private static int CreatePrefixTableIndex(int prefixTableIndex) {
            return prefixTableIndex | WuManberInternalState.PREFIX_TABLE_MASK;
        }

        private void AddPrefix(int prefixTableIndex, string prefix, string pattern) {
            throw new NotImplementedException();
        }

        private int CreatePrefixTable() {
            throw new NotImplementedException();
        }

        private int GetPrefixTableIndex(int minSpan) {
            throw new NotImplementedException();
        }




        internal WuManberInternalState Build() {
            return new WuManberInternalState(
                _prefixLength, _blockLength, new Dictionary<Substring, List<string>>[0],
                _shiftTable, _minPatternLength);
        }
    }
}
