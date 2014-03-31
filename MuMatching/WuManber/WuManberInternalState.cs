using PrefixDict = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, System.Collections.Generic.List<string>>;
using ShiftTable = System.Collections.Generic.Dictionary<MuMatching.WuManber.Substring, int>;

namespace MuMatching.WuManber
{
    // WM�㷨���ڲ�״̬���ݱ�ʾ
    internal sealed class WuManberInternalState {

        internal const int PREFIX_TABLE_MASK = unchecked((int)0x80000000);    // ǰ׺����������
        internal const int PREFIX_TABLE_UNMASK = ~PREFIX_TABLE_MASK;    
        internal readonly int                   PrefixLength;       // ǰ׺����
        internal readonly int                   BlockLength;        // �ַ��鳤��
        internal readonly PrefixDict[]          PrefixTable;        // �ַ���ǰ׺��
        internal readonly ShiftTable            ShiftTable;         // ��Ծ��
        internal readonly int                   MinPatternLength;   // ��Сģʽ������

        /* 
         * 
         * ShiftTable˵����
         * 
         * ShiftTable���ַ���->ǰ�Ƴ��ȵ�ӳ�䡣ƥ������ʱͨ��ShiftTable����ַ����Ӧ��ǰ�Ƴ��ȣ�
         * ��ǰ�Ƴ���Ϊ0ʱ���ܷ���ģʽƥ�䣬��ʱ��Ҫȡ�ø��ַ����ǰ׺����һ��ȷ��ƥ�䡣
         * ���Ƕ�ShiftTable�е�value�������⴦��
         *  1.��value����λΪ0ʱ��ʾָ��Ҫ�ƶ���ʵ�ʳ��ȡ� 
         *  2.��value����λΪ1������ַ����ǰ�Ƴ���Ϊ0������valueָ����ַ����Ӧ��ǰ׺��������
         *  ���磺
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