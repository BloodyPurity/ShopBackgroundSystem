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
/// 处理用户基础业务
/// </summary>
public class UserDAL
{
    string strCon = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
    public UserDAL()
    {
    }
    /// <summary>
    /// 返回一个用户类型的string，用于判断是否该让用户进入某些页面
    /// </summary>
    /// <param name="Uaccount">账户</param>
    /// <returns>用户类型:Utype</returns>
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
    /// 返回一个stringList类型的对象，用作数据源。剔除了UID、密码和用户类型信息。
    /// </summary>
    /// <param name="Uaccount">账号</param>
    /// <returns>一组用于让用户进行查改的信息。</returns>
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
    /// 用于用户详情页的更新，账户、用户类型当然是无法更改的。
    /// 用户更新密码后需要退出。
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="Uaccount">用户的账户</param>
    /// <returns>true如果密码已经变更。否则返回false</returns>
    public bool UpdateDetail(User user, string Uaccount)
    {
        string str = "select Upwd from User where Uaccount=@Uaccount";
        string str1 = "update User set UName =@UName,USex =@USex,UAdress=@UAdress,USalary =@USalary,Uphone=@Uphone,Ubirth=@Ubirth where Uaccount=@Uaccount";
        string str2 = "select Upwd from User where Uaccount=@Uaccount";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);//修改前的密码
            cmd.Parameters.AddWithValue("@Uaccount", Uaccount);
            MySqlCommand cmd1 = new MySqlCommand(str1, conn);//修改内容
            cmd1.Parameters.AddWithValue("@UName", user.UName);
            cmd1.Parameters.AddWithValue("@USex", user.USex);
            cmd1.Parameters.AddWithValue("@UAdress", user.UAdress);
            cmd1.Parameters.AddWithValue("@USalary", user.USalary);
            cmd1.Parameters.AddWithValue("@Uphone", user.Uphone);
            cmd1.Parameters.AddWithValue("@Ubirth", user.Ubirth.ToString("G"));
            cmd1.Parameters.AddWithValue("@Uaccount", Uaccount);
            MySqlCommand cmd2 = new MySqlCommand(str2, conn);//修改后的密码
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
    /// 根据Utype返回所有的用户信息数据集。
    /// </summary>
    /// <param name="Utype">//用户类型</param>
    /// <returns>一个数据集，可以用来当做数据源。</returns>
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
    /// 弃用的代码
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
    /// 用于管理员的单行更新，与用户详情页更新的区别在于它可以查看所有人的信息。
    /// </summary>
    /// <param name="user">需要修改的用户对象信息</param>
    /// <param name="UID">要更改的用户的UID，唯一标识</param>
    /// <param name="adminUaccount">管理员的账户信息，最好从Session中取得</param>
    /// <returns>false如果更新失败，或者是非管理员角色。true如果没有出现上述情况。</returns>
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
    /// 用户修改密码，需要提供账户和原先的密码。
    /// </summary>
    /// <param name="Uaccount">账户</param>
    /// <param name="prePwd">旧密码</param>
    /// <param name="newPwd">新密码</param>
    /// <returns>修改成功返回true，其余返回false</returns>
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
    /// 为管理员提供的密码重置，重置为123456。
    /// </summary>
    /// <param name="Uaccount"></param>
    /// <param name="thisUaccount"></param>
    /// <returns>0如果更新失败；1如果更新成功；2如果更新的是自己</returns>
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
    /// 为管理员提供的删除用户。可以删除自己。
    /// </summary>
    /// <param name="Uaccount"></param>
    /// <param name="thisUaccount"></param>
    /// <returns>0如果删除失败；1如果删除成功；2如果删除的是自己</returns>
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
    /// 返回用户头像的路径
    /// </summary>
    /// <param name="Uaccount">用户名</param>
    /// <returns>string，路径</returns>
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
    /// 将头像路径存入数据库,同时会调用方法删除之前存放的头像
    /// </summary>
    /// <param name="ImageURL">图片路径</param>
    /// <param name="Uaccount">账号</param>
    /// <returns>1更新成功，0更新失败</returns>
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