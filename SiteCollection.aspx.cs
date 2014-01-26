using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;

namespace PortalEnumerator
{
    public partial class SiteCollection : System.Web.UI.Page
    {
        private SPSite _sPSite = null;

        protected SPSite SPSite
        {
            get
            {
                Guid guid;

                if (this._sPSite == null)
                {
                    guid = new Guid(Convert.ToString(Session["Site"]));
                    this._sPSite = new SPSite(guid);
                }

                return this._sPSite;
            }
        }

        protected void DetailFormView_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ((FormView)sender).DataBind();
        }

        protected void DetailFormView_DataBinding(object sender, EventArgs e)
        {
            DataTable table;
            DataColumn urlColumn, nameColumn, guidColumn, authorNameColumn, authorEmailColumn,
                authorLoginColumn, descriptionColumn, titleColumn;
            DataRow row;
            SPWeb web;

            table = new DataTable();

            titleColumn = new DataColumn("Title", Type.GetType("System.String"));
            table.Columns.Add(titleColumn);

            nameColumn = new DataColumn("Name", Type.GetType("System.String"));
            table.Columns.Add(nameColumn);

            urlColumn = new DataColumn("Url", Type.GetType("System.String"));
            table.Columns.Add(urlColumn);

            authorNameColumn = new DataColumn("AuthorName", Type.GetType("System.String"));
            table.Columns.Add(authorNameColumn);

            authorEmailColumn = new DataColumn("AuthorEmail", Type.GetType("System.String"));
            table.Columns.Add(authorEmailColumn);

            authorLoginColumn = new DataColumn("AuthorLogin", Type.GetType("System.String"));
            table.Columns.Add(authorLoginColumn);

            descriptionColumn = new DataColumn("Description", Type.GetType("System.String"));
            table.Columns.Add(descriptionColumn);

            guidColumn = new DataColumn("Guid", Type.GetType("System.String"));
            table.Columns.Add(guidColumn);

            web = this.SPSite.OpenWeb();

            row = table.NewRow();
            row[nameColumn] = web.Name;
            row[urlColumn] = web.Url;
            row[guidColumn] = this.SPSite.ID.ToString("D");
            row[authorNameColumn] = web.Author.Name;
            row[authorEmailColumn] = web.Author.Email;
            row[authorLoginColumn] = web.Author.LoginName;
            row[descriptionColumn] = web.Description;
            row[titleColumn] = web.Title;

            table.Rows.Add(row);

            ((FormView)sender).DataSource = table;
        }

        protected void DetailFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Session.Remove("Site");
                Response.Redirect(ResolveUrl("Portal.aspx"), true);
            }
        }

        protected void SubSiteGridView_DataBound(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.NewSitesGridView.DataBind();
        }

        protected void NewSitesGridView_DataBinding(object sender, EventArgs e)
        {
            DataTable table;
            DataRow row;
            SPWeb web;
            bool keyFound;
 
            table = new DataTable();

            table.Columns.Add(new DataColumn("Guid", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("CollectionGuid", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Url", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("SiteStatusId", Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("SiteStatusIsOther", Type.GetType("System.Boolean")));
            table.Columns.Add(new DataColumn("SiteStatusText", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("SiteStatusOther", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("SiteUsageId", Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("SiteUsageIsOther", Type.GetType("System.Boolean")));
            table.Columns.Add(new DataColumn("SiteUsageText", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("SiteUsageOther", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("AvailabilityRequirementId", Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("AvailabilityRequirementIsOther", Type.GetType("System.Boolean")));
            table.Columns.Add(new DataColumn("AvailabilityRequirementText", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("AvailabilityRequirementOther", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Notes", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("EnteredBy", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("EnteredOn", Type.GetType("System.DateTime")));
            table.Columns.Add(new DataColumn("ModifiedBy", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ModifiedOn", Type.GetType("System.DateTime")));

            web = this.SPSite.OpenWeb();

            ((GridView)sender).Caption = "All Sub-webs for " + web.Name;

            foreach (SPWeb subweb in this.SPSite.AllWebs)
            {
                row = table.NewRow();
                keyFound = false;
                foreach (DataKey key in this.SubSiteGridView.DataKeys)
                {
                    if (Convert.ToString(key.Values["Guid"]) == subweb.ID.ToString("D"))
                        keyFound = true;
                }

                if (keyFound)
                    continue;

                row[table.Columns["Guid"]] = subweb.ID.ToString("D");
                row[table.Columns["CollectionGuid"]] = SPSite.ID.ToString("D");
                row[table.Columns["Name"]] = subweb.Name;
                row[table.Columns["Url"]] = subweb.Url;
                row[table.Columns["SiteStatusId"]] = -1;
                row[table.Columns["SiteStatusIsOther"]] = false;
                row[table.Columns["SiteStatusText"]] = "";
                row[table.Columns["SiteStatusOther"]] = "";
                row[table.Columns["SiteUsageId"]] = -1;
                row[table.Columns["SiteUsageIsOther"]] = false;
                row[table.Columns["SiteUsageText"]] = "";
                row[table.Columns["SiteUsageOther"]] = "";
                row[table.Columns["AvailabilityRequirementId"]] = -1;
                row[table.Columns["AvailabilityRequirementIsOther"]] = false;
                row[table.Columns["AvailabilityRequirementText"]] = "";
                row[table.Columns["AvailabilityRequirementOther"]] = "";
                row[table.Columns["Notes"]] = "";
                row[table.Columns["EnteredBy"]] = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                row[table.Columns["EnteredOn"]] = DateTime.UtcNow;
                row[table.Columns["ModifiedBy"]] = row[table.Columns["EnteredBy"]];
                row[table.Columns["ModifiedOn"]] = row[table.Columns["EnteredOn"]];
                table.Rows.Add(row);
            }

            ((GridView)sender).DataSource = table;
        }

        protected object GridviewDataItem(GridView gridView, int rowIndex, string columName)
        {
            if (rowIndex < 1 || rowIndex >= gridView.Rows.Count ||
                    ((DataRowView)(gridView.Rows[rowIndex].DataItem)).Row.Table.Columns[columName] == null)
                return null;

            return ((DataRowView)(gridView.Rows[rowIndex].DataItem)).Row[((DataRowView)(gridView.Rows[rowIndex].DataItem)).Row.Table.Columns[columName]];
        }

        protected void NewSitesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Convert.ToInt64(this.GridviewDataItem((GridView)sender, e.RowIndex, "SiteStatusId")) == -1 ||
                Convert.ToInt64(this.GridviewDataItem((GridView)sender, e.RowIndex, "SiteUsageId")) == -1 ||
                Convert.ToInt64(this.GridviewDataItem((GridView)sender, e.RowIndex, "AvailabilityRequirementId")) == -1)
            {
                e.Cancel = true;
                return;
            }

            this.WebCollectionDataSource.InsertParameters["@Guid"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "Guid"));
            this.WebCollectionDataSource.InsertParameters["@CollectionGuid"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "CollectionGuid"));
            this.WebCollectionDataSource.InsertParameters["@Name"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "Name"));
            this.WebCollectionDataSource.InsertParameters["@Url"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "Url"));
            this.WebCollectionDataSource.InsertParameters["@SiteStatusId"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "SiteStatusId"));
            this.WebCollectionDataSource.InsertParameters["@SiteStatusOther"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "SiteStatusOther"));
            this.WebCollectionDataSource.InsertParameters["@SiteUsageId"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "SiteUsageId"));
            this.WebCollectionDataSource.InsertParameters["@SiteUsageOther"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "SiteUsageOther"));
            this.WebCollectionDataSource.InsertParameters["@AvailabilityRequirementId"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "AvailabilityRequirementId"));
            this.WebCollectionDataSource.InsertParameters["@AvailabilityRequirementOther"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "AvailabilityRequirementOther"));
            this.WebCollectionDataSource.InsertParameters["@Notes"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "Notes"));
            this.WebCollectionDataSource.InsertParameters["@EnteredBy"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "EnteredBy"));
            this.WebCollectionDataSource.InsertParameters["@EnteredOn"].DefaultValue = Convert.ToString(this.GridviewDataItem((GridView)sender, e.RowIndex, "EnteredOn"));
            
            this.WebCollectionDataSource.Insert();

            Response.Redirect(Request.Path, true);
        }

        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ((GridView)sender).EditIndex = e.NewEditIndex;
            ((GridView)sender).DataBind();
        }

        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ((GridView)sender).EditIndex = -1;
            ((GridView)sender).DataBind();
        }

        protected void SubSiteGridView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            ((GridView)sender).EditIndex = -1;
            ((GridView)sender).DataBind();
        }
    }
}
