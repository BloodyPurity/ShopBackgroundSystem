using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_goodsoff : System.Web.UI.Page
{
    UserDAL userDAL = new UserDAL();
    Goods goods = new Goods();
    GoodsDAL goodsDAL = new GoodsDAL();
    string Uaccount = null;
    string Utype = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Uaccount = Session["user"]?.ToString();
        if (!IsPostBack)
        {
            Bind();
            if (Uaccount != null && Uaccount != "")
            {
                Utype = userDAL.Utype(Uaccount);
                if (Utype != "admin")
                {
                    Response.Redirect("~/UI/login/login.aspx");
                }
            }
            else
            {
                //没有登录重定向到登录页
                Response.Redirect("~/UI/login/login.aspx");
            }
        }
    }
    protected void Bind()
    {
        Uaccount = Session["user"]?.ToString();
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        GirdBind();
    }
    protected void GirdBind()
    {
        DataSet ds = goodsDAL.SelectGoodsoff();
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            int colnumcount = ds.Tables[0].Rows.Count;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = colnumcount;
            GridView1.Rows[0].Cells[0].Text = "没有相关记录";
            GridView1.Rows[0].Cells[0].Style.Add("color", "red");
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GirdBind();
    }
    protected void GridView1_GoodsDelete(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt16(e.CommandArgument.ToString());
        if (e.CommandName == "gdelete")
            if (!goodsDAL.ReUpGoods(id))
            {
                Response.Write("<script>alert('恢复失败')</script>");
            }
            else
            {
                Response.Write("<script>alert('恢复成功');window.location.href='goodsoff.aspx';</script>");
            }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}