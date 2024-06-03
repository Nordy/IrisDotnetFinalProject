using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JonathanNordmanProject.apis;

namespace JonathanNordmanProject
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void UpdateShahafSchedules(object sender, EventArgs e)
        {
            ShahafApi.UpdateSchedules();
        }


    }
}