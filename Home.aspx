<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Assignment.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>SITConnect</legend>
                </br>

                <br />
                <asp:Label ID="Label1" runat="server" EnableViewState="false"></asp:Label>
                <br />
                <br />
                Email:
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                Credit card number:
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btn_logout" runat="server" Text="Logout" OnClick="Logout" Visible="false"/>

            </fieldset>
        </div>
    </form>
</body>
</html>
