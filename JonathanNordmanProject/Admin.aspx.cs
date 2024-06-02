using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JonathanNordmanProject
{
    public partial class Admin : System.Web.UI.Page
    {
        public ShahafServer shahafServer;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.shahafServer = new ShahafServer();
        }

        protected void UpdateShahafSchedules(object sender, EventArgs e)
        {
            shahafServer.UpdateSchedules();
        }


    }
}