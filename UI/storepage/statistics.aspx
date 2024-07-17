<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statistics.aspx.cs" Inherits="UI_store_statistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/echarts5.min.js"></script>
    
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
        <div class="panel-footer"><h2>经营概况</h2></div>
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
                    <h3>店铺财产</h3>
                </div>
                <div class="form-group">
                    <label for="exampleInputName2"></label>
                    <div class="form-inline">
                    <label for="exampleInputEmail1">店铺资产：</label><asp:TextBox ID="tbxmoney" CssClass="form-control" runat="server"></asp:TextBox>
                    <label for="exampleInputEmail1">利润：</label><asp:TextBox ID="tbxincome" CssClass="form-control" runat="server"></asp:TextBox>
                    <label for="exampleInputEmail1">状态：</label><asp:TextBox ID="tbxnow" CssClass="form-control" runat="server"></asp:TextBox>
                    <label for="exampleInputName2"></label>
                    <asp:Button ID="btnDetail" runat="server" class="btn btn-success" Text="详情" onclick="btnDetail_Click"/>
                    </div>
                </div>
                <div class="panel-footer">注意：无</div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3>销售统计</h3>
                </div>
                <div class ="col-md-4">
                    <div class="form-group">
                    <div id="myChart-pie1" style="width: 400px;height:400px;"></div>
                    </div>
                </div>
                <div class ="col-md-7 col-md-offset-1">
                    <div class="form-group">
                    <div id="myChart-histogram1" style="width: 700px; height: 400px;"></div>
                    </div>
                </div>
                <div class="panel-footer">注意：图中所有数据仅包含销售，即出库所得</div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3>进货统计</h3>
                </div>
                <div class ="col-md-4">
                    <div class="form-group">
                    <div id="myChart-pie2" style="width: 400px;height:400px;"></div>
                    </div>
                </div>
                <div class ="col-md-7 col-md-offset-1">
                    <div class="form-group">
                    <div id="myChart-histogram2" style="width: 700px; height: 400px;"></div>
                    </div>
                </div>
                <div class="panel-footer">注意：图中所有数据仅包含进货，即入库所得</div>
            </div>
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
    <script>
        <%--<%=''%>--%>
        <%= space%>
        var myChartPie1 = echarts.init(document.getElementById('myChart-pie1'));
        //定义饼图数据
        var data = [<%foreach (var i in Price_Gtype){%>{ value:<%= i.Value%>, name:'<%= i.Key%>'},<%};%>];
        //定义饼图配置项
        var option1 = {
            title: {
                text: '销售额',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: 'left',
                data: [<%foreach (var i in Price_Gtype){%>'<%= i.Key%>',<%};%>],
            },
            series: [
                {
                    name: '销售额',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '60%'],
                    data: data,
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };
        myChartPie1.setOption(option1);
        var myChartHistogram1 = echarts.init(document.getElementById('myChart-histogram1'));
        var option2 = {
            title: {
                text: '销量'
            },
            tooltip: {},
            legend: {
                data: ['销量']
            },
            xAxis: {
                data: [<%foreach (var i in Count_Gtype){%>'<%= i.Key%>',<%};%>]
            },
            yAxis: {},
            series: [{
                name: '销量',
                type: 'bar',
                data: [<%foreach (var i in Count_Gtype){%><%= i.Value%>,<%};%>]
            }]
        };
        myChartHistogram1.setOption(option2);
        var myChartPie2 = echarts.init(document.getElementById('myChart-pie2'));
        //定义饼图数据
        var data = [<%foreach (var i in Cost_Gtype){%>{ value:<%= i.Value%>, name:'<%= i.Key%>' },<%};%>];
        //定义饼图配置项
        var option3 = {
            title: {
                text: '进货额',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: 'left',
                data: [<%foreach (var i in Cost_Gtype){%>'<%= i.Key%>',<%};%>],
            },
            series: [
                {
                    name: '进货额',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '60%'],
                    data: data,
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };
        myChartPie2.setOption(option3);
        var myChartHistogram2 = echarts.init(document.getElementById('myChart-histogram2'));
        var option4 = {
            title: {
                text: '进货量'
            },
            tooltip: {},
            legend: {
                data: ['进货量']
            },
            xAxis: {
                data: [<%foreach (var i in Count_GtypeIn){%>'<%= i.Key%>',<%};%>]
            },
            yAxis: {},
            series: [{
                name: '进货量',
                type: 'bar',
                        data: [<%foreach (var i in Count_GtypeIn){%><%= i.Value%>,<%};%>]
                    }]
                };
        myChartHistogram2.setOption(option4);
    </script>
</html>
