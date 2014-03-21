namespace MuMatching
{

    /// <summary>
    /// ֧�ֶ�<see cref="System.String"/>�����첽���ַ���ģʽƥ��Ĺ��ܡ�
    /// </summary>
    public interface IAsyncStringMatcher
    {
        /// <summary>
        /// ִ���첽ģʽƥ�䡣
        /// </summary>
        /// <param name="source">����Դ��</param>
        /// <param name="startIndex">������Դ��ʼƥ���λ�á�</param>
        /// <param name="count">ƥ����ַ�������</param>
        /// <returns>�첽��ȡ������ģʽ�б�</returns>
        IAsyncEnumerable<StringMatchHit> ExecuteAsync(string source, int startIndex, int count);
    }
}