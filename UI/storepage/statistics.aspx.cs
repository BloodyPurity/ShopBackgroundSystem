using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_store_statistics : System.Web.UI.Page
{
    StoreDAL storeDAL = new StoreDAL();
    UserDAL userDAL = new UserDAL();
    string Uaccount = null;
    string Utype = null;
    List<string> str = null;
    public Dictionary<string, double> Price_Gtype = new Dictionary<string, double>();
    public Dictionary<string, double> Cost_Gtype = new Dictionary<string, double>();
    public Dictionary<string, double> Count_Gtype = new Dictionary<string, double>();
    public Dictionary<string, double> Count_GtypeIn = new Dictionary<string, double>();
    public string space = null;
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
            if(Utype != "admin")
            {
                Response.Redirect("~/UI/login/login.aspx");
            }
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        str = storeDAL.Asset();
        if (str != null)
        {
            tbxmoney.Text = str[1];
            double i = Convert.ToDouble(str[1]);
            tbxincome.Text = storeDAL.AssetRecordsSingle();
            tbxnow.Text = i > 0 ? "盈利" : "亏损";
        }
        ChartBind();
    }
    protected void ChartBind()
    {
        DataSet ds1 = storeDAL.SelectStockoutPricebyGtype();
        for(int i = 0; i < ds1.Tables[0].Rows.Count; i++)
        {
            Price_Gtype.Add(ds1.Tables[0].Rows[i][0].ToString(),Convert.ToDouble(ds1.Tables[0].Rows[i][1]));
        }
        DataSet ds2 = storeDAL.SelectStockoutCountbyGtype();
        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
        {
            Count_Gtype.Add(ds2.Tables[0].Rows[i][0].ToString(), Convert.ToDouble(ds2.Tables[0].Rows[i][1]));
        }
        DataSet ds3 = storeDAL.SelectStockinPricebyGtype();
        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
        {
            Cost_Gtype.Add(ds3.Tables[0].Rows[i][0].ToString(), Convert.ToDouble(ds3.Tables[0].Rows[i][1]));
        }
        DataSet ds4 = storeDAL.SelectStockinCountbyGtype();
        for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
        {
            Count_GtypeIn.Add(ds4.Tables[0].Rows[i][0].ToString(), Convert.ToDouble(ds4.Tables[0].Rows[i][1]));
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Bind();
    }
}