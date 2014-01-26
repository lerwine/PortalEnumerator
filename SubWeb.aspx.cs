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
    public partial class SubWeb : System.Web.UI.Page
    {
        private SPSite _sPSite = null;
        private SPWeb _spWeb = null;

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

        protected SPWeb SPWeb
        {
            get
            {
                Guid guid;

                if (this._spWeb == null)
                {
                    guid = new Guid(Convert.ToString(Session["Web"]));

                    foreach (SPWeb web in this.SPSite.AllWebs)
                    {
                        if (web.ID == guid)
                            this._spWeb = web;
                    }
                }

                return this._spWeb;
            }
        }

        protected void SubSiteGridView_DataBinding(object sender, EventArgs e)
        {
            DataTable table;
            DataColumn nameColumn, urlColumn, guidColumn;
            DataRow row;
            SPWeb web;

            table = new DataTable();

            nameColumn = new DataColumn("Name", Type.GetType("System.String"));
            table.Columns.Add(nameColumn);

            urlColumn = new DataColumn("Url", Type.GetType("System.String"));
            table.Columns.Add(urlColumn);

            guidColumn = new DataColumn("Guid", Type.GetType("System.String"));
            table.Columns.Add(guidColumn);

            web = this.SPSite.OpenWeb();

            ((GridView)sender).Caption = "Sub-webs for " + this.SPWeb.Name;

            foreach (SPWeb subweb in this.SPWeb.Webs)
            {
                if (subweb.Url == this.SPWeb.Url)
                    continue;

                row = table.NewRow();

                row[nameColumn] = subweb.Name;
                row[urlColumn] = subweb.Url;
                row[guidColumn] = subweb.ID.ToString("D");

                table.Rows.Add(row);
            }

            ((GridView)sender).DataSource = table;
        }

        protected void SubSiteGridView_PreRender(object sender, EventArgs e)
        {
            ((GridView)sender).DataBind();
        }

        protected void DetailFormView_PreRender(object sender, EventArgs e)
        {
            ((FormView)sender).DataBind();
        }

        protected void DetailFormView_DataBinding(object sender, EventArgs e)
        {
            DataTable table;
            DataColumn urlColumn, nameColumn, guidColumn, parentNameColumn, parentGuidColumn, parentUrlColumn, topUrlColumn, topGuidColumn, authorNameColumn, authorEmailColumn,
                authorLoginColumn, descriptionColumn, titleColumn;
            DataRow row;

            table = new DataTable();

            titleColumn = new DataColumn("Title", Type.GetType("System.String"));
            table.Columns.Add(titleColumn);

            nameColumn = new DataColumn("Name", Type.GetType("System.String"));
            table.Columns.Add(nameColumn);

            urlColumn = new DataColumn("Url", Type.GetType("System.String"));
            table.Columns.Add(urlColumn);

            parentNameColumn = new DataColumn("ParentName", Type.GetType("System.String"));
            table.Columns.Add(parentNameColumn);

            parentGuidColumn = new DataColumn("ParentGuid", Type.GetType("System.String"));
            table.Columns.Add(parentGuidColumn);

            topGuidColumn = new DataColumn("CollectionGuid", Type.GetType("System.String"));
            table.Columns.Add(topGuidColumn);

            parentUrlColumn = new DataColumn("ParentUrl", Type.GetType("System.String"));
            table.Columns.Add(parentUrlColumn);

            topUrlColumn = new DataColumn("CollectionUrl", Type.GetType("System.String"));
            table.Columns.Add(topUrlColumn);

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

            row = table.NewRow();
            row[nameColumn] = this.SPWeb.Name;
            row[urlColumn] = this.SPWeb.Url;
            row[parentNameColumn] = this.SPWeb.ParentWeb.Name;
            row[parentGuidColumn] = this.SPWeb.ParentWeb.ID.ToString("D");
            row[topGuidColumn] = this.SPSite.ID.ToString("D");
            row[guidColumn] = this.SPWeb.ID.ToString("D");
            row[authorNameColumn] = this.SPWeb.Author.Name;
            row[authorEmailColumn] = this.SPWeb.Author.Email;
            row[authorLoginColumn] = this.SPWeb.Author.LoginName;
            row[descriptionColumn] = this.SPWeb.Description;
            row[titleColumn] = this.SPWeb.Title;

            table.Rows.Add(row);
            
            ((FormView)sender).DataSource = table;
        }

        protected void SubSiteGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Session["Web"] = ((GridView)sender).DataKeys[e.NewSelectedIndex].Values["Guid"];
            Response.Redirect(ResolveUrl("SubWeb.aspx"), true);
        }

        protected void DetailFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Session.Remove("Web");

                if (Convert.ToString(e.CommandArgument) == "Portal")
                {
                    Session.Remove("Site");
                    Response.Redirect(ResolveUrl("Portal.aspx"), true);
                    return;
                }

                Response.Redirect(ResolveUrl("SiteCollection.aspx"), true);
            }
        }
    }
}
