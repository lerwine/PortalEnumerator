<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Portal.aspx.cs" Inherits="PortalEnumerator.Portal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="PortalSitesGridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Caption="Portal Site Collections" CaptionAlign="Top"
            DataKeyNames="Guid" OnDataBinding="PortalSitesGridView_DataBinding" OnSelectedIndexChanging="PortalSitesGridView_SelectedIndexChanging"
            PageSize="18">
            <Columns>
                <asp:CommandField ButtonType="Button" ShowCancelButton="False" ShowSelectButton="True" />
                <asp:HyperLinkField DataNavigateUrlFields="Url" DataTextField="Url" HeaderText="Url" Target="_blank" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
