<%@ Page Language="C#" AutoEventWireup="true" CodeFile="asset.aspx.cs" Inherits="UI_storepage_asset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
        </div>
    </form>
</body>
</html>
