using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_stockoutdetail : System.Web.UI.Page
{
    StoreDAL storeDAL = new StoreDAL();
    UserDAL userDAL = new UserDAL();
    GoodsDAL goodsDAL = new GoodsDAL();
    string Uaccount = null;
    string id;
    List<string> sid;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Uaccount = Session["user"]?.ToString();
            id = Request.QueryString["soid"];
        }
        catch { }
        if (!string.IsNullOrEmpty(Uaccount) && !string.IsNullOrEmpty(id))
        {

        }
        else { Response.Redirect("../../UI/login/login"); }
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
        id = Request.QueryString["soid"];
        sid = storeDAL.SelectStockoutlogSingle(Convert.ToInt32(id));
        tbxid.Text = sid[0];
        tbxgname.Text = sid[1];
        tbxgcount.Text = sid[3];
        tbxprice.Text = sid[8];
        tbxnotes.Text = sid[4];
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        List<string> str = goodsDAL.SelectGoodsSingle(tbxgname.Text);

        id = Request.QueryString["soid"];

        string notes = tbxnotes.Text;

        //如果数量超过最大数量，则会只出售最大数量
        double count = Convert.ToDouble(tbxgcount.Text);
        double maxcount = Convert.ToDouble(str[3]);
        count = count > maxcount ? maxcount : count;

        double discount = Convert.ToDouble(str[5]);
        //如果不填，则按正常售价处理
        double perprice = 0;
        bool isspecial;
        if (!string.IsNullOrEmpty(tbxprice.Text) && Convert.ToDouble(tbxprice.Text)!=0)
        {
            isspecial= true;
            perprice = Convert.ToDouble(tbxprice.Text);
        }
        else
        {
            isspecial= false;
            perprice = Convert.ToDouble(str[4]);
        }
        if (storeDAL.UpdateStockoutlogPersonal(Convert.ToInt32(id), count,discount, perprice, notes, isspecial))
        {
            Response.Write("<script>alert('修改成功');window.location.href='../goodspage/stockout.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('修改失败，请联系店长');window.location.href='../goodspage/stockout.aspx';</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/goodspage/stockout.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}