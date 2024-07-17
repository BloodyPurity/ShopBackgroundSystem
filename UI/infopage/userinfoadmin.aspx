<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userinfoadmin.aspx.cs" Inherits="UI_infopage_userinfoadmin" %>

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
        .footer {
            position: absolute;
            bottom: 0;  width: 100%;
            /* Set the fixed height of the footer here */
            height: 60px;
            background-color: #f5f5f5;
        }
        .dropdown-menu li{
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel-footer"><h2>员工详情</h2></div>
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
            <div class="col-md-4 col-md-offset-4">
                <div class="panel-body">
                    <p>账户:<asp:TextBox CssClass="form-control" ID="tUaccount" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <p>类型:<asp:TextBox CssClass="form-control" ID="tUtype" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <p>姓名:<asp:TextBox CssClass="form-control" ID="tUName" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <p>性别:<asp:TextBox CssClass="form-control" ID="tUSex" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <p>地址:<asp:TextBox CssClass="form-control" ID="tUAdress" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <p>工资:<asp:TextBox CssClass="form-control" ID="tUsalary" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <p>生日:<asp:TextBox CssClass="form-control" ID="tUbirth" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <p>电话:<asp:TextBox CssClass="form-control" ID="tUphone" runat="server" ReadOnly="true"></asp:TextBox></p>
                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="返回" OnClick="Button1_Click" />
                </div>
            </div>
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
