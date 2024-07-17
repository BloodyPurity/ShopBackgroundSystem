using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_store_anouncement : System.Web.UI.Page
{
    StoreDAL storeDAL = new StoreDAL();
    UserDAL userDAL = new UserDAL();
    string Uaccount = null;
    public string announcementtime;
    Announcement announcement = new Announcement();
    protected void Page_Load(object sender, EventArgs e)
    {
        announcementtime = storeDAL.LateTime();
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "id";
            ViewState["OrderDire"] = "Desc";

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
        GridBind();
    }
    protected void GridBind()
    {
        DataSet ds = storeDAL.Announcement(); ;
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
            string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
            DataView view = ds.Tables[0].DefaultView;
            view.Sort = sort;
            GridView1.DataSource = view;
            GridView1.DataBind();
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].ToString() == sPage)
        {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                ViewState["OrderDire"] = "Desc";
        }
        else
        {
            ViewState["SortOrder"] = e.SortExpression;
        }
        GridBind();
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Uaccount = Session["user"]?.ToString();
        if (!string.IsNullOrEmpty(tbxtitle.Text) && !string.IsNullOrEmpty(tbxdetail.Text) && !string.IsNullOrEmpty(Uaccount))
        {
            announcement.name = tbxtitle.Text;
            announcement.detail = tbxdetail.Text;
            announcement.createtime = DateTime.Now;
            announcement.owner = Uaccount;
            if (storeDAL.AddAnnouncement(announcement))
            {
                Response.Write("<script>alert('操作成功');window.location.href='announcement.aspx';</script>");
                Bind();
            }
            else
            {
                
            }
        }
        else
        {
            Response.Write("<script>alert('公告名及内容不可为空');</script>");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "havealook")
        {
            Response.Redirect("../../UI/storepage/announcementdetail" + "?anid=" + id);
        }
    }
}