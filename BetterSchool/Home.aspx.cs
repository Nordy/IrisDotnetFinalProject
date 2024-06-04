using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterSchool
{
    public partial class Home : System.Web.UI.Page
    {
        public string navbar;
        public string title;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.navbar = Components.Navbar((Session["username"] != null), "Home", (Session["isAdmin"] != null));
            this.title = Components.Title();
        }
    }
}