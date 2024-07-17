<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="UI_adminregister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>普通用户注册</title>
    <style>
    .c{
    }
</style>
    <script type="text/javascript" src="../../Scripts/JavaScriptEncrypt.js"></script>
    <script language="javascript">       
        function pwd() {
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCC0hrRIjb3noDWNtbDpANbjt5Iwu2NFeDwU16Ec87ToqeoIm2KI+cOs81JP9aTDk/jkAlU97mN8wZkEMDr5utAZtMVht7GLX33Wx9XjqxUsDfsGkqNL8dXJklWDu9Zh80Ui2Ug+340d5dZtKtd+nv09QZqGjdnSp9PTfFDBY133QIDAQAB");
            var str = document.getElementById("tUpwd").value;
            var encryptData = encrypt.encrypt(str);//加密后的字符串
            console.log(encryptData);
            document.getElementById("tUpwd").value = encryptData;
            var str1 = document.getElementById("confirmUpwd").value;
            var encryptData = encrypt.encrypt(str1);//加密后的字符串
            console.log(encryptData);
            document.getElementById("confirmUpwd").value = encryptData;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return pwd();">
        <div class="c">
            <p>头像:<asp:FileUpload ID="fulImage" runat="server" accept=".jpg,.png,.jpeg" /></p>
            <p>账&emsp;&emsp;户:<asp:TextBox ID="tUaccount" runat="server" ></asp:TextBox></p>
            <p>密&emsp;&emsp;码:<asp:TextBox ID="tUpwd" runat="server" TextMode="Password"></asp:TextBox></p>
            
            <p>确认密码:<asp:TextBox ID="confirmUpwd" runat="server" TextMode="Password"></asp:TextBox></p>
            <p>姓&emsp;&emsp;名:<asp:TextBox ID="tUName" runat="server"></asp:TextBox></p>
            <p>性&emsp;&emsp;别:
                <asp:RadioButton ID="radSexMale" runat="server"  Text="男" Checked="true" GroupName="sex"/>
                <asp:RadioButton ID="radSexFemale" runat="server" Text="女" GroupName="sex" />
            </p>
            <p>地&emsp;&emsp;址:<asp:TextBox ID="tUAdress" runat="server"></asp:TextBox></p>
            <p>工&emsp;&emsp;资:<asp:TextBox ID="tUsalary" runat="server"></asp:TextBox></p>
            <p>生&emsp;&emsp;日:<asp:TextBox  ID="tUbirth" runat="server" ReadOnly="true"></asp:TextBox></p>
            <asp:Button ID="btnCalendar" runat="server" OnClick="btnCalendar_Click" Text="我被隐藏啦" style="display:none" />
            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" Visible="false"  Width="250px" style="background-color:aliceblue"></asp:Calendar>
            <p>电&emsp;&emsp;话:<asp:TextBox ID="tUphone" runat="server"></asp:TextBox></p>
            <p><asp:Button ID="BtnRegist" runat="server" Text="注册" OnClick="BtnRegist_Click" />
               <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" />
            </p>
            <asp:Label ID="Label1" runat="server" Text="注册信息：无"></asp:Label>
        </div>
    </form>
</body>
</html>
