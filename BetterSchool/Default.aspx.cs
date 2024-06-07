using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterSchool
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLoggedIn"] != null)
                Response.Redirect("Dashboard.aspx");
            else
                Response.Redirect("Home.aspx");
        }
    }
}