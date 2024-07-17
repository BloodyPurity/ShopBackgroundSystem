using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_stockinadmin : System.Web.UI.Page
{
    StoreDAL storeDAL = new StoreDAL();
    UserDAL userDAL = new UserDAL();
    Goods goods = new Goods();
    GoodsDAL goodsDAL = new GoodsDAL();
    string Uaccount = null;
    string Utype = null;
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
        if (Uaccount != null && Uaccount != "")
        {
            Utype = userDAL.Utype(Uaccount);
            if (Utype != "admin")
                Response.Redirect("~/UI/login/login.aspx");
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        GridBind();
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
    protected void GridBind()
    {
        Uaccount = Session["user"]?.ToString();
        if (!string.IsNullOrEmpty(Uaccount))
        {
            DataSet ds = storeDAL.SelectStockinlog();
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
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        List<string> str= storeDAL.SelectStockinlogSingle(id);
        if (e.CommandName == "gconfirm" && str.Count!=0)
        {
            if(str[8] == "0")
            {
                if (storeDAL.CheckStockin(id, Convert.ToInt32(str[4]), str[1], Convert.ToInt32(str[7])))
                {
                    Response.Redirect("~/UI/goodspage/stockinadmin");
                }
            }
            else
            {
                GridBind();
                Response.Write("<script>alert('店长已经确认，无法再次确认');window.location.href('stockinadmin');</script>");
            }
        }
        else if (e.CommandName == "gdelete")
        {
            if (storeDAL.DeleteStockinlogPersonal(id))
            {
                GridBind();
                Response.Redirect("~/UI/goodspage/stockin");
            }
            else
            {
                GridBind();
                Response.Write("<script>alert('店长已经确认，无法删除');window.location.href('stockinadmin');</script>");
            }
        }
    }
}