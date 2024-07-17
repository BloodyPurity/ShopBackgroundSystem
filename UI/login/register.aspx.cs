using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_adminregister : System.Web.UI.Page
{
    RSAHelper rSAHelper = new RSAHelper(); 
    LoginDAL loginDAL = new LoginDAL();
    UserDAL userDAL = new UserDAL();
    User user = new User();
    bool hasImage =false;
    protected void Page_Load(object sender, EventArgs e)
    {
        tUbirth.Attributes["onclick"] = ClientScript.GetPostBackEventReference(btnCalendar, null);
        this.tUpwd.Attributes["value"] = tUpwd.Text;
        this.confirmUpwd.Attributes["value"] = confirmUpwd.Text;
    }
    protected void BtnRegist_Click(object sender, EventArgs e)
    {
        string ua = tUaccount.Text;
        string up = tUpwd.Text;
        string cp = confirmUpwd.Text;
        up = rSAHelper.Decrypt(up);
        cp = rSAHelper.Decrypt(cp);
        if (up != cp)
        {
            Label1.Text = "密码错误";
            return;
        }
        user.Uaccount = ua;
        user.Upwd = up;
        if (ua =="" || up == "" || cp == "" || user.Uaccount == "" || user.Upwd == "")
        {
            Label1.Text = "账号或密码不可为空";
            return;
        }
        user.Upwd = MD5Helper.GetMD5(user.Uaccount+user.Upwd);
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
                hasImage = true;
                //判断是否选中文件
                if (fulImage.FileName.Length <= 0)
                {
                    //Label1.Text = "未选中任何文件！";
                    hasImage = false;
                }
                //判断选中文件的长度大小是否大于4M（默认单位为B）
                if (fulImage.PostedFile.ContentLength > 4 * 1024 * 1024)
                {
                    Response.Write("<script>alert('文件过大!')</script>");
                    //Label1.Text = "文件长度过长！！";
                    hasImage = false;
                }
                //先存数据库，再保存
                if (hasImage)//保存
                {
                    //<上传> 功能代码
                    //------------------------------------------------------
                    //新建文件夹Uploads，设置保存文件名
                    string path = AppDomain.CurrentDomain.BaseDirectory + "uploads";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string filePath = DateTime.Now.ToString("yyyyMMddhhmmssms") + fulImage.FileName;
                    string dbPath = "~/uploads/" + filePath;
                    if (userDAL.SetImageUser(dbPath, user.Uaccount) == 1)
                    {
                        fulImage.SaveAs(Path.Combine(path, filePath));
                    }
                }
                Response.Write("<script>alert('成功注册')</script>");
                Response.Redirect("~/UI/infopage/userinfo.aspx");
            }
        }
        else
        {
            this.tUpwd.Attributes["value"] = "";
            this.confirmUpwd.Attributes["value"] = "";
            Response.Write("<script>alert('已经存在此账户')</script>");
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        this.tUpwd.Attributes["value"] = "";
        this.confirmUpwd.Attributes["value"] = "";
        Response.Redirect("~/UI/login/login.aspx");
    }
    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        Calendar1.Visible = true;
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Calendar1.Visible = false;
        tUbirth.Text = Calendar1.SelectedDate.ToString("d");
    }
}