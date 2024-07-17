using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_goodsedit : System.Web.UI.Page
{
    string Uaccount = null;
    string gname = null;
    UserDAL userDAL = new UserDAL();
    GoodsDAL goodsDAL = new GoodsDAL();
    Goods goods = new Goods();
    bool hasImage;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Uaccount = Session["user"]?.ToString();
            gname = Request.QueryString["gname"];
        }
        catch { }
        if(!string.IsNullOrEmpty(Uaccount) && !string.IsNullOrEmpty(gname))
        {
            if (userDAL.Utype(Uaccount) != "admin")
            {
                Response.Write("<script>alert('无访问权限');window.location.href='goods.aspx';</script>");
            }
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
        gname = Request.QueryString["gname"];
        List<string> goods1 = goodsDAL.SelectGoodsSingle(gname);
        tbxid.Text = goods1[0];
        tbxgname.Text = goods1[1];
        tbxgtype.Text = goods1[2];
        tbxgcount.Text = goods1[3];
        tbxprice.Text = goods1[4];
        tbxdiscount.Text = goods1[5];
        tbxnotes.Text = goods1[6];
        tbxgicon.ImageUrl= goods1[7];
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        goods.Gname = tbxgname.Text;
        try
        {
            goods.Gcount = Convert.ToDouble(tbxgcount.Text);
        }
        catch
        {
            goods.Gcount = 0;
        }
        try
        {
            goods.Price = Convert.ToDouble(tbxprice.Text);
        }
        catch
        {
            goods.Price = 0;
        }
        try
        {
            goods.discount = Convert.ToDouble(tbxdiscount.Text);
        }
        catch
        {
            goods.discount = 1;
        }
        goods.notes = tbxnotes.Text;
        if (goodsDAL.UpdateGoods(goods))
        {
            hasImage = true;
            //判断是否选中文件
            if (fulImage.FileName.Length <= 0)
            {
                hasImage = false;
            }
            //判断选中文件的长度大小是否大于4M（默认单位为B）
            if (fulImage.PostedFile.ContentLength > 4 * 1024 * 1024)
            {
                Response.Write("<script>alert('文件过大!')</script>");
                hasImage = false;
            }
            //先存数据库，再保存
            if (hasImage)//保存
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "goodsicon";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string filePath = DateTime.Now.ToString("yyyyMMddhhmmssms") + fulImage.FileName;
                string dbPath = "~/goodsicon/" + filePath;
                if (goodsDAL.SetImageGoods(dbPath, goods.Gname) == 1)
                {
                    fulImage.SaveAs(Path.Combine(path, filePath));
                }
            }
            Response.Write("<script>alert('操作成功');window.location.href='goods.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('操作失败');</script>");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/goodspage/goods");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}