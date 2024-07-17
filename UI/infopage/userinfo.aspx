<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userinfo.aspx.cs" Inherits="UI_infopage_userinfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户列表</title>
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
            th{
                text-align:center;
            }
            tr{
                text-align:center;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="panel-footer"><h2>员工一览</h2></div>
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
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3>人员信息</h3>
                    </div>
                    <asp:GridView ID="GridView1"
                        BorderStyle="None"
                        GridLines ="None"
                        class="table table-striped"
                        AllowSorting="True"
                        OnSorting="GridView1_Sorting"
                        AutoGenerateColumns="False"
                        OnRowEditing="GridView1_RowEditing"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowUpdating="GridView1_RowUpdating"
                        OnRowDeleting="GridView1_RowDeleting"
                        OnRowCommand="GridView1_RowCommand"
                        AllowPaging="true"
                        PageSize="5"
                        OnPageIndexChanging ="GridView1_PageIndexChanging"
                        OnRowDataBound="GridView1_RowDataBound"
                        runat="server">
                        <Columns>
                            <asp:BoundField DataField="UID" SortExpression="UID" HeaderText="用户ID" ReadOnly="True" HeaderStyle-CssClass="center" />
                            <asp:BoundField DataField="Uaccount" SortExpression="Uaccount" HeaderText="账号" ReadOnly="True" HeaderStyle-CssClass="center"/>
                            <asp:BoundField DataField="Utype" SortExpression="Utype" HeaderText="账户类型" ReadOnly="True" HeaderStyle-CssClass="center"/>
                            <asp:BoundField DataField="UName" SortExpression="UName" HeaderText="用户名" HeaderStyle-CssClass="center"/>
                            <asp:BoundField DataField="USex" SortExpression="USex" HeaderText="性别" HeaderStyle-CssClass="center"/>
                            <asp:BoundField DataField="UAdress" SortExpression="UAdress" HeaderText="用户住址" ItemStyle-Width="200" HeaderStyle-CssClass="center"/>
                            <asp:BoundField DataField="USalary" SortExpression="USalary" HeaderText="工资" ItemStyle-Width="100" HeaderStyle-CssClass="center"/>
                            <asp:BoundField DataField="Ubirth" SortExpression="Ubirth" HeaderText="生日"  ReadOnly="True"  ItemStyle-Width="120" HeaderStyle-CssClass="center"/>
                            <asp:BoundField DataField="Uphone" SortExpression="Uphone" HeaderText="电话号" ItemStyle-Width="150" HeaderStyle-CssClass="center"/>
                            <asp:HyperLinkField  HeaderText="详情" DataNavigateUrlFields = "Uaccount" DataNavigateUrlFormatString = "/UI/infopage/userinfoadmin.aspx?ua={0}"  Text="查看" HeaderStyle-CssClass="center" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Text="重置密码" CommandArgument='<%#Eval("Uaccount")%>' CommandName="LB1"> </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
                            <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <div class="panel-footer">
                        <asp:Button ID="BtnNew" runat="server" Text="新建用户" class="btn btn-success" OnClick="BtnNew_Click"/>
                        <asp:Button ID="Button1" runat="server" Text="查看本人信息" class="btn btn-primary" OnClick="Button1_Click"/>
                        <asp:Button ID="Button2" runat="server" Text="退出登录" class="btn btn-danger" OnClick="Button2_Click"/>
                    </div>
                </div>
            </div>
            <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
        </div>
    </form>
    </body>
</html>
