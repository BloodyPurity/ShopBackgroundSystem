﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="defaultregister.aspx.cs" Inherits="UI_defaultregister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>店员注册(管理员模式)</title>
    <script src="../../Scripts/JavaScriptEncrypt.js"></script>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="https://cdn.bootcss.com/moment.js/2.22.0/moment-with-locales.js"></script>
    <script src="https://cdn.bootcss.com/bootstrap-datetimepicker/4.17.47/js/bootstrap-datetimepicker.min.js"></script>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet"/>
    <link href="https://cdn.bootcss.com/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />

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
    <script language="javascript">       
        function pwd() {
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCC0hrRIjb3noDWNtbDpANbjt5Iwu2NFeDwU16Ec87ToqeoIm2KI+cOs81JP9aTDk/jkAlU97mN8wZkEMDr5utAZtMVht7GLX33Wx9XjqxUsDfsGkqNL8dXJklWDu9Zh80Ui2Ug+340d5dZtKtd+nv09QZqGjdnSp9PTfFDBY133QIDAQAB");
            var str = document.getElementById("tUaccount").value;
            var encryptData = encrypt.encrypt(str);//加密后的字符串
            console.log(encryptData);
            document.getElementById("tUaccount").value = encryptData;
        };
        function userCheck() {
            var pattern1 = /^([A-Za-z0-9_\-\.]{5,18})+$/;
            var pattern2 = /^(13[0-9]|14[01456879]|15[0-35-9]|16[2567]|17[0-8]|18[0-9]|19[0-35-9])\d{8}$/;
            var pattern3 = /^(?:[1-9]\d*|0)(?:\.\d+)?$/;
            var str1 = document.getElementById("tUaccount").value;
            var str2 = document.getElementById("tUphone").value;
            var str3 = document.getElementById("tUsalary").value;
            if (pattern1.test(str1) && pattern2.test(str2) && pattern3.test(str3)) {
                return true;
            }
            alert("请检查输入问题");
            return false;
        };
        $('#datetimepicker1').datetimepicker({
            format: 'YYYY-MM-DD',
            locale: moment.locale('zh-cn'),
            defaultDate: "1970-1-1"
        });
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'YYYY-MM-DD',
                locale: moment.locale('zh-cn')
            });
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
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
            <div class="panel panel-default" title="员工注册">
                <div class="panel-heading">
                    <h3 class=".text-info">员工注册</h3>
                </div>
                <div class="form-group">
                    <label for="exampleInputEmail1">账户：</label><asp:TextBox ID="tUaccount" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-inline">
                    <div class="form-group">
                    <label for="exampleInputEmail1">姓名：</label><asp:TextBox ID="tUName" runat="server" class="form-control"></asp:TextBox>
                    <label for="exampleInputEmail1">性别：</label>
                    <asp:RadioButton ID="radSexMale" runat="server"  Text="男" Checked="true" GroupName="sex"/>
                    <asp:RadioButton ID="radSexFemale" runat="server" Text="女" GroupName="sex" />
                    </div>
                </div>
                <div class="form-group">
                <label for="exampleInputEmail1">地址：</label><asp:TextBox ID="tUAdress" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                <label for="exampleInputEmail1">工资：</label><asp:TextBox ID="tUsalary" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-inline">
                    <div class="form-group">
                        <label for="exampleInputEmail1">生日：</label>
                        <div class='input-group date' id='datetimepicker1'>
                            <asp:TextBox  ID="tUbirth" runat="server" class="form-control"></asp:TextBox>
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                        <label for="exampleInputEmail1">电话：</label><asp:TextBox ID="tUphone" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="BtnRegist" runat="server" Text="注册" class="btn btn-primary" OnClick="BtnRegist_Click"/>
                <asp:Button ID="BtnCancel" runat="server" Text="取消" class="btn btn-danger" OnClick="BtnCancel_Click"/>
                <div class="panel-footer">注意：管理员模式下注册用户为其赋予初始密码123456</div>
            </div>
        </div>
        <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
    </form>
</body>
</html>
