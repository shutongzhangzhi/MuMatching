using System.Diagnostics;
using PrefixDict = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, System.Collections.Generic.List<string>>;
using ShiftTable = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, int>;

namespace MuMatching.WuManber
{
    // WM�㷨���ڲ�״̬���ݱ�ʾ
    internal sealed class WuManberInternalState {

        internal struct LengthStruct {
       
            public readonly int         PrefixLength;       // ǰ׺����
            public readonly int         BlockLength;        // �ַ��鳤��
            public readonly int         MinPatternLength;   // ��Сģʽ������

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


        internal const int PREFIX_TABLE_MASK    = unchecked((int)0x80000000);    // ǰ׺������
        internal const int PREFIX_TABLE_UNMASK  = ~PREFIX_TABLE_MASK;    


        internal readonly PrefixDict[]          PrefixTables;                   // �ַ���ǰ׺��
        internal readonly ShiftTable            ShiftTable;                     // ��Ծ��
        internal readonly LengthStruct          LengthInfo;                     // �ַ��鳤�ȵȽṹ��Ϣ

        /* 
         * 
         * ShiftTable˵����
         * 
         * ShiftTable���ַ���->ǰ�Ƴ��ȵ�ӳ�䡣ƥ������ʱͨ��ShiftTable����ַ����Ӧ��ǰ�Ƴ��ȣ�
         * ��ǰ�Ƴ���Ϊ0ʱ���ܷ���ģʽƥ�䣬��ʱ��Ҫȡ�ø��ַ����ǰ׺����һ��ȷ��ƥ�䡣
         * ���Ƕ�ShiftTable�е�value�������⴦��
         *  1.��shiftֵ����λΪ0ʱ��ʾָ��Ҫ�ƶ���ʵ�ʳ��ȡ� 
         *  2.��shiftֵ����λΪ1������ַ����ǰ�Ƴ���Ϊ0������shiftָ����ַ����Ӧ��ǰ׺��������
         *  ���磺
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