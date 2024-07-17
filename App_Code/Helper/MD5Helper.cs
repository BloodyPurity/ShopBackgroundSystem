using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// MD5Helper ��ժҪ˵��
/// </summary>
public class MD5Helper
{
    public static string GetMD5(string input)
    {
        StringBuilder sb = new StringBuilder();
        MD5 md5 = MD5.Create();
        //�������ַ���ת�����ֽ�����
        byte[] buffer = Encoding.UTF8.GetBytes(input);
        //MD5����
        byte[] md5uffer = md5.ComputeHash(buffer);
        //��ÿһ�������ļ����ֽ�ת�����ַ���
        foreach (byte item in md5uffer)
        {
            //������16���Ƶ���ʽ��ʾ����
            //��ÿ���ַ�ռ�����ֽ�����ʾ
            sb.Append(item.ToString("x2"));
        }
        return sb.ToString();
    }
}