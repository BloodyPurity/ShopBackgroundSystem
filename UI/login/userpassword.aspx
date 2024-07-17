<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userpassword.aspx.cs" Inherits="UI_login_userpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改密码</title>
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
            min-height:55vh;
        }
    </style>
    <script type="text/javascript" src="../../Scripts/JavaScriptEncrypt.js"></script>
    <script language="javascript">       
        function pwd() {
            var str0 = document.getElementById("TextBox0").value;
            if (str0 == null || str0 == '') {
                alert("请输入账号");
                return false;
            }
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCC0hrRIjb3noDWNtbDpANbjt5Iwu2NFeDwU16Ec87ToqeoIm2KI+cOs81JP9aTDk/jkAlU97mN8wZkEMDr5utAZtMVht7GLX33Wx9XjqxUsDfsGkqNL8dXJklWDu9Zh80Ui2Ug+340d5dZtKtd+nv09QZqGjdnSp9PTfFDBY133QIDAQAB");
            var str = document.getElementById("TextBox1").value;
            if (str == null || str == '') {
                alert("请输入密码");
                return false;
            }
            var encryptData = encrypt.encrypt(str);//加密后的字符串
            console.log(encryptData);
            document.getElementById("TextBox1").value = encryptData;
            var str1 = document.getElementById("TextBox2").value;
            if (str1 == null || str1 == '') {
                alert("请输入密码");
                return false;
            }
            var encryptData = encrypt.encrypt(str1);//加密后的字符串
            console.log(encryptData);
            document.getElementById("TextBox2").value = encryptData;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
            <div class ="form-group">
            <label for="exampleInputEmail1">账号</label><asp:TextBox ID="TextBox0" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
            <div class ="form-group">
            <label for="exampleInputEmail1">密码</label><asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <div class ="form-group">
            <label for="exampleInputEmail1">确认密码</label><asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-group">
                <div class="panel-footer">
                    <asp:Button ID="Button1" runat="server" Text="确认修改" CssClass="btn btn-success" OnClick="Button1_Click"/>
                    <asp:Button ID="Button2" runat="server" Text="取消" CssClass="btn btn-danger" OnClick="Button2_Click"/>
                </div>
            </div>
        </div>
        </div>
         <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
