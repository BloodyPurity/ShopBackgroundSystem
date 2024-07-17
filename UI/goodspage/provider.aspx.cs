using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_provider : System.Web.UI.Page
{
    UserDAL userDAL = new UserDAL();
    GoodsDAL goodsDAL = new GoodsDAL();
    string Uaccount = null;
    protected void Page_Load(object sender, EventArgs e)
    {   if (!IsPostBack)
            {
                Bind();
            }
        Uaccount = Session["user"]?.ToString();
        if (Uaccount != null && Uaccount != "")
        {
            string Utype = userDAL.Utype(Uaccount);
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
    }
    protected void Bind()
    {
        Uaccount = Session["user"]?.ToString();
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        DataSet ds = goodsDAL.SelectProvider();
        GridView1.DataSource = ds;
        GridView1.DataBind();
        DataSet dsddl=goodsDAL.SelectGtypeddl();
        ddlGtype.DataSource = dsddl;
        ddlGtype.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void btnProvider_Click(object sender, EventArgs e)
    {
        string pname = tbxPname.Text;
        string pdescription = tbxPdescription.Text;
        string gtype = ddlGtype.SelectedItem.Text;
        Response.Write(gtype);
        string holder = Uaccount;
        if(pname != null && gtype != null && holder != null)
        {
            if(goodsDAL.AddProvider(pname,gtype,pdescription,holder))
            {
                Response.Write("<script>alert('添加成功');window.location.href='provider.aspx';</script>");
                Bind();
            }
        }
        else
        {
            Response.Write("信息不完全");
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}