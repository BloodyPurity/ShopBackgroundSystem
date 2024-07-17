using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_stockindetail : System.Web.UI.Page
{
    StoreDAL storeDAL = new StoreDAL();
    UserDAL userDAL = new UserDAL();
    Goods goods = new Goods();
    GoodsDAL goodsDAL = new GoodsDAL();
    string Uaccount = null;
    string id;
    List<string> sid;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Uaccount = Session["user"]?.ToString();
            id = Request.QueryString["siid"];
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
        id = Request.QueryString["siid"];
        sid = storeDAL.SelectStockinlogSingle(Convert.ToInt32(id));
        tbxid.Text = sid[0];
        tbxgname.Text = sid[1];
        tbxprovider.Text = sid[3];
        tbxgcount.Text = sid[4];
        tbxprice.Text = sid[9];
        tbxnotes.Text = sid[5];
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        double count = Convert.ToDouble(tbxgcount.Text);
        double percost = Convert.ToDouble(tbxprice.Text);
        id = Request.QueryString["siid"];
        string notes = tbxnotes.Text;
        if (storeDAL.UpdateStockinlogPersonal(Convert.ToInt32(id), count, percost, notes))
        {
            Response.Write("<script>alert('修改成功');window.location.href='../goodspage/stockin.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('修改失败，请联系店长');window.location.href='../goodspage/stockin.aspx';</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/goodspage/stockin.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}