using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;

namespace AdapterTest
{
    /// <summary>
    /// �ַ�������ת����
    /// </summary>
    public class StringEncoding
    {
        private StringEncoding()
        {
        }

        /// <summary>
        /// ���ַ���ת��Ϊ��������
        /// </summary>
        public static string ToSimplifiedChinese(string s)
        {
            return Microsoft.VisualBasic.Strings.StrConv(s, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// ���ļ�ת��Ϊ��������
        /// </summary>
        /// <param name="filename">Դ�ļ���</param>
        /// <param name="encoding">Դ�ļ��ַ�����</param>
        /// <param name="outFilename">Ŀ���ļ���</param>
        /// <param name="outEncoding">Ŀ���ļ��ַ�����</param>
        /// <example>
        /// <code>
        /// ToSimplifiedChinese("big5.txt", Encoding.GetEncoding("big5"), "gb.txt", Encoding.GetEncoding("gb2312"));
        /// ToSimplifiedChinese("big5.txt", Encoding.GetEncoding("big5"), "gb.txt", Encoding.UTF8);
        /// </code>
        /// </example>
        public static void ToSimplifiedChinese(string filename, Encoding encoding, string outFilename, Encoding outEncoding)
        {
            StreamReader r = new StreamReader(filename, encoding);
            StreamWriter w = new StreamWriter(outFilename, false, outEncoding);
            try
            {
                w.Write(Strings.StrConv(r.ReadToEnd(), VbStrConv.SimplifiedChinese, 0));
                w.Flush();
            }
            finally
            {
                w.Close();
                r.Close();
            }
        }

        /// <summary>
        /// ���ַ���ת��Ϊ��������
        /// </summary>
        public static string ToTraditionalChinese(string s)
        {
            return Microsoft.VisualBasic.Strings.StrConv(s, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// ���ļ�ת��Ϊ��������
        /// </summary>
        /// <param name="filename">Դ�ļ���</param>
        /// <param name="encoding">Դ�ļ��ַ�����</param>
        /// <param name="outFilename">Ŀ���ļ���</param>
        /// <param name="outEncoding">Ŀ���ļ��ַ�����</param>
        /// <example>
        /// <code>
        /// ToTraditionalChinese("gb.txt", Encoding.GetEncoding("gb2312"), "gb.txt", Encoding.GetEncoding("big5"));
        /// ToTraditionalChinese("gb.txt", Encoding.GetEncoding("gb2312"), "gb.txt", Encoding.UTF8);
        /// </code>
        /// </example>
        public static void ToTraditionalChinese(string filename, Encoding encoding, string outFilename, Encoding outEncoding)
        {
            StreamReader r = new StreamReader(filename, encoding);
            StreamWriter w = new StreamWriter(outFilename, false, outEncoding);
            try
            {
                w.Write(Strings.StrConv(r.ReadToEnd(), VbStrConv.TraditionalChinese, 0));
                w.Flush();
            }
            finally
            {
                w.Close();
                r.Close();
            }
        }
    }

}
