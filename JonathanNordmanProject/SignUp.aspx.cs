using JonathanNordmanProject.apis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JonathanNordmanProject
{
    public partial class SignUp : System.Web.UI.Page
    {
        public string navbar;
        public string title;
        public string classes;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.navbar = Components.Navbar((Session["username"] != null), "Signup", (Session["isAdmin"] != null));
            this.title = Components.Title();
            string selectSql = "SELECT DISTINCT class FROM Tschedule";
            DataTable table = MyAdoHelper.ExecuteDataTable("db.mdf", selectSql);
            List<string> list = new List<string>();

            for (int i = 0; i < table.Rows.Count;i++)
            {
                list.Add((string)(table.Rows[i][0]));
            }
            list.Sort(StringComparer.Create(CultureInfo.GetCultureInfo("he-IL"), false));
            classes = "";
            for (int i = 0; i< list.Count; i++)
            {
                classes += $@"<label for=""{i}"">{list[i]}<input type=""checkbox"" id=""{i}"" onchange=""checkboxStatusChange()"" value=""{list[i]}"" /></label>";
            }
        }

    }
}