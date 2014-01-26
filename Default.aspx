<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PortalEnumerator._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
                <asp:GridView ID="ResultsGridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" Caption="Portal Listing" CaptionAlign="Top" DataKeyNames="VirtualServer,Url" OnDataBinding="ResultsGridView_DataBinding" OnSelectedIndexChanging="ResultsGridView_SelectedIndexChanging" PageSize="18">
                    <Columns>
                        <asp:CommandField ButtonType="Button" EditText="Explore" ShowSelectButton="True" SelectText="Open" />
                        <asp:BoundField DataField="VirtualServer" HeaderText="Virtual Server" SortExpression="VirtualServer" />
                        <asp:HyperLinkField DataNavigateUrlFields="Url" DataTextField="Url" HeaderText="URL"
                            SortExpression="Url" Target="_blank" Text="Url" />
                    </Columns>
                </asp:GridView>
    </form>
</body>
</html>
