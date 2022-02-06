<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Assignment.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_pwd.ClientID %>').value;

            if (str.length < 12) {
                document.getElementById("lbl_pwd").innerHTML = "Password length must be at least 12 characters";
                document.getElementById("lbl_pwd").style.color = "Red";
                return ("too_short");
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwd").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwd").style.color = "Red";
                return ("no_number");
            }

            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwd").innerHTML = "Password require at least 1 uppercase";
                document.getElementById("lbl_pwd").style.color = "Red";
                return ("no_upper");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwd").innerHTML = "Password require at least 1 lowercase";
                document.getElementById("lbl_pwd").style.color = "Red";
                return ("no_lower");
            }
            else if (str.search(/[^a-zA-Z0-9]/) == -1) {
                document.getElementById("lbl_pwd").innerHTML = "Password require at least 1 special character";
                document.getElementById("lbl_pwd").style.color = "Red";
                return ("no_special");
            }
            else {
                document.getElementById("lbl_pwd").innerHTML = "Excellent!";
                document.getElementById("lbl_pwd").style.color = "Green";
                return ("excellent");

            }

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Registration Form:</div>
        <p>
            First Name:
            <asp:TextBox ID="tb_firstName" runat="server"></asp:TextBox>
        </p>
        <p>
            Last Name:
        <asp:TextBox ID="tb_lastName" runat="server"></asp:TextBox>
        </p>
        <p>
            Credit Card Number:
            <asp:TextBox ID="tb_creditCard" runat="server" TextMode="Number"></asp:TextBox>
        </p>
        <p>
            Email:
        <asp:TextBox ID="tb_email" runat="server" TextMode="Email"></asp:TextBox>
        </p>
        <p>
            Password:
            <asp:TextBox ID="tb_pwd" runat="server" onkeyup="javascript:validate()" TextMode="Password"></asp:TextBox>
        &nbsp;<asp:Label ID="lbl_pwd" runat="server" Text="Label" EnableViewState="false"></asp:Label>
        </p>
        <p>
            Date of Birth:
        <asp:TextBox ID="tb_DOB" runat="server" TextMode="Date"></asp:TextBox>
        </p>
        <p>
            Photo:</p>
        <p>
            <asp:FileUpload ID="photo" runat="server" />
        </p>
        <asp:Button ID="btn_reg" runat="server" OnClick="btn_reg_Click" Text="Register" />
    </form>
</body>
</html>
