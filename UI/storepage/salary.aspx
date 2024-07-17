<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salary.aspx.cs" Inherits="UI_storepage_salary" %>

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
        <div class="panel-footer"><h2>工资管理</h2></div>
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

        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
