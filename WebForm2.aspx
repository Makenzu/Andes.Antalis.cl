<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WebForm2.aspx.vb" Inherits="app.WebForm2"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server" ID="Head1">

    <title></title>

    <script src="/js/jquery-1.5.1.js" type="text/javascript"></script>

    <script language="javascript">
        $(document).ready(function() {
            $("#btnAdd").click(function() {
                $("#ListBox1 > option[@selected]").appendTo("#ListBox2");
            });
            $("#btnAddAll").click(function() {
                $("#ListBox1 > option").appendTo("#ListBox2");
            });
            $("#btnRemove").click(function() {
                $("#ListBox2 > option[@selected]").appendTo("#ListBox1");
            });
            $("#btnRemoveAll").click(function() {
                $("#ListBox2 > option").appendTo("#ListBox1");
            });
        });              
    </script>
</head>

<body>

    <form id="form1" runat="server">

    <div>

        <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
        </asp:ListBox>
        <input id="btnAdd" type="button" value="Add" />
        <input id="btnAddAll" type="button" value="Add All" />
        <input id="btnRemove" type="button" value="Remove" />
        <input id="btnRemoveAll"type="button" value="Remove All" />
        <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Multiple"></asp:ListBox>  
    </div>

    </form>

</body>

</html>
