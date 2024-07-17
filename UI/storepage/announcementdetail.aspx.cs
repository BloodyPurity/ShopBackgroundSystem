using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_storepage_announcementdetail : System.Web.UI.Page
{
    int id;
    string Uaccount;
    List<string> list = new List<string>();
    StoreDAL storeDAL= new StoreDAL();
    UserDAL userDAL= new UserDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void Bind()
    {
        string i = Request.QueryString["anid"];
        if (!string.IsNullOrEmpty(i))
        {
            id= Convert.ToInt32(i);
        }
        else
        {
            Response.Redirect("~/UI/login/login.aspx");
        }
        Uaccount = Session["user"]?.ToString();
        if (Uaccount != null && Uaccount != "")
        {
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        list = storeDAL.AnnouncementSingle(id);
        labname.Text = list[1];
        labtime.Text = list[3];
        labowner.Text = list[5];
        tbxdetail.Text = list[2];
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }

    protected void btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/storepage/announcement");
    }
}