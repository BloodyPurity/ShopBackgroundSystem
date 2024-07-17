﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aboutus.aspx.cs" Inherits="UI_mainpage_aboutus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>关于我们</title>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet"/>
    <style>
        .jumbotron{
                background:url(../../uploads/BGP.jpg);
                background-size: auto 100%;
                height:500px;
        }
        .jumbotron h1{
            color:white;
        }
        .jumbotron p{
            color:white;
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
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
