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
using System.Xml;
using Microsoft.SharePoint.Portal;
using Microsoft.SharePoint.Portal.Topology;

namespace PortalEnumerator
{
    public partial class Portal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PortalSitesGridView.DataBind();
        }

        protected void PortalSitesGridView_DataBinding(object sender, EventArgs e)
        {
            DataTable table;
            DataColumn urlColumn, guidColumn;
            DataRow row;
            TopologyManager topologyManager;
            PortalSite portalSite;
            PortalContext portalContext;

            topologyManager = new TopologyManager();
            portalSite = topologyManager.PortalSites[new Uri(Convert.ToString(Session["Portal"]))];
            portalContext = PortalApplication.GetContext(portalSite);
            
            table = new DataTable();

            urlColumn = new DataColumn("Url", Type.GetType("System.String"));
            table.Columns.Add(urlColumn);

            guidColumn = new DataColumn("Guid", Type.GetType("System.String"));
            table.Columns.Add(guidColumn);

            foreach (TeamSite site in topologyManager.TeamSites)
            {
                if (site.UrlPath == "/")
                    continue;

                row = table.NewRow();

                row[urlColumn] = Convert.ToString(Session["Portal"]) + site.UrlPath.Substring(1);
                row[guidColumn] = site.ID.ToString("D");

                table.Rows.Add(row);
            }

            ((GridView)sender).DataSource = table;
        }

        protected void PortalSitesGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            e.Cancel = true;

            Session["Site"] = ((GridView)sender).DataKeys[e.NewSelectedIndex].Values["Guid"];
            Session.Remove("Edit");
            Response.Redirect(ResolveUrl("~/SiteCollection.aspx"), true);
        }
    }
}
