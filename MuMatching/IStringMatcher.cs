using System.Collections.Generic;
using System.IO;

namespace MuMatching
{
    /// <summary>
    /// �ַ���ģʽƥ�������ṩ�ַ���ģʽƥ��Ĺ��ܡ�
    /// </summary>
    /// <remarks>
    /// <see cref="StringMatcher"/>�ǲ�ͬ�㷨ģ��ƥ�����ĳ�����࣬<c>Execute</c>��ִ�о���ƥ����̵ĺ��ķ�����
    /// ����������<see cref="System.String"/>��<see cref="System.IO.TextReader"/>���͵��ַ��������룬
    /// �����Դ�������ƥ��Ŀ��ģʽ���������Կ�ö�ٵ�<see cref="StringMatchHit"/>�б���ʽ�������е�ģʽ��
    /// �����ֵ�λ�á����ȵ���Ϣ��
    /// </remarks>
    public interface IStringMatcher
    {
        /// <summary>
        /// ��<see cref="System.IO.TextReader"/>���͵�������ƥ��Ŀ��ģʽ����
        /// </summary>
        /// <param name="source">����Դ��</param>
        /// <returns>���е�ģʽ�б�</returns>
        IEnumerable<StringMatchHit> Execute(TextReader source);

        /// <summary>
        /// ��<see cref="System.String"/>���͵�������ƥ��Ŀ��ģʽ����
        /// </summary>
        /// <param name="source">����Դ��</param>
        /// <param name="startIndex">��ʼƥ��λ�á�</param>
        /// <param name="count">ƥ����ַ�������</param>
        /// <returns>���е�ģʽ�б�</returns>
        IEnumerable<StringMatchHit> Execute(string source, int startIndex, int count);
    }
}