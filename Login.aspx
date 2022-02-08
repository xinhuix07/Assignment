<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src ="https://www.google.com/recaptcha/api.js?render=SITE KEY"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Login<br />
            <br />
            Email:<asp:TextBox ID="tb_email" runat="server" TextMode="Email"></asp:TextBox>
            <br />
            <br />
            Password:<asp:TextBox ID="tb_pwd" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="Login"/>
            <br />
            <br />
            <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response" />
            <asp:Label ID="Label1" runat="server" EnableViewState="false"></asp:Label>
            <br />
        </div>
    </form>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('SITE KEY', { action: "Login" }).then(function(token){
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
