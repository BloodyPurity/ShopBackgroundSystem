using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_login : System.Web.UI.Page
{
    RSAHelper rSAHelper=new RSAHelper();
    LoginDAL loginDAL = new LoginDAL();
    UserDAL userDAL = new UserDAL();
    User user = new User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button1.Attributes.Add("OnClick", "return uaccountCheck();");
            Button2.Attributes.Add("OnClick", "return uaccountCheck();");
            LiteralMsg.Text = "欢迎使用";
        }
        if (Session["user"] != null)
        {
            string t = userDAL.Utype(Session["user"].ToString());
            if (t == "admin")
            {
                Response.Redirect("~/UI/infopage/userinfo");
            }
            else if (t == "user")
            {
                Response.Redirect("~/UI/infopage/userinfodetail");
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ua = TextBox1.Text.ToString();
        string up = TextBox2.Text.ToString();
        up=rSAHelper.Decrypt(up);
        if (ua != "" || up != "")
        {
            string Uaccount = loginDAL.Login(ua, MD5Helper.GetMD5(ua+up));
            Session["User"] = Uaccount;
            string Utype = null;
            if (Uaccount != null && Uaccount != "")
            {
                Utype = userDAL.Utype(Uaccount);
                LiteralMsg.Text = "";
                if (Utype == "user")
                {
                    Response.Redirect("~/UI/infopage/userinfodetail.aspx");
                }
                if (Utype == "admin")
                {
                    Response.Redirect("~/UI/infopage/userinfo.aspx");
                }
            }
            LiteralMsg.Text = "用户名或密码错误";
        }
        else
        {
            LiteralMsg.Text = "输点儿什么吧";
        }
        //Button1.Text = Uaccount;

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string ua = TextBox1.Text.ToString();
        string up = TextBox2.Text.ToString();
        up=rSAHelper.Decrypt(up);
        if (ua != null && ua != "" && up != null && up != "")
        {
            if (loginDAL.RegistCheck(ua, "admin") != 0)
            {
                user.Uaccount = ua;
                user.Upwd = MD5Helper.GetMD5(ua+up);
                user.Ubirth = DateTime.Now;
                user.USalary = 0f;
                loginDAL.RegistAdmin(user);
                LiteralMsg.Text = "成功注册，其他信息到详情页修改";
            }
            else
            {
                LiteralMsg.Text = "注册失败";
            }
        }
        else
        {
            LiteralMsg.Text = "输点儿什么吧";
        }
    }

    protected void btnRegist_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/login/register");
    }
}