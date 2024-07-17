<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MessagePage.master" AutoEventWireup="true" CodeFile="userinfodetail.aspx.cs" Inherits="UI_infopage_userinfodetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        //<上传>按钮点击事件（JS优先级最高)

        function uppic() {
            //判断要上传的文件的长度大小
            var f = document.getElementById("ContentPlaceHolder1_fulImage");
            if (f.files[0].size > 4 * 1024 * 1024) {
                alert("文件长度过长！！");
                document.getElementById("ContentPlaceHolder1_fulImage").files = null;
                return false;
            }
            return true;
        }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <div class ="col-md-10">
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
                        <li><asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="登出系统" Onclick="btnLogout_Click"/></li>
                      </ul>
                    </div>
                    <asp:Label ID="labelwelcome" CssClass="text-info" runat="server" Text=""></asp:Label>
                    <asp:Image ID="imgHead" class="img-circle" AlternateText="头像" runat="server"/>
                </div>
            </nav>
        </div>
        <div class="col-md-6 col-md-offset-3">
            <div class="panel panel-default" title="信息详情">
            <div class="panel-heading">
                <h3 class=".text-info">信息详情</h3>
            </div>
            <div class="form-group">
            <label for="exampleInputEmail1">账户：</label><asp:TextBox ID="tUaccount" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
            <label for="exampleInputEmail1">姓名：</label><asp:TextBox ID="tUName" CssClass="form-control" runat="server"></asp:TextBox>
            <label for="exampleInputEmail1">性别：</label>
                <asp:RadioButton ID="radSexMale" runat="server"  Text="男" Checked="true" GroupName="sex"/>
                <asp:RadioButton ID="radSexFemale" runat="server" Text="女" GroupName="sex" />
            </div>
            <div class="form-group">
            <label for="exampleInputEmail1">地址：</label><asp:TextBox ID="tUAdress" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
            <label for="exampleInputEmail1">工资：</label><asp:TextBox ID="tUsalary" CssClass="form-control" runat="server"></asp:TextBox>
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
            <div class="form-group">
            <label for="exampleInputEmail1">头像：</label><asp:FileUpload ID="fulImage" CssClass="form-control" runat="server" accept=".jpg,.png,.jpeg" oninput="return uppic();"/>
            </div>
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="修改信息" OnClick="btnUpdate_Click"/>
            <asp:Button ID="btnLogOut" runat="server" CssClass="btn btn-danger" Text="退出登录" OnClick="btnLogOut_Click"/>
            <div class="panel-footer">注意：若需要修改密码，请前往 个人->修改密码 修改</div>
        </div>
        </div>
    </div>
    <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
</asp:Content>

