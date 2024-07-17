using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Providers.Entities;

/// <summary>
/// �����¼����¼�
/// </summary>
public class LoginDAL
{
    string strCon = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
    public LoginDAL()
    {

    }
    /// <summary>
    /// ����ȷ�ŷ���һ���û��������ֵ������Session����
    /// </summary>
    /// <param name="Uaccount">�˻�</param>
    /// <param name="Upwd">����</param>
    /// <returns>�û����˺ţ�������ת��</returns>
    public string Login(string Uaccount, string Upwd)
    {
        if (Uaccount == null || Upwd == null)
        {
            return null;
        }
        else
        {
            string str = "select Uaccount from User where Uaccount= @Uaccount and Upwd= @Upwd";
            using (MySqlConnection conn = new MySqlConnection(strCon))
            {
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
                cmd.Parameters.AddWithValue("@Upwd", Upwd);
                conn.Open();
                string U = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
                return U;
            }
        }
    }
    /// <summary>
    /// ����ֵΪ1����ͨ��ע���顣ֻ�������һ������Ա
    /// </summary>
    /// <param name="Uaccount">�˻�</param>
    /// <param name="Utype">����</param>
    /// <returns>int��δͨ����鷵��0</returns>
    public int RegistCheck(string Uaccount, string Utype)
    {
        string str = "select distinct Utype from User where Utype=@Utype";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Utype", Utype);
            conn.Open();
            string T = null;
            T = cmd.ExecuteScalar()?.ToString();
            if (T == "admin")//����Ա��ɫ�Ѿ�����
            {
                conn.Close();
                return 0;
            }
            else
            {
                //�˻��Ѵ���
                str = "select Uaccount from User where Uaccount=@Uaccount";
                cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
                string U = null;
                U = cmd.ExecuteScalar()?.ToString();
                conn.Close();
                if (U != null)
                {
                    return 0;
                }
                else return 1;
            }
        }
    }
    /// <summary>
    /// ����Աע�Ṧ��
    /// </summary>
    /// <param name="user"></param>
    /// <returns>int,������0��ִ�гɹ�</returns>
    public int RegistAdmin(User user)
    {
        string str = "insert into User(Uaccount,Upwd,Utype,UName,USex,UAdress,USalary,Ubirth,Uphone) values(@Uaccount,@Upwd,'admin',@UName,@USex,@UAdress,@USalary,@Ubirth,@Uphone)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Uaccount", user.Uaccount);
            cmd.Parameters.AddWithValue("@Upwd", user.Upwd);
            cmd.Parameters.AddWithValue("@UName", user.UName);
            cmd.Parameters.AddWithValue("@USex", user.USex);
            cmd.Parameters.AddWithValue("@UAdress", user.UAdress);
            cmd.Parameters.AddWithValue("@USalary", user.USalary);
            cmd.Parameters.AddWithValue("@Ubirth", user.Ubirth.ToString("G"));
            cmd.Parameters.AddWithValue("@Uphone", user.Uphone);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
    /// <summary>
    /// ��ͨ�û�ע�Ṧ��
    /// </summary>
    /// <param name="user"></param>
    /// <returns>������0��ִ�гɹ�</returns>
    public int RegistUser(User user)
    {
        string str = "insert into User(Uaccount,Upwd,Utype,UName,USex,UAdress,USalary,Ubirth,Uphone) values(@Uaccount,@Upwd,'user',@UName,@USex,@UAdress,@USalary,@Ubirth,@Uphone)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Uaccount", user.Uaccount);
            cmd.Parameters.AddWithValue("@Upwd", user.Upwd);
            cmd.Parameters.AddWithValue("@UName", user.UName);
            cmd.Parameters.AddWithValue("@USex", user.USex);
            cmd.Parameters.AddWithValue("@UAdress", user.UAdress);
            cmd.Parameters.AddWithValue("@USalary", user.USalary);
            cmd.Parameters.AddWithValue("@Ubirth", user.Ubirth);
            cmd.Parameters.AddWithValue("@Uphone", user.Uphone);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
}