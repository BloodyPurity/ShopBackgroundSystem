<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userpassword.aspx.cs" Inherits="UI_infopage_userpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改密码</title>
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
    <script type="text/javascript" src="../../Scripts/JavaScriptEncrypt.js"></script>
    <script language="javascript">       
        function pwd() {
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCC0hrRIjb3noDWNtbDpANbjt5Iwu2NFeDwU16Ec87ToqeoIm2KI+cOs81JP9aTDk/jkAlU97mN8wZkEMDr5utAZtMVht7GLX33Wx9XjqxUsDfsGkqNL8dXJklWDu9Zh80Ui2Ug+340d5dZtKtd+nv09QZqGjdnSp9PTfFDBY133QIDAQAB");
            var str = document.getElementById("TextBox2").value;
            var encryptData = encrypt.encrypt(str);//加密后的字符串
            console.log(encryptData);
            document.getElementById("TextBox2").value = encryptData;
            var str1 = document.getElementById("TextBox3").value;
            var encryptData = encrypt.encrypt(str1);//加密后的字符串
            console.log(encryptData);
            document.getElementById("TextBox3").value = encryptData;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return pwd();">
        <div>
        <div class="panel-footer"><h2>个人信息</h2></div>
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
            <div class="panel panel-default" title="修改密码">
                <div class="panel-heading">
                    <h3 class=".text-info">修改密码</h3>
                </div>
                <label for="exampleInputEmail1">原密码：</label><asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <label for="exampleInputEmail1">新密码：</label><asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                
                <asp:Button ID="Button1" runat="server" Text="确认修改" CssClass="btn btn-success" OnClick="Button1_Click"/>
                <asp:Button ID="Button2" runat="server" Text="取消" CssClass="btn btn-danger" OnClick="Button2_Click"/>
                <div class="panel-footer">注意：修改密码后需要重新登录</div>
            </div>
        </div>
       </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
