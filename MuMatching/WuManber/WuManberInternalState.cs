using PrefixDict = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, System.Collections.Generic.List<string>>;
using ShiftTable = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, int>;

namespace MuMatching.WuManber
{
    // WM算法的内部状态数据表示
    internal sealed class WuManberInternalState {

        internal const int PREFIX_TABLE_MASK = unchecked((int)0x80000000);    // 前缀表索引掩码
        internal const int PREFIX_TABLE_UNMASK = ~PREFIX_TABLE_MASK;    
        internal readonly int                   PrefixLength;       // 前缀长度
        internal readonly int                   BlockLength;        // 字符块长度
        internal readonly PrefixDict[]          PrefixTable;        // 字符块前缀表
        internal readonly ShiftTable            ShiftTable;         // 跳跃表
        internal readonly int                   MinPatternLength;   // 最小模式串长度

        /* 
         * 
         * ShiftTable说明：
         * 
         * ShiftTable是字符块->前移长度的映射。匹配运行时通过ShiftTable获得字符块对应的前移长度，
         * 当前移长度为0时可能发生模式匹配，这时需要取得该字符块的前缀表，进一步确认匹配。
         * 我们对ShiftTable中的value进行特殊处理：
         *  1.当value符号位为0时表示指针要移动的实际长度。 
         *  2.当value符号位为1代表该字符块的前移长度为0，并且value指向该字符块对应的前缀表索引。
         *  例如：
         *      if( (value & PREFIX_TABLE_INDEX_MASK) != PREFIX_TABLE_INDEX_MASK) {
         *          currentIndex += value;
         *      } else {
         *          var prefixDict = PrefixTable[value & 0x7FFFFFFF];
         *          ...
         *          ...
         *      }
         * 
         */

        public WuManberInternalState(
            int prefixLength, int blockLength, PrefixDict[] prefixTable, ShiftTable shiftTable, int minPatternLength)
        {
            PrefixLength        = prefixLength;
            BlockLength         = blockLength;
            PrefixTable         = prefixTable;
            ShiftTable          = shiftTable;
            MinPatternLength    = minPatternLength;
        }

    }


}