using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class UI_infopage_userpassword : System.Web.UI.Page
{
    RSAHelper rSAHelper = new RSAHelper();
    UserDAL userDAL = new UserDAL();
    User user = new User();
    string Uaccount = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();
    }
    protected void Bind()
    {
        try
        {
            if (Session["user"] != null && Session["user"].ToString() != null)
            {
                Uaccount = Session["user"].ToString();
            }
            if (Uaccount == null || Uaccount == "")
            {
                Uaccount = Request.QueryString["ua"];
            }
        }
        catch
        {
        }
        if (Uaccount != null && Uaccount != "")
        {
            labelwelcome.Text = "欢迎，" + Uaccount;
            imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        }
        else
            Response.Redirect("~/UI/login/login");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string prePwd = TextBox2.Text;
        string newPwd = TextBox3.Text;
        prePwd = rSAHelper.Decrypt(prePwd);
        newPwd = rSAHelper.Decrypt(newPwd);
        if (userDAL.ResetUser(Uaccount, prePwd, newPwd))
        {
            Response.Write("<script>alert(' 修改成功 ')</script>");
            Session["user"] = null;
            Response.Redirect("~/UI/login/login");
        }
        else
        {
            //Label3.Text = "修改失败！";
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/login/login");
    }
}