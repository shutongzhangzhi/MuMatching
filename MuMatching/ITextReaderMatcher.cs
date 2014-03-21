using System.Collections.Generic;
using System.IO;

namespace MuMatching
{
    /// <summary>
    /// ֧�ֶ�<see cref="System.IO.TextReader"/>�����ַ���ģʽƥ��Ĺ��ܡ�
    /// </summary>
    /// <remarks>
    /// <see cref="ITextReaderMatcher"/>��<see cref="System.IO.TextReader"/>�ж�ȡ�ַ����У�����
    /// �ַ�������ƥ��Ŀ��ģʽ�����������������ַ����м����ǵ�λ�õ���Ϣ�Կ�ö�ٵ�<see cref="StringMatchHit"/>
    /// �б���ʽ���ء�
    /// </remarks>
    public interface ITextReaderMatcher
    {
        /// <summary>
        /// ��<see cref="System.IO.TextReader"/>�ж�ȡ�ַ����в�ƥ��Ŀ��ģʽ����
        /// </summary>
        /// <param name="source">����Դ��</param>
        /// <returns>���е�ģʽ�б�</returns>
        IEnumerable<StringMatchHit> Execute(TextReader source);
    }
}