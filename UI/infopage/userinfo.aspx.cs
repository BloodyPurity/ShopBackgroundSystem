using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_infopage_userinfo : System.Web.UI.Page
{
    User user = new User();
    UserDAL userDAL = new UserDAL();
    string Uaccount = null;
    string utype;
    protected void Page_Load(object sender, EventArgs e)
    {
        Uaccount = Session["user"]?.ToString();
        if (Session["User"] != null && Session["User"].ToString() != "")
        {
            utype = userDAL.Utype(Uaccount);
            if (utype == "admin")
            {
            }
            else
            {
                //不是管理员的用户重定向到用户详情页
                Response.Redirect("~/UI/infopage/userinfodetail");
            }
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "UID";
            ViewState["OrderDire"] = "ASC";

            Bind();
        }
    }

    protected void Bind()
    {
        if (Uaccount != null && Uaccount != "")
        {
            utype = userDAL.Utype(Uaccount);
            if (utype == "admin")
            {
            }
            else
            {
                //不是管理员的用户重定向到用户详情页
                Response.Redirect("~/UI/infopage/userinfodetail");
            }
        }
        else
        {
            //没有登录重定向到登录页
            Response.Redirect("~/UI/login/login.aspx");
        }
        Uaccount = Session["user"]?.ToString();
        labelwelcome.Text = "欢迎，" + Uaccount;
        imgHead.ImageUrl = userDAL.GetImageUser(Uaccount);
        string Utype = userDAL.Utype(Uaccount);
        DataSet ds = userDAL.AllUserinfo(Utype);
        DataView view = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        view.Sort = sort;
        GridView1.DataSource = view;
        GridView1.DataKeyNames = new string[] { "UID" };//主键
        GridView1.DataBind();
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
        Bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int UID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text);
        user.Uaccount = ((GridView1.Rows[e.RowIndex].Cells[1])).Text;
        user.Utype = (GridView1.Rows[e.RowIndex].Cells[2]).Text.ToString().Trim();
        user.UName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        user.USex = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
        user.UAdress = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
        try
        {
            user.USalary = (float)Convert.ToDouble(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim());
        }
        catch
        {
            user.USalary = 0f;
        }
        user.Uphone = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].Controls[0])).Text.ToString().Trim();
        if (!userDAL.UpdateRow(user, UID, Uaccount))
        {
            Session["user"] = null;
            Response.Redirect("~/UI/login/login.aspx");
        }
        GridView1.EditIndex = -1;
        Bind();
    }
    protected string ShowDetail(GridViewSelectEventArgs e)
    {
        string url = GridView1.Rows[e.NewSelectedIndex].Cells[1].Text;
        return url;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Redirect("~/UI/login/login.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Uaccount = e.CommandArgument.ToString();
        if (e.CommandName == "LB1")
        {
            switch (userDAL.ResetAdmin(Uaccount, Session["user"].ToString()))
            {
                case 0:
                    break;
                case 1:
                    Response.Write("<script>alert('重置成功')</script>");
                    Bind();
                    break;
                case 2:
                    Session["user"] = null;
                    Response.Redirect("~/UI/login/login.aspx");
                    break;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string Uaccount = GridView1.Rows[e.RowIndex].Cells[1].Text;
        //string thisUaccount = Session["user"].ToString();
        //e.Attributes.Add("onclick", "return confirm('确定要删吗?');"); 
        switch (userDAL.DeleteAdmin(Uaccount, Session["user"].ToString()))
        {
            case 0:
                Response.Write("<script>alert('" + Uaccount + "删除失败，存在与该员工相关联的数据');window.location.href='userinfo.aspx';</script>");
                Bind();
                break;
            case 1:
                Response.Redirect("~/UI/infopage/userinfo.aspx");
                Bind();
                break;
            case 2:
                Session["user"] = null;
                Response.Redirect("~/UI/login/login.aspx");
                break;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/infopage/userinfodetail");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/login/defaultregister.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标经过时，行背景色变 
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#39c5bb'");
            //鼠标移出时，行背景色变 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.Cells[12].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[1].Text + "\"吗?')");
            }
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = null;
        Response.Write("<script>window.location.href='../infopage/userinfo.aspx';</script>");
    }
}