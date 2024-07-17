<%@ Page Language="C#" AutoEventWireup="true" CodeFile="provider.aspx.cs" Inherits="UI_goodspage_provider" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="panel-footer"><h2>供货商一览</h2></div>
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
            <div class="panel panel-default" title="商品展示">
                <div class="panel-heading">
                    <h3 class=".text-info">供货商</h3>
                </div>
                <asp:GridView ID="GridView1" runat="server"
                    BorderStyle="None"
                    GridLines ="None"
                    class="table table-striped"
                    style="text-align:center"
                    AllowPaging="true"
                    PageSize="5"
                    OnPageIndexChanging ="GridView1_PageIndexChanging"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" ReadOnly="True" ItemStyle-Width="10"/>
                        <asp:BoundField DataField="name" SortExpression="name" HeaderText="供货商" ReadOnly="True" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="goodstype" SortExpression="goodstype" HeaderText="商品类型" ReadOnly="True" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="description" SortExpression="description" HeaderText="简介" />
                        <asp:BoundField DataField="holder" SortExpression="holder" HeaderText="负责人" />
                    </Columns>
                </asp:GridView>
                <div class="panel-footer">注意：同一供货商可以提供不同货物，这可能需要不同的负责人</div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class=".text-info">添加供货商</h3>
                </div>
                <div class=" form-group">
                    <div class ="form-inline">
                    供货商名：<asp:TextBox ID="tbxPname" CssClass="form-control" runat="server"></asp:TextBox>
                    供货类型：<asp:DropDownList ID="ddlGtype" runat="server" CssClass="form-control" DataValueField="name" DataTextField="name"></asp:DropDownList>
                    </div>
                    相关信息：<asp:TextBox ID="tbxPdescription" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Button ID="btnProvider" runat="server" CssClass="btn btn-primary" Text="确认" onclick="btnProvider_Click"/>
                </div>
                <div class="panel-footer">注意：同一供货商可以提供不同货物，这可能需要不同的负责人</div>
            </div>
                </div>
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
