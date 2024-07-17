<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="UI_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录页</title>
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
            min-height:65vh;
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
        }
        function uaccountCheck() {
            var pattern1 = /^([A-Za-z0-9_\-\.]{5,18})+$/;
            var str1 = document.getElementById("TextBox1").value;
            var str2 = document.getElementById("TextBox2").value;
            if (pattern1.test(str1) && pattern1.test(str2)) {
                return true;
            }

            alert("账号或密码错误");
            return false;
        }
        function reg() {
            alert("暂未开放普通注册，请联系店长");
        }
    </script>
</head>
<body>
    <form  id="loginform" runat="server" onsubmit="return pwd();">
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
        <div class="col-md-12">
        <div class="col-md-4 col-md-offset-4">
              <div class="form-group">
                <label for="inputEmail3">账号</label>
                <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
              </div>
              <div class="form-group">
                <label for="inputPassword3">密码</label>
                <asp:TextBox ID="TextBox2" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
              </div>
              <div class="form-group">
                <div class="panel-footer">
                    <asp:Button ID="Button1" class="btn btn-default" runat="server" Text="登录" OnClick="Button1_Click" />
                    <button id="btnemp" class="btn btn-success"  onclick="return reg();">员工注册</button>
                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="管理员注册" OnClick="Button2_Click" />
                    <p><asp:Literal ID="LiteralMsg" runat="server"></asp:Literal></p>
                </div>
              </div>
        </div>
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>

</html>