<%@ Page Language="C#" AutoEventWireup="true" CodeFile="product.aspx.cs" Inherits="UI_mainpage_product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>产品</title>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet"/>
    <style>
        strong{
            color:darkgoldenrod;
        }
        .panel-footer{
            background-color:white;
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ul class="nav nav-tabs ">
              <li role="presentation" ><a href="../../UI/mainpage/main"><span class="glyphicon glyphicon-home"></span>主页</a></li>
                <li role="presentation" ><a href="../../UI/mainpage/product"><span class="glyphicon glyphicon-th"></span>产品</a></li>
              <li role="presentation" ><a href="../../UI/mainpage/aboutus"><span class="glyphicon glyphicon-list-alt"></span>关于我们</a></li>
              <li role="presentation" class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
                  <span class="glyphicon glyphicon-user"></span>个人信息 <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                  <li role="presentation" ><a href="../../UI/login/login.aspx">登录/注册</a></li>
                  <li role="presentation" ><a href="../../UI/infopage/userinfodetail.aspx">个人详情</a></li>
                  <li role="presentation" ><a href="../../UI/login/userpassword.aspx">修改密码</a></li>
                </ul>
              </li>
            </ul>
            <h2 style="color:violet">Q1:<small><strong>系统的功能是什么？</strong></small></h2>
            <h2>A1:<small>一个店铺管理系统，它可以用来管理员工，以及商品的进出库。</small></h2>
            <h2>Q2:<small><strong>系统具体的操作有哪些？</strong></small></h2>
            <h2>A2:<small>对于店长来说，可以操作所有店长操作：包括员工管理、出入库管理、商品的添加、信息修改、打折等等。也可以进行所有员工操作。<br />
                          &emsp;&emsp;对于店员来说，可以操作出入库、修改自己的出入库信息、发布店内公告等等。</small></h2>
            <h2>Q3:<small><strong>系统的重难点？</strong></small></h2>
            <h2>A3:<small>作为一个店铺管理系统，不可能允许员工独自创建账号，只能通过店长来完成。<br />
                          &emsp;&emsp;入库操作需要通过选择商品类型来确定供应商和商品，这里的商品必须是上架中的。<br />
                          &emsp;&emsp;出库操作中，有一项特殊处理价格。如果填写这项，则出库时商品价格不会参与打折等操作。<br />
                          &emsp;&emsp;一个小的难点在于：公告发布后需要有提示。
                   </small></h2>
            <h2>Q4:<small><strong>系统还缺什么功能？</strong></small></h2>
            <h2>A4:<small>首先，工人需要工资；<br />
                &emsp;&emsp;其次，每一笔花销都应该有记录，而且店铺的资金可以花到别处，也能从别处来。
                   </small></h2>
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
