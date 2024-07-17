using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_infopage_userinfoadmin : System.Web.UI.Page
{
    User user = new User();
    UserDAL userDAL = new UserDAL();
    string Uaccount = null;
    string u = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null && Session["user"].ToString() != "")
        {
            string Utype = userDAL.Utype(Session["user"].ToString());
            if (Utype == "admin")
            {
                Bind();
            }
            else
            {
                //不是管理员的用户重定向到用户详情页
                Response.Redirect("~/UI/infopage/userinfodetail");
            }
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
    }
    protected void Bind()
    {
        try
        {
            Uaccount = Request.QueryString["ua"];
        }
        catch
        {
        }
        if (Uaccount != null)
        {
            u = Session["user"]?.ToString();
            labelwelcome.Text = "欢迎，" + u;
            imgHead.ImageUrl = userDAL.GetImageUser(u);
            List<string> Userinfo = userDAL.Userinfo(Uaccount);
            if (Userinfo.Count > 0)
            {
                user.Utype = userDAL.Utype(Uaccount);
                user.Uaccount = Userinfo[0];
                user.UName = Userinfo[1];
                user.USex = Userinfo[2];
                user.UAdress = Userinfo[3];
                if (Userinfo[4] != "")
                    user.USalary = (float)Convert.ToDouble(Userinfo[4]);
                if (Userinfo[5] != "")
                    user.Ubirth = Convert.ToDateTime(Userinfo[5]);
                user.Uphone = Userinfo[6];
            }
            tUaccount.Text = user.Uaccount;
            tUtype.Text = user.Utype;
            tUName.Text = user.UName;
            tUSex.Text = user.USex;
            tUAdress.Text = user.UAdress;
            tUsalary.Text = user.USalary.ToString();
            tUbirth.Text = user.Ubirth.ToString();
            tUphone.Text = user.Uphone;
        }
        else
            Response.Redirect("~/UI/login/login");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/infopage/userinfo");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}