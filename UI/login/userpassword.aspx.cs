using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_login_userpassword : System.Web.UI.Page
{
    RSAHelper rSAHelper = new RSAHelper();
    UserDAL userDAL = new UserDAL();
    User user = new User();
    string Uaccount = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Button1.Attributes.Add("OnClick", "return pwd();");
        Bind();
    }
    protected void Bind()
    {
        try
        {
            if (Session["user"] != null && Session["user"].ToString() != null)
            {
                Uaccount = Session["user"].ToString();
                TextBox0.Text = Uaccount;
            }
        }
        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Uaccount = TextBox0.Text;
        string prePwd = TextBox1.Text.Replace(" ", "+");
        string newPwd = TextBox2.Text.Replace(" ", "+");
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/login/login");
    }
}