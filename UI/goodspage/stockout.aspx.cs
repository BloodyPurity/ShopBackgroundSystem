using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_stockout : System.Web.UI.Page
{
    StoreDAL storeDAL = new StoreDAL();
    UserDAL userDAL = new UserDAL();
    Goods goods = new Goods();
    GoodsDAL goodsDAL = new GoodsDAL();
    string Uaccount = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSubmitGoods.Attributes.Add("onclick", "return subCheck();");
        Uaccount = Session["user"]?.ToString();
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
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        ddlGtypeBind();
        GridBind();
        ddlGoodsBind();
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
            DataSet ds = storeDAL.SelectStockoutlogPersonal(Uaccount);
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
    protected void ddlGtypeBind()
    {
        DataSet ds = goodsDAL.SelectGtypeddl();
        ddlGtype.DataSource = ds;
        ddlGtype.DataBind();
    }
    protected void ddlGoodsBind()
    {
        if (ddlGtype.SelectedValue != null)
        {
            DataSet ds = goodsDAL.SelectGoodsddl(ddlGtype.SelectedValue);
            ddlGoods.DataSource = ds;
            ddlGoods.DataBind();
        }
    }
    protected void ddlGtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridBind();
        ddlGoodsBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "gedit")
        {
            Response.Redirect("../../UI/goodspage/stockoutdetail" + "?soid=" + id);
        }
        else if (e.CommandName == "gdelete")
        {
            if (storeDAL.DeleteStockoutlogPersonal(id))
            {
                GridBind();
                Response.Redirect("~/UI/goodspage/stockout");
            }
            else
            {
                GridBind();
                Response.Write("<script>alert('店长已经确认！');window.location.href('stockout');</script>");
            }
        }
    }
    protected void btnSubmitGoods_Click(object sender, EventArgs e)
    {
        Uaccount = Session["user"]?.ToString();
        goods.Gname = ddlGoods.SelectedValue;
        List<string> str = goodsDAL.SelectGoodsSingle(goods.Gname);

        goods.notes = tbxNotes.Text;
        //如果数量超过最大数量，则会只出售最大数量
        goods.Gcount = Convert.ToDouble(tbxCount.Text);
        double maxcount = Convert.ToDouble(str[3]);
        goods.Gcount = goods.Gcount > maxcount ? maxcount : goods.Gcount;
        //如果不填，则按正常售价处理
        if (!string.IsNullOrEmpty(tbxPrice.Text))
        {
            goods.Price = Convert.ToDouble(tbxPrice.Text);
        }
        else
        {
            goods.Price = Convert.ToDouble(str[4]);
        }
        goods.discount= Convert.ToDouble(str[5]);
        if (storeDAL.Stockoutlog(goods, Uaccount))
        {
            Response.Redirect("~/UI/goodspage/stockout");
        }
        else
        {
            Response.Write("<script>alert('出了一些预料之外的错误...');window.location.href('stockout');</script>");
        }
    }

    
}