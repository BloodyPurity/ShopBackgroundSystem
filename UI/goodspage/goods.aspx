<%@ Page Language="C#" AutoEventWireup="true" CodeFile="goods.aspx.cs" Inherits="UI_goodspage_goods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>商品页</title>
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
        .col-md-10{
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
        .img-rounded{
            width:50px;
            height:50px;
        }
        .dropdown-menu li{
            text-align:center;
        }
        th{
            text-align:center;
        }
        tr{
            text-align:center;
        }
    </style>
    <script>
        function digitcheck() {
            var pattern = /^[0-9]+(.[0-9]{1,3})?$/;
            var str = document.getElementById("tbxGname1").value;
            var str1 = document.getElementById("tbxCount").value;
            var str2 = document.getElementById("tbxPrice").value;
            if (pattern.test(str1) && pattern.test(str2) && str!='') {
                return true;
            }
            alert("请检查输入");
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="panel-footer"><h2>商品一览</h2></div>
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
                <div class="panel panel-default" title="商品展示">
                    <div class="panel-heading">
                        <h3 class=".text-info">商品</h3>
                    </div>
                    <div class="form-group" title="商品名">
                        <div class ="col-md-6">
                        <div class=" form-inline">
                            <asp:TextBox ID="tbxSearch" runat="server" CssClass="form-control" placeholder="搜索商品..."></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" class="btn btn-success" Text="提交" OnClick="btnSearch_Click"/>
                        </div>
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" runat="server"
                        BorderStyle="None"
                        GridLines ="None"
                        class="table table-striped"
                        style="text-align:center"
                        AllowPaging="true"
                        PageSize="5"
                        OnPageIndexChanging ="GridView1_PageIndexChanging"
                        OnRowCommand="GridView1_RowCommand"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" ReadOnly="True" ItemStyle-Width="10"/>
                            <asp:TemplateField HeaderText="商品图片">
                                <ItemTemplate>
                                    <asp:Image ID = "img1" class="img-rounded"  ImageUrl = '<%#Eval("gicon") %> '  
                                  runat = "server"  AlternateText = "图片缺失"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="gname" SortExpression="gname" HeaderText="商品名" ReadOnly="True" ItemStyle-Width="150"/>
                            <asp:BoundField DataField="gtype" SortExpression="gtype" HeaderText="类型" ReadOnly="True" ItemStyle-Width="100"/>
                            <asp:BoundField DataField="gcount" SortExpression="gcount" HeaderText="存量" />
                            <asp:BoundField DataField="price" SortExpression="price" HeaderText="价格" />
                            <asp:BoundField DataField="discount" SortExpression="discount" HeaderText="折扣" />
                            <asp:BoundField DataField="notes" SortExpression="notes" HeaderText="备注" ItemStyle-Width="300"/>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" Text="编辑" CommandName="gedit" class="btn btn-primary" runat="server" CommandArgument='<%#Eval("gname")%>'></asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" Text="下架" CommandName="gdelete" class="btn btn-danger" runat="server" CommandArgument='<%#Eval("gname")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="panel-footer">注意：下架后请在 商品信息--已下架商品 中查看</div>
                </div>
                <div class="col-md-8">
                    <div class="panel panel-default" title="添加商品">
                        <div class="panel-heading">
                            <h3 class=".text-info">添加商品</h3>
                        </div>
                         <div class="panel-body">
                             <div class="form-inline">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">商 品 名：</label>
                                    <asp:TextBox ID="tbxGname1" runat="server" class="form-control"></asp:TextBox>
                                 </div>
                                 <div class="form-group">
                                    <label for="exampleInputEmail1">商品图片</label>
                                    <asp:FileUpload ID="fulImage" runat="server" class="form-control" accept=".jpg,.png,.jpeg" />
                                </div>
                             </div>
                             <label for="exampleInputName2"></label>
                            <div class="form-inline">
                              <div class="form-group">
                                <label for="exampleInputPassword1">商品类型：</label>
                                  <asp:DropDownList ID="ddlGtype1" class="form-control" runat="server" DataValueField="name" DataTextField="name"></asp:DropDownList>
                              </div>
                                <div class="form-group">
                                  <label for="exampleInputPassword1">商品数量：</label>
                                  <asp:TextBox ID="tbxCount" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                  <label for="exampleInputPassword1">上架价格：</label>
                                  <asp:TextBox ID="tbxPrice" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                 </div>
                             <label for="exampleInputName2"></label>
                              <div class="form-group">
                                <label for="exampleInputFile">商品信息：</label>
                                <asp:TextBox ID="tbxNotes" runat="server" class="form-control"></asp:TextBox>
                              </div>
                              <asp:Button ID="btnSubmitGoods" runat="server" Text="确定" Onclick="btnSubmitGoods_Click" class="btn btn-primary"/>
                           </div>
                            <div class="panel-footer">注意：尽量不要重复添加同一商品</div>
                    </div>
                </div>
                <div class="col-md-4">
                <div class="panel panel-default" title="打折最终价格为 原价格×折扣">
                    <div class="panel-heading">
                        <h3 class=".text-info">商品打折</h3>
                    </div>
                    <div class="panel-body">
                    <div class="form-group">
                        <label for="exampleInputName2">商品名：</label><asp:TextBox ID="tbxGname" class="form-control" runat="server"></asp:TextBox>
                    </div>
                        <label for="exampleInputName2"></label>
                    <div class="form-inline">
                        <label for="exampleInputName2">商品类型：</label>
                        <asp:DropDownList ID="ddlGtype" class="form-control" runat="server" DataValueField="name" DataTextField="name"></asp:DropDownList>
                    </div>
                    <label for="exampleInputFile"></label>
                    <div class="form-inline">
                        <label for="exampleInputName2">折扣力度：</label>
                        <asp:DropDownList ID="ddlDiscount" runat="server" DataValueField="name" DataTextField="name" class="form-control">
                                <asp:ListItem Value="1">原价</asp:ListItem>
                                <asp:ListItem Value="0.95">95%</asp:ListItem>
                                <asp:ListItem Value="0.9">90%</asp:ListItem>
                                <asp:ListItem Value="0.85">85%</asp:ListItem>
                                <asp:ListItem Value="0.8">80%</asp:ListItem>
                                <asp:ListItem Value="0.75">75%</asp:ListItem>
                                <asp:ListItem Value="0.7">70%</asp:ListItem>
                                <asp:ListItem Value="0.65">65%</asp:ListItem>
                                <asp:ListItem Value="0.6">60%</asp:ListItem>
                                <asp:ListItem Value="0.55">55%</asp:ListItem>
                                <asp:ListItem Value="0.5">50%</asp:ListItem>
                                <asp:ListItem Value="0.45">45%</asp:ListItem>
                                <asp:ListItem Value="0.4">40%</asp:ListItem>
                                <asp:ListItem Value="0.35">35%</asp:ListItem>
                                <asp:ListItem Value="0.3">30%</asp:ListItem>
                                <asp:ListItem Value="0.25">25%</asp:ListItem>
                                <asp:ListItem Value="0.2">20%</asp:ListItem>
                                <asp:ListItem Value="0.15">15%</asp:ListItem>
                                <asp:ListItem Value="0.1">10%</asp:ListItem>
                                <asp:ListItem Value="0.05">5%</asp:ListItem>
                          </asp:DropDownList>
                    </div>
                        <asp:Button ID="btnSubmit" runat="server" Text="确定" class="btn btn-primary" Onclick="btnSubmit_Click"/>
                    </div>
                    <div class="panel-footer">注意：商品名和商品类型选一项即可，多选操作无效</div>
                </div>
                </div>
            </div>
                <div class="panel-footer">店铺管理系统<a href="https://github.com/bloodypurity">Copyright © 2024 BloodyPurity.All rights reserved.</a></div>
        </div>
    </form>
</body>
</html>
