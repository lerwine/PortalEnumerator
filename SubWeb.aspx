<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubWeb.aspx.cs" Inherits="PortalEnumerator.SubWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FormView ID="DetailFormView" runat="server" DataKeyNames="Guid" OnDataBinding="DetailFormView_DataBinding"
            OnItemCommand="DetailFormView_ItemCommand" OnPreRender="DetailFormView_PreRender">
            <ItemTemplate>
                <asp:Button ID="PortalButton" runat="server" CommandArgument="Portal" CommandName="Select"
                    Text="Back To Portal" /><br />
                <asp:Button ID="TopButton" runat="server" CommandArgument="Top" CommandName="Select"
                    Text="Go to Top Level Website" /><br />
                <table>
                    <tr>
                        <td style="text-align: right" valign="top">
                            Title:</td>
                        <td valign="top">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>'></asp:Label></td>
                        <td style="text-align: right" valign="top">
                            Site Status:</td>
                        <td valign="top">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                <asp:ListItem>No longer needed</asp:ListItem>
                                <asp:ListItem>Temporarily Inactive (2 weeks or less)</asp:ListItem>
                                <asp:ListItem>Inactive for 2 weeks or more</asp:ListItem>
                                <asp:ListItem>Under Construction</asp:ListItem>
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="TextBox1" runat="server" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            Name:</td>
                        <td valign="top">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                        <td style="text-align: right" valign="top">
                            Site Usage:</td>
                        <td valign="top">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server">
                                <asp:ListItem>Not being used</asp:ListItem>
                                <asp:ListItem>Utilized on a limited basis</asp:ListItem>
                                <asp:ListItem>Utilized within directorate only</asp:ListItem>
                                <asp:ListItem>Utilized by other directorates</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="TextBox2" runat="server" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            Description:</td>
                        <td valign="top">
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>
                        <td style="text-align: right" valign="top">
                            Availability Requirements:</td>
                        <td valign="top">
                            <asp:RadioButtonList ID="RadioButtonList3" runat="server">
                                <asp:ListItem>Site can remain unavailable for 1 day</asp:ListItem>
                                <asp:ListItem>Site can only remain unavailable for a few hours</asp:ListItem>
                                <asp:ListItem>Site must remain available at all times</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="TextBox3" runat="server" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            URL:</td>
                        <td colspan="3" valign="top">
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Url") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            Author:</td>
                        <td colspan="3" valign="top">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("mailto:{0}", Eval("AuthorEmail")) %>'
                                Text='<%# String.Format("{0} ({1})", Eval("AuthorName"), Eval("AuthorLogin")) %>'></asp:HyperLink></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <br />
        <asp:GridView ID="SubSiteGridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Top" DataKeyNames="Guid" OnDataBinding="SubSiteGridView_DataBinding"
            OnPreRender="SubSiteGridView_PreRender" OnSelectedIndexChanging="SubSiteGridView_SelectedIndexChanging"
            PageSize="18">
            <Columns>
                <asp:CommandField ButtonType="Button" ShowCancelButton="False" ShowSelectButton="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:HyperLinkField DataNavigateUrlFields="Url" DataTextField="Url" HeaderText="Url"
                    SortExpression="Url" Target="_blank" />
            </Columns>
        </asp:GridView>
        &nbsp;</div>
    </form>
</body>
</html>
