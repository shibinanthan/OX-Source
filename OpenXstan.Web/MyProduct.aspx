<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProduct.aspx.cs" Inherits="OpenXstan.Web.MyProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListView ID="listProduct" runat="server">
            <ItemTemplate>
                <%#Eval("Id") %>-<%#Eval("Title") %><br />
            </ItemTemplate>
        </asp:ListView>
    </div>
    </form>
</body>
</html>
