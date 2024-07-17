using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_defaultregister : System.Web.UI.Page
{
    RSAHelper rSAHelper = new RSAHelper();
    LoginDAL loginDAL = new LoginDAL();
    UserDAL userDAL = new UserDAL();
    User user = new User();
    string Uaccount;
    protected void Page_Load(object sender, EventArgs e)
    {
        BtnRegist.Attributes.Add("OnClick", "return userCheck();");
        if (Session["user"] == null || Session["user"].ToString() == "")
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
        else if (userDAL.Utype(Session["user"].ToString()) != "admin")
        {
            Response.Redirect("~/UI/login/login.aspx");
        }
        Bind();
    }
    protected void Bind()
    {
        Uaccount = Session["user"]?.ToString();
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
    }
    protected void BtnRegist_Click(object sender, EventArgs e)
    {
        string ua = tUaccount.Text;
        user.Uaccount = ua;
        user.Upwd = MD5Helper.GetMD5(user.Uaccount + "123456");
        if (ua == "" || user.Uaccount == "" || user.Upwd == "")
        {
            //alert
            return;
        }
        user.UName = tUName.Text;
        if (radSexMale.Checked)
        {
            user.USex = "男";
        }
        else if (radSexFemale.Checked)
        {
            user.USex = "女";
        }
        user.UAdress = tUAdress.Text;
        try
        {
            user.USalary = (float)Convert.ToDouble(tUsalary.Text);
        }
        catch
        {
            user.USalary = 0f;
        }
        try
        {
            user.Ubirth = Convert.ToDateTime(tUbirth.Text);
        }
        catch
        {
            user.Ubirth = DateTime.Today;
        }
        user.Uphone = tUphone.Text;
        if (loginDAL.RegistCheck(user.Uaccount, "user") == 1)
        {
            if (loginDAL.RegistUser(user) != 0)
            {
                //Label1.Text = "注册成功！";
                Response.Redirect("~/UI/infopage/userinfo.aspx");
            }
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/infopage/userinfo.aspx");
    }
}