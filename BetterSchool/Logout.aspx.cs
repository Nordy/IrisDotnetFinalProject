using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterSchool
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLoggedIn"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            Session["username"] = null;
            Session["isAdmin"] = null;
            Session["fname"] = null;
            Session["lname"] = null;
            Session["isLoggedIn"] = null;
            Response.Redirect("Home.aspx");
        }
    }
}