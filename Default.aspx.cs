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
using System.Xml;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Portal;
using Microsoft.SharePoint.Portal.Topology;
using Microsoft.SharePoint.Portal.UserProfiles;

namespace PortalEnumerator
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ResultsGridView.DataBind();
        }

        protected void ResultsGridView_DataBinding(object sender, EventArgs e)
        {
            DataTable table;
            DataColumn virtualServerColumn, urlColumn;
            DataRow row;
            TopologyManager topologyManager;
            PortalSite portalSite;
            PortalContext portalContext;
            UserProfileManager upm;
            UserProfileConfigManager upcm;

            topologyManager = new TopologyManager();
            portalSite = topologyManager.PortalSites[new Uri("http://spsdev")];
            portalContext = PortalApplication.GetContext(portalSite);
            
            upm = new UserProfileManager(portalContext);
            upcm = new UserProfileConfigManager(portalContext);

            table = new DataTable();

            virtualServerColumn = new DataColumn("VirtualServer", Type.GetType("System.String"));
            table.Columns.Add(virtualServerColumn);

            urlColumn = new DataColumn("Url", Type.GetType("System.String"));
            table.Columns.Add(urlColumn);
            
            foreach (PortalSite site in topologyManager.PortalSites)
            {
                row = table.NewRow();

                row[virtualServerColumn] = site.VirtualServer.Name;
                row[urlColumn] = site.Url;

                table.Rows.Add(row);
            }

            ((GridView)sender).DataSource = table;
        }

        protected void ResultsGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            e.Cancel = true;

            Session["Portal"] = ((GridView)sender).DataKeys[e.NewSelectedIndex].Values["Url"];

            Response.Redirect(ResolveUrl("~/Portal.aspx"), true);
        }
    }
}
