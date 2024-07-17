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
/// 处理登录相关事件
/// </summary>
public class LoginDAL
{
    string strCon = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
    public LoginDAL()
    {

    }
    /// <summary>
    /// 都正确才返回一个用户名。这个值可以用Session保存
    /// </summary>
    /// <param name="Uaccount">账户</param>
    /// <param name="Upwd">密码</param>
    /// <returns>用户的账号，用于跳转。</returns>
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
    /// 返回值为1即可通过注册检查。只允许存在一个管理员
    /// </summary>
    /// <param name="Uaccount">账户</param>
    /// <param name="Utype">类型</param>
    /// <returns>int，未通过检查返回0</returns>
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
            if (T == "admin")//管理员角色已经存在
            {
                conn.Close();
                return 0;
            }
            else
            {
                //账户已存在
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
    /// 管理员注册功能
    /// </summary>
    /// <param name="user"></param>
    /// <returns>int,不等于0则执行成功</returns>
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
    /// 普通用户注册功能
    /// </summary>
    /// <param name="user"></param>
    /// <returns>不等于0则执行成功</returns>
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