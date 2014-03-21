using System;
using System.Collections.Generic;
using System.IO;

namespace MuMatching
{
    /// <summary>
    /// ֧�ֶ�<see cref="System.String"/>�����ַ���ģʽƥ��Ĺ��ܡ�
    /// </summary>
    /// <remarks>
    /// <see cref="IStringMatcher"/>��<see cref="System.String"/>��Ϊ����Դ������������ָ��λ�ÿ�ʼƥ��Ŀ��ģʽ����
    /// �������������Ӵ��Լ����ǵ�λ�õ���Ϣ�Կ�ö�ٵ�<see cref="StringMatchHit"/>�б���ʽ���ء�
    /// </remarks>
    public interface IStringMatcher
    {
        /// <summary>
        /// ��<see cref="System.String"/>��ƥ��Ŀ��ģʽ����
        /// </summary>
        /// <param name="source">����Դ��</param>
        /// <param name="startIndex">������Դ��ʼƥ���λ�á�</param>
        /// <param name="count">ƥ����ַ�������</param>
        /// <returns>���е�ģʽ�б�</returns>
        IEnumerable<StringMatchHit> Execute(string source, int startIndex, int count);

    }
}