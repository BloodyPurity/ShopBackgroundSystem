using Microsoft.Owin.BuilderProperties;
using MySql.Data.MySqlClient;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Providers.Entities;

/// <summary>
/// �����û�����ҵ��
/// </summary>
public class UserDAL
{
    string strCon = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
    public UserDAL()
    {
    }
    /// <summary>
    /// ����һ���û����͵�string�������ж��Ƿ�����û�����ĳЩҳ��
    /// </summary>
    /// <param name="Uaccount">�˻�</param>
    /// <returns>�û�����:Utype</returns>
    public string Utype(string Uaccount)
    {
        string str = "select Utype from User where Uaccount=@Uaccount";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            conn.Open();
            string T = null;
            T = cmd.ExecuteScalar()?.ToString();
            conn.Close();
            return T;
        }
    }
    /// <summary>
    /// ����һ��stringList���͵Ķ�����������Դ���޳���UID��������û�������Ϣ��
    /// </summary>
    /// <param name="Uaccount">�˺�</param>
    /// <returns>һ���������û����в�ĵ���Ϣ��</returns>
    public List<string> Userinfo(string Uaccount)
    {
        string str = "select Uaccount,UName,USex,UAdress,USalary,Ubirth,Uphone from User where Uaccount=@Uaccount";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            List<string> T = new List<string>();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr[i].ToString() != "")
                    {
                        T.Add(dr[i].ToString());
                    }
                    else
                    {
                        T.Add("");
                    }
                }
            }
            //T.RemoveAt(T.Count-1);
            return T;
        }
    }
    /// <summary>
    /// �����û�����ҳ�ĸ��£��˻����û����͵�Ȼ���޷����ĵġ�
    /// �û������������Ҫ�˳���
    /// </summary>
    /// <param name="user">�û�</param>
    /// <param name="Uaccount">�û����˻�</param>
    /// <returns>true��������Ѿ���������򷵻�false</returns>
    public bool UpdateDetail(User user, string Uaccount)
    {
        string str = "select Upwd from User where Uaccount=@Uaccount";
        string str1 = "update User set UName =@UName,USex =@USex,UAdress=@UAdress,USalary =@USalary,Uphone=@Uphone,Ubirth=@Ubirth where Uaccount=@Uaccount";
        string str2 = "select Upwd from User where Uaccount=@Uaccount";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);//�޸�ǰ������
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            MySqlCommand cmd1 = new MySqlCommand(str1, conn);//�޸�����
            cmd1.Parameters.AddWithValue("@UName", user.UName);
            cmd1.Parameters.AddWithValue("@USex", user.USex);
            cmd1.Parameters.AddWithValue("@UAdress", user.UAdress);
            cmd1.Parameters.AddWithValue("@USalary", user.USalary);
            cmd1.Parameters.AddWithValue("@Uphone", user.Uphone);
            cmd1.Parameters.AddWithValue("@Ubirth", user.Ubirth.ToString("G"));
            cmd1.Parameters.AddWithValue("@Uaccount", Uaccount);
            MySqlCommand cmd2 = new MySqlCommand(str2, conn);//�޸ĺ������
            cmd2.Parameters.AddWithValue("@Uaccount", Uaccount);
            conn.Open();
            string pwd = cmd.ExecuteScalar().ToString();
            cmd1.ExecuteNonQuery();
            string pwd1 = cmd2.ExecuteScalar().ToString();
            conn.Close();
            if (pwd == pwd1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    /// <summary>
    /// ����Utype�������е��û���Ϣ���ݼ���
    /// </summary>
    /// <param name="Utype">//�û�����</param>
    /// <returns>һ�����ݼ�������������������Դ��</returns>
    public DataSet AllUserinfo(string Utype)
    {
        if (Utype == "admin")
        {
            string sqlstr = "select UID,Uaccount,UType,UName,USex,UAdress,USalary,Ubirth,Uphone from User";
            MySqlDataAdapter da;
            DataSet set = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strCon))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                da = new MySqlDataAdapter(cmd);
                da.Fill(set);
                conn.Close();
                return set;
            }
        }
        else { return null; }
    }
    /// <summary>
    /// ���õĴ���
    /// </summary>
    /// <param name="Utype"></param>
    /// <param name="UName"></param>
    /// <returns></returns>
    [Obsolete]
    public DataSet SelectedUserinfo(string Utype,string UName)
    {
        if (Utype == "admin")
        {
            string sqlstr = "select UID,Uaccount,UType,UName,USex,UAdress,USalary,Ubirth,Uphone from User where UName=@UName";
            MySqlDataAdapter da;
            DataSet set = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strCon))
            {
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                cmd.Parameters.AddWithValue("@UName", UName);
                conn.Open();
                da = new MySqlDataAdapter(cmd);
                da.Fill(set);
                conn.Close();
                return set;
            }
        }
        else { return null; }
    }
    /// <summary>
    /// ���ڹ���Ա�ĵ��и��£����û�����ҳ���µ��������������Բ鿴�����˵���Ϣ��
    /// </summary>
    /// <param name="user">��Ҫ�޸ĵ��û�������Ϣ</param>
    /// <param name="UID">Ҫ���ĵ��û���UID��Ψһ��ʶ</param>
    /// <param name="adminUaccount">����Ա���˻���Ϣ����ô�Session��ȡ��</param>
    /// <returns>false�������ʧ�ܣ������Ƿǹ���Ա��ɫ��true���û�г������������</returns>
    public bool UpdateRow(User user, int UID, string adminUaccount)
    {
        string str,str1;
        str = "update User set UName =@UName,USex =@USex,UAdress=@UAdress,USalary =@USalary,Uphone=@Uphone where UID=@UID";
        str1 = "select Utype from User where Uaccount=@Uaccount";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd1 = new MySqlCommand(str1, conn);
            cmd1.Parameters.AddWithValue("@Uaccount",adminUaccount);
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@UName", user.UName);
            cmd.Parameters.AddWithValue("@USex", user.USex);
            cmd.Parameters.AddWithValue("@UAdress", user.UAdress);
            cmd.Parameters.AddWithValue("@USalary", user.USalary);
            cmd.Parameters.AddWithValue("@Uphone", user.Uphone);
            cmd.Parameters.AddWithValue("@UID", UID);
            conn.Open();
            try
            {
                string i = cmd1.ExecuteScalar().ToString();
                if (i != "admin")
                {
                    return false;
                }
                cmd.ExecuteNonQuery();
            }
            catch//(DataException e)
            {
                return false;
            }
            conn.Close();
            return true;
        }
    }
    /// <summary>
    /// �û��޸����룬��Ҫ�ṩ�˻���ԭ�ȵ����롣
    /// </summary>
    /// <param name="Uaccount">�˻�</param>
    /// <param name="prePwd">������</param>
    /// <param name="newPwd">������</param>
    /// <returns>�޸ĳɹ�����true�����෵��false</returns>
    public bool ResetUser(string Uaccount, string prePwd, string newPwd)
    {
        string str = "update User set Upwd =@Upwd1 where Uaccount=@Uaccount and Upwd=@Upwd2";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Upwd1", MD5Helper.GetMD5(Uaccount + newPwd));
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            cmd.Parameters.AddWithValue("@Upwd2", MD5Helper.GetMD5(Uaccount + prePwd));
            conn.Open();
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            conn.Close();
            if (i == 0) return false;
            else return true;
        }
    }
    /// <summary>
    /// Ϊ����Ա�ṩ���������ã�����Ϊ123456��
    /// </summary>
    /// <param name="Uaccount"></param>
    /// <param name="thisUaccount"></param>
    /// <returns>0�������ʧ�ܣ�1������³ɹ���2������µ����Լ�</returns>
    public int ResetAdmin(string Uaccount, string thisUaccount)
    {
        string str = "update User set Upwd =@Upwd where Uaccount=@Uaccount";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Upwd", MD5Helper.GetMD5(Uaccount + "123456"));
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            conn.Open();
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch
            {
                return 0;
            }
            conn.Close();
            if (i == 0) return 0;
            else if (Uaccount == thisUaccount) return 2;
            else return 1;
        }
    }
    /// <summary>
    /// Ϊ����Ա�ṩ��ɾ���û�������ɾ���Լ���
    /// </summary>
    /// <param name="Uaccount"></param>
    /// <param name="thisUaccount"></param>
    /// <returns>0���ɾ��ʧ�ܣ�1���ɾ���ɹ���2���ɾ�������Լ�</returns>
    public int DeleteAdmin(string Uaccount, string thisUaccount)
    {
        string str = "delete from User where Uaccount =@Uaccount";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            conn.Open();
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch
            {
                return 0;
            }
            conn.Close();
            if (i == 0) return 0;
            else if (Uaccount == thisUaccount) return 2;
            else return 1;
        }
    }
    /// <summary>
    /// �����û�ͷ���·��
    /// </summary>
    /// <param name="Uaccount">�û���</param>
    /// <returns>string��·��</returns>
    public string GetImageUser(string Uaccount)
    {
        string str = "select Uicon from User where Uaccount =@Uaccount";
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            conn.Open();
            var i = cmd.ExecuteScalar();
            conn.Close();
            if (i != null && i.ToString() != "")
            {
                string rst = i.ToString();
                return rst;
            }
            else return null;
        }
    }
    /// <summary>
    /// ��ͷ��·���������ݿ�,ͬʱ����÷���ɾ��֮ǰ��ŵ�ͷ��
    /// </summary>
    /// <param name="ImageURL">ͼƬ·��</param>
    /// <param name="Uaccount">�˺�</param>
    /// <returns>1���³ɹ���0����ʧ��</returns>
    public int SetImageUser(string ImageURL,string Uaccount)
    {
        string preUrl = GetImageUser(Uaccount);
        string str = "update User set Uicon =@Uicon where Uaccount =@Uaccount";
        if(preUrl != null && preUrl != "")
        {
            preUrl = preUrl.Replace("~/", " ").Trim();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                File.Delete(Path.Combine(path, preUrl));
            }
            catch
            {
                
            }
        }
        using( MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@Uicon", ImageURL);
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
}