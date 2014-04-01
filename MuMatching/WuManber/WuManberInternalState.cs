using System.Diagnostics;
using PrefixDict = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, System.Collections.Generic.List<string>>;
using ShiftTable = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, int>;

namespace MuMatching.WuManber
{
    // WM算法的内部状态数据表示
    internal sealed class WuManberInternalState {

        internal struct LengthStruct {
       
            public readonly int         PrefixLength;       // 前缀长度
            public readonly int         BlockLength;        // 字符块长度
            public readonly int         MinPatternLength;   // 最小模式串长度

            public LengthStruct(int prefixLength, int blockLength, int minPatternLength)
                : this() {

                Debug.Assert(prefixLength > 0);
                Debug.Assert(blockLength > 0);
                Debug.Assert(minPatternLength > 0);
                Debug.Assert(prefixLength <= minPatternLength);
                Debug.Assert(blockLength <= minPatternLength);

                PrefixLength            = prefixLength;        
                BlockLength             = blockLength;          
                MinPatternLength        = minPatternLength;    
            }

        }


        internal const int PREFIX_TABLE_MASK    = unchecked((int)0x80000000);    // 前缀表掩码
        internal const int PREFIX_TABLE_UNMASK  = ~PREFIX_TABLE_MASK;    


        internal readonly PrefixDict[]          PrefixTables;                   // 字符块前缀表
        internal readonly ShiftTable            ShiftTable;                     // 跳跃表
        internal readonly LengthStruct          LengthInfo;                     // 字符块长度等结构信息

        /* 
         * 
         * ShiftTable说明：
         * 
         * ShiftTable是字符块->前移长度的映射。匹配运行时通过ShiftTable获得字符块对应的前移长度，
         * 当前移长度为0时可能发生模式匹配，这时需要取得该字符块的前缀表，进一步确认匹配。
         * 我们对ShiftTable中的value进行特殊处理：
         *  1.当shift值符号位为0时表示指针要移动的实际长度。 
         *  2.当shift值符号位为1代表该字符块的前移长度为0，并且shift指向该字符块对应的前缀表索引。
         *  例如：
         *      if(shift > 0)) {
         *          currentIndex += shift;
         *      } else {
         *          var prefixDict = state.GetPrefixTable(shift);
         *          ...
         *          ...
         *      }
         * 
         */

        public WuManberInternalState(
            int prefixLength, int blockLength, int minPatternLength, PrefixDict[] prefixTables, ShiftTable shiftTable)
        {
            PrefixTables        = prefixTables;
            ShiftTable          = shiftTable;
            LengthInfo          = new LengthStruct(prefixLength, blockLength, minPatternLength);
        }


        internal PrefixDict GetPrefixTable(int prefixTableIndex) {
            return PrefixTables[prefixTableIndex & PREFIX_TABLE_UNMASK];
        }

        internal static int UnmaskPrefixTableIndex(int maskPrefixTableIdx) {
            return maskPrefixTableIdx & PREFIX_TABLE_UNMASK;
        }

        internal static int MaskPrefixTableIndex(int prefixTableIdx) {
            return prefixTableIdx | PREFIX_TABLE_MASK;
        }
    }


}