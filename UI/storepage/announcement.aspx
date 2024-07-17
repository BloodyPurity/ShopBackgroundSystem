<%@ Page Language="C#" AutoEventWireup="true" CodeFile="announcement.aspx.cs" Inherits="UI_store_anouncement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet"/>
    <style>
        h3{
            color:darkgoldenrod;
        }
        .panel-footer{
            background-color:white;
            text-align:center;
        }
        .col-md-10 .panel-footer{
            text-align:right;
            color:red;
        }
        label{
            text-align:center;
        }
        .col-md-2{
        }
        .img-circle{
            width:60px;
            height:60px;
        }
        .dropdown-menu li{
            text-align:center;
        }
        textarea{
            resize:none;
        }
        th{
            text-align:center;
        }
        tr{
            text-align:center;
        }
        </style>
    <script>
        window.onload = function showtime() {
            var pretime = '<%= announcementtime%>';
            var time = new Date();
            thistime = time.toLocaleString();
            if (pretime != null && pretime!="") {
                var ptime = Date.parse(new Date(pretime));
                var ntime = Date.parse(new Date(thistime));
                var usedtime = ntime - ptime;
                var seconds = usedtime;
                if (seconds > 3600 * 1000 * 10) {
                    document.getElementById("newannouncement").style.display = "none";
                }
            }
            else {
                document.getElementById("newannouncement").style.display = "none";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel-footer"><h2>店内公告</h2></div>
        <div class="col-md-2">
            <ul class="nav nav-pills nav-stacked">
              <li role="presentation"  ><a href="../../UI/mainpage/main.aspx"><span class="glyphicon glyphicon-home"></span>主页</a></li>
              <li role="presentation" class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
                  <span class="glyphicon glyphicon-shopping-cart"></span>商品 <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                  <li role="presentation"  ><a href="../../UI/goodspage/goods">商品信息</a></li>
                  <li role="presentation"  ><a href="../../UI/goodspage/goodsoff">已下架商品</a></li>
                  <li role="presentation"  ><a href="../../UI/goodspage/provider">供货商</a></li>
                  <li role="presentation"  ><a href="../../UI/goodspage/goodstype">商品类型</a></li>
                </ul>
              </li>
              <li role="presentation" class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
                  <span class="glyphicon glyphicon-calendar"></span>店长操作 <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                  <li role="presentation"  ><a href="../../UI/infopage/userinfo">员工一览</a></li>
                  <li role="presentation"  ><a href="../../UI/login/defaultregister">新人入职</a></li>
                  <li role="presentation"  ><a href="../../UI/storepage/salary">发工资</a></li>
                  <li role="presentation"  ><a href="../../UI/goodspage/stockinadmin">入库管理</a></li>
                  <li role="presentation"  ><a href="../../UI/goodspage/stockoutadmin">出库管理</a></li>
                </ul>
              </li>
              <li role="presentation" class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
                  <span class="glyphicon glyphicon-briefcase"></span>员工操作 <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                  <li role="presentation"  ><a href="../../UI/goodspage/stockin">入库</a></li>
                  <li role="presentation"  ><a href="../../UI/goodspage/stockout">出库</a></li>
                </ul>
              </li>
              <li role="presentation" class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
                  <span class="glyphicon glyphicon-yen"></span>店铺 <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                  <li role="presentation"  ><a href="../../UI/storepage/announcement">店铺公告</a></li>
                  <li role="presentation"  ><a href="../../UI/storepage/statistics">统计</a></li>
                </ul>
              </li>
            </ul>
        </div>
        <div class="col-md-10">
            <div class="panel-footer">
                <nav class="navbar navbar-default">
                    <div class="container-fluid">
                        <div class="btn-group">
                          <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            操作 <span class="caret"></span>
                          </button>
                          <ul class="dropdown-menu">
                            <li><a href="../../UI/infopage/userinfodetail">个人资料</a></li>
                            <li><a href="../../UI/infopage/userpassword">修改密码</a></li>
                            <li role="separator" class="divider"></li>
                            <li><asp:Button ID="btnLogout" runat="server" CssClass="btn btn-default" Text="登出系统" Onclick="btnLogout_Click"/></li>
                          </ul>
                        </div>
                        <asp:Label ID="labelwelcome" CssClass="text-info" runat="server" Text=""></asp:Label>
                        <asp:Image ID="imgHead" class="img-circle" AlternateText="头像" runat="server"/>
                    </div>
                </nav>
            </div>
            <div id="newannouncement" class="alert alert-warning alert-dismissible" role="alert">
              <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              近期有<strong>新的公告</strong>，请注意查收。
            </div>
            <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3>公告</h3>
                </div>
                <asp:GridView ID="GridView1"
                    BorderStyle="None"
                    GridLines ="None"
                    class="table table-striped"
                    AutoGenerateColumns="False"
                    AllowSorting="true"
                    OnSorting="GridView1_Sorting"
                    AllowPaging="true"
                    PageSize="5"
                    OnPageIndexChanging ="GridView1_PageIndexChanging"
                    OnRowCommand="GridView1_RowCommand"
                    runat="server">
                    <Columns>
                        <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="name" SortExpression="name" HeaderText="标题" ReadOnly="True" />
                        <asp:BoundField DataField="time" SortExpression="time" HeaderText="发布时间" />
                        <asp:BoundField DataField="owner" SortExpression="owner" HeaderText="发布者" />
                        <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnlook" Text="查看" CommandName="havealook" class="btn btn-primary" runat="server" CommandArgument='<%#Eval("id")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="panel-footer">注意：无</div>
            </div>
            </div>
            <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3>新增公告</h3>
                </div>
                <div class="form-group">
                    <label>标题</label><asp:TextBox ID="tbxtitle" runat="server" CssClass="form-control"></asp:TextBox>
                    <label>内容</label><asp:TextBox ID="tbxdetail" runat="server" CssClass="form-control" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </div>
                <asp:Button ID="btnsubmit" Text="发布" runat="server" CssClass="btn btn-primary" OnClick="btnsubmit_Click" />
                <div class="panel-footer">注意：发布者为当前用户，时间为点按“发布”的时刻。</div>
            </div>
            </div>
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
