using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterSchool
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public string navbar;
        public string title;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLoggedIn"] == null)
            {
                Response.Redirect("Home.aspx");
                return;
            }
            navbar = Components.Navbar((Session["isLoggedIn"] != null ? (bool)Session["isLoggedIn"] : false), "Dashboard", (Session["isAdmin"] != null ? (bool)Session["isAdmin"] : false), (Session["fname"] != null ? (string)Session["fname"] : null));
            title = Components.Title();
        }
    }
}