using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

public partial class UI_infopage_userinfodetail : System.Web.UI.Page
{
    UserDAL userDAL = new UserDAL();
    User user = new User();
    bool hasImage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    public void Bind()
    {
        if (Session["User"] != null && Session["User"].ToString()!="")
        {
            string Uaccount = Session["User"].ToString();
            string i = userDAL.GetImageUser(Uaccount);
            if(i != null)
            {
                imgHead.ImageUrl = i;
            }
            else
            {
                imgHead.ImageUrl = "~/uploads/f_visual.jpg";
            }
            labelwelcome.Text = "欢迎，" + Uaccount;
            List<string> Userinfo = userDAL.Userinfo(Uaccount);
            user.Uaccount = Userinfo[0];
            user.UName = Userinfo[1];
            user.USex = Userinfo[2];
            user.UAdress = Userinfo[3];
            if (Userinfo[4] != "")
                user.USalary = (float)Convert.ToDouble(Userinfo[4]);
            if (Userinfo[5] != "")
                user.Ubirth = Convert.ToDateTime(Userinfo[5]);
            user.Uphone = Userinfo[6];
            //foreach (string key in Userinfo)
            //{
            //    Response.Write(key);
            //}
            tUaccount.Text = user.Uaccount;
            tUName.Text = user.UName;
            if (user.USex == "男")
            {
                radSexMale.Checked = true;
            }
            else if (user.USex == "女")
            {
                radSexFemale.Checked = true;
            }
            tUAdress.Text = user.UAdress;
            tUsalary.Text = user.USalary.ToString();
            tUbirth.Text = user.Ubirth.ToString();
            tUphone.Text = user.Uphone;
        }
        else Response.Redirect("~/UI/login/login");

    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Bind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        user.Uaccount = Session["user"].ToString();//不能改账号
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
            //string path = "uploads/" +  + fulImage.FileName;
            string dbPath = "~/uploads/" + filePath;
            if (userDAL.SetImageUser(dbPath, user.Uaccount) == 1)
            {
                fulImage.SaveAs(Path.Combine(path, filePath));
            }
        }
        bool isPwdChanged = userDAL.UpdateDetail(user, user.Uaccount);
        if (isPwdChanged)
        {
            Session["user"] = null;
        }
        Bind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //获取页面的用户、密码
        string ua = Session["user"].ToString();
        Response.Redirect("~/UI/infopage/userpassword.aspx" + "?userCode=" + ua);
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}