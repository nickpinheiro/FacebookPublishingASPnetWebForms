<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="update.aspx.cs" Inherits="FacebookPublishingASPnetWebForms.Status.update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox><br /><br />
        <asp:Button ID="Button1" runat="server" Text="Post to Facebook" OnClick="Button1_Click" /><br /><br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
