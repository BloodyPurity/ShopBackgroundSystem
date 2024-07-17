using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_goodspage_goods : System.Web.UI.Page
{
    UserDAL userDAL = new UserDAL();
    Goods goods = new Goods(); 
    GoodsDAL goodsDAL = new GoodsDAL();
    string Uaccount = null;
    string Utype = null;
    bool hasImage;
    protected void Page_Load(object sender, EventArgs e)
    {
        Uaccount = Session["user"]?.ToString();
        if (!IsPostBack)
        {
            btnSubmitGoods.Attributes.Add("OnClick", "return digitcheck();");
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
        GirdBind();
        DiscountBind();
    }
    public void GirdBind()
    {
        DataSet ds = null;
        string gname = tbxSearch.Text;
        if (!string.IsNullOrEmpty(gname))
        {
            ds = goodsDAL.SelectGoods(gname);
        }
        else ds = goodsDAL.SelectGoods();
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
    protected void DiscountBind()
    {
        DataSet ds =goodsDAL.SelectGtypeddl();
        ddlGtype.DataSource = ds;
        ddlGtype.DataBind();
        ListItem listItem = new ListItem();
        listItem.Text = "不选";
        listItem.Value = "";
        ddlGtype.Items.Add(listItem);
        ddlGtype.SelectedIndex = ddlGtype.Items.Count - 1;//默认为不选
        ddlGtype1.DataSource = ds;
        ddlGtype1.DataBind();
    }
    //商品分页
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GirdBind();
    }
    //折扣
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Utype = userDAL.Utype(Uaccount);
        if (Utype == "admin")
        {
            if (!string.IsNullOrEmpty(tbxGname.Text) && ddlGtype.SelectedValue == "")
            {

                string Gname = tbxGname.Text;
                string Gtype = ddlGtype.SelectedValue;
                double Discount = Convert.ToDouble(ddlDiscount.SelectedValue);
                if (goodsDAL.Discount(Gname, Gtype, Discount))
                {
                    Response.Write("<script>alert('操作成功');window.location.href='goods.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('可能不存在此商品');window.location.href='goods.aspx';</script>");
                }
            }
            else if(string.IsNullOrEmpty(tbxGname.Text) && ddlGtype.SelectedValue != "")
            {
                string Gname = tbxGname.Text;
                string Gtype = ddlGtype.SelectedValue;
                double Discount = Convert.ToDouble(ddlDiscount.SelectedValue);
                if (goodsDAL.Discount(Gname, Gtype, Discount))
                {
                    Response.Write("<script>alert('操作成功');window.location.href='goods.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('可能不存在此商品');window.location.href='goods.aspx';</script>");
                }
            }
            else
            {
                Console.WriteLine("123");
                Response.Write("<script>alert('输入出错');window.location.href='goods.aspx';</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('无权限');window.location.href='goods.aspx';</script>");
        }
    }
    //添加商品
    protected void btnSubmitGoods_Click(object sender, EventArgs e)
    {
        Utype = userDAL.Utype(Uaccount);
        if (Utype == "admin")
        {
            if (!string.IsNullOrEmpty(tbxGname1.Text) && !string.IsNullOrEmpty(tbxCount.Text) && !string.IsNullOrEmpty(tbxPrice.Text))
            {
                goods.Gname = tbxGname1.Text;
                goods.Gtype = ddlGtype1.SelectedValue;
                goods.Gcount = Convert.ToDouble(tbxCount.Text);
                goods.Price = Convert.ToDouble(tbxPrice.Text);
                goods.notes = tbxNotes.Text;
                goods.discount = 1;
                if (goodsDAL.AddGoods(goods))
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
                    Response.Write("<script>alert('操作失败');window.location.href='goods.aspx';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('你想干啥？');window.location.href='goods.aspx';</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('无权限');window.location.href='goods.aspx';</script>");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string gname = e.CommandArgument.ToString();
        Utype = userDAL.Utype(Uaccount);
        if (Utype == "admin")
        {
            if (e.CommandName == "gdelete")
            {
                if (!goodsDAL.RemoveGoods(gname))
                {
                    Response.Write("<script>alert('下架出错');window.location.href='goods.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('下架成功');window.location.href='goods.aspx';</script>");
                }
            }
            else if (e.CommandName == "gedit")
            {
                Response.Redirect("../../UI/goodspage/goodsedit" + "?gname=" + gname);
            }
        }
        else
        {
            Response.Write("<script>alert('无下架或编辑权限')</script>");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GirdBind();
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}