<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteCollection.aspx.cs" Inherits="PortalEnumerator.SiteCollection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FormView ID="DetailFormView" runat="server" OnDataBinding="DetailFormView_DataBinding"
            OnItemCommand="DetailFormView_ItemCommand" OnPreRender="DetailFormView_PreRender">
            <ItemTemplate>
                <asp:Button ID="TopButton" runat="server" CommandName="Select" Text="Back To Portal" /><br />
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label><br />
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title") %>'></asp:Label><br />
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
            </ItemTemplate>
        </asp:FormView>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <asp:GridView ID="NewSitesGridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Top" DataKeyNames="Guid" OnDataBinding="NewSitesGridView_DataBinding" PageSize="18" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" Caption="Sites to be assessed" CellPadding="4" Font-Size="X-Large" OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdating="NewSitesGridView_RowUpdating">
            <FooterStyle BackColor="#FFFFCC" Font-Size="Large" ForeColor="#330099" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
                <asp:BoundField DataField="Url" HeaderText="Url" ReadOnly="True" SortExpression="Url" />
                <asp:TemplateField HeaderText="Status" SortExpression="SiteStatusText">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SiteStatusDataSource"
                            DataTextField="Text" DataValueField="ID" SelectedValue='<%# Bind("SiteStatusId") %>' AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="-1">...Select Item</asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SiteStatusOther") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# (Convert.ToBoolean(Eval("SiteStatusIsOther"))) ? Eval("SiteStatusOther") : Eval("SiteStatusText") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Usage" SortExpression="SiteUsageText">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SiteUsageDataSource"
                            DataTextField="Text" DataValueField="ID" SelectedValue='<%# Bind("SiteUsageId") %>' AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="-1">...Select Item</asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SiteUsageOther") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# (Convert.ToBoolean(Eval("SiteUsageIsOther"))) ? Eval("SiteUsageOther") : Eval("SiteUsageText") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Availability Requirement" SortExpression="AvailabilityRequirementText">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="AvailabilityRequirementDataSource"
                            DataTextField="Text" DataValueField="ID" SelectedValue='<%# Bind("AvailabilityRequirementId") %>' AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="-1">...Select Item</asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("AvailabilityRequirementOther") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# (Convert.ToBoolean(Eval("AvailabilityRequirementIsOther"))) ? Eval("AvailabilityRequirementOther") : Eval("AvailabilityRequirementText") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle BackColor="White" Font-Size="Medium" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <PagerStyle BackColor="#FFFFCC" Font-Size="Large" ForeColor="#330099" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="Medium" ForeColor="#FFFFCC" />
        </asp:GridView>
            </asp:View>
            <asp:View ID="View2" runat="server">
            </asp:View>
        </asp:MultiView>&nbsp;
        <br />
        <asp:GridView ID="SubSiteGridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Top" DataKeyNames="Guid" PageSize="18" Caption="Completed Site Assessments" CellPadding="4" DataSourceID="WebCollectionDataSource" EmptyDataText="No site assessments have been completed" Font-Size="X-Large" ForeColor="#333333" GridLines="None" OnDataBound="SubSiteGridView_DataBound" OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdated="SubSiteGridView_RowUpdated">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="Medium" ForeColor="White" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
                <asp:BoundField DataField="Url" HeaderText="Url" ReadOnly="True" SortExpression="Url" />
                <asp:BoundField DataField="SiteStatusText" HeaderText="Status" SortExpression="SiteStatusText" />
                <asp:BoundField DataField="SiteUsageText" HeaderText="Usage" SortExpression="SiteUsageText" />
                <asp:BoundField DataField="AvailabilityRequirementText" HeaderText="Availability Requirement"
                    SortExpression="AvailabilityRequirementText" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
            </Columns>
            <RowStyle BackColor="#F7F6F3" Font-Size="Medium" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="Medium" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:SqlDataSource ID="WebCollectionDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SpAssessmentConnectionString %>"
            InsertCommand="INSERT INTO Sites(Guid, CollectionGuid, Name, Url, SiteStatusId, SiteStatusOther, SiteUsageId, SiteUsageOther, AvailabilityRequirementId, AvailabilityRequirementOther, Notes, EnteredBy, EnteredOn, ModifiedBy, ModifiedOn) VALUES (@Guid, @CollectionGuid, @Name, @Url, @SiteStatusId, @SiteStatusOther, @SiteUsageId, @SiteUsageOther, @AvailabilityRequirementId, @AvailabilityRequirementOther, @Notes, @EnteredBy, @EnteredOn, @EnteredBy, @EnteredOn)"
            SelectCommand="SELECT [Guid], [CollectionGuid], [Name], [Url], [SiteStatusId], [SiteStatusIsOther], [SiteStatusText], [SiteStatusOther], [SiteUsageId], [SiteUsageIsOther], [SiteUsageText], [SiteUsageOther], [AvailabilityRequirementId], [AvailabilityRequirementIsOther], [AvailabilityRequirementText], [AvailabilityRequirementOther], [Notes], [EnteredBy], [EnteredOn], [ModifiedBy], [ModifiedOn] FROM [SiteDetails] WHERE ([CollectionGuid] = @CollectionGuid) ORDER BY [Url]"
            UpdateCommand="UPDATE Sites SET SiteStatusId = @SiteStatusId, SiteStatusOther = @SiteStatusOther, SiteUsageId = @SiteUsageId, SiteUsageOther = @SiteUsageOther, AvailabilityRequirementId = @AvailabilityRequirementId, AvailabilityRequirementOther = @AvailabilityRequirementOther, Notes = @Notes, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE (Guid = @Guid)">
            <UpdateParameters>
                <asp:ControlParameter ControlID="SubSiteGridView" Name="SiteStatusId" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="SiteStatusOther" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="SiteUsageId" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="SiteUsageOther" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="AvailabilityRequirementId"
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="AvailabilityRequirementOther"
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="Notes" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="ModifiedBy" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="ModifiedOn" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="SubSiteGridView" Name="Guid" PropertyName="SelectedValue" />
            </UpdateParameters>
            <SelectParameters>
                <asp:SessionParameter Name="CollectionGuid" SessionField="Site" Type="String" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="Guid" />
                <asp:Parameter Name="CollectionGuid" />
                <asp:Parameter Name="Name" />
                <asp:Parameter Name="Url" />
                <asp:Parameter Name="SiteStatusId" />
                <asp:Parameter Name="SiteStatusOther" />
                <asp:Parameter Name="SiteUsageId" />
                <asp:Parameter Name="SiteUsageOther" />
                <asp:Parameter Name="AvailabilityRequirementId" />
                <asp:Parameter Name="AvailabilityRequirementOther" />
                <asp:Parameter Name="Notes" />
                <asp:Parameter Name="EnteredBy" />
                <asp:Parameter Name="EnteredOn" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SiteStatusDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SpAssessmentConnectionString %>"
            SelectCommand="SELECT [ID], [Text] FROM [SiteStatus] ORDER BY [IsOther], [Text]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SiteUsageDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SpAssessmentConnectionString %>"
            SelectCommand="SELECT [ID], [Text] FROM [SiteUsage] ORDER BY [IsOther], [Text]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="AvailabilityRequirementDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SpAssessmentConnectionString %>"
            SelectCommand="SELECT [ID], [Text] FROM [AvailabilityRequirement] ORDER BY [IsOther], [Text]">
        </asp:SqlDataSource>
        &nbsp;
    
    </div>
    </form>
</body>
</html>
