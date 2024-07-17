using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_goodstype : System.Web.UI.Page
{
    UserDAL userDAL = new UserDAL();
    GoodsDAL goodsDAL = new GoodsDAL();
    Goods goods = new Goods();
    string Uaccount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void Bind()
    {
        Uaccount = Session["user"]?.ToString();
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        DataSet ds= goodsDAL.SelectGtype();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
       bool a = goodsDAL.AddProvider("我", "水果", " ", "admin");
       bool b = goodsDAL.AddProvider("我", "水果", " ", "admin");
       bool c = goodsDAL.AddProvider("我", "坚果", " ", "admin");
        Response.Write(a+" "+b+" "+c);
    }
    protected void Unnamed_Click1(object sender, EventArgs e)
    {
        if(TextBox1.Text.Length > 0)
        {
            string gtype=TextBox1.Text;
            goodsDAL.AddGtype(gtype);
            Bind();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}