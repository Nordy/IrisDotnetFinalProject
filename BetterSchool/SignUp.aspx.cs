using BetterSchool.apis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterSchool
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
            string fileName = "db.mdf";
            string selectDistinctSql = "SELECT DISTINCT class FROM Tschedule";
            DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectDistinctSql);
            List<string> list = new List<string>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add((string)(table.Rows[i][0]));
            }
            list.Sort(StringComparer.Create(CultureInfo.GetCultureInfo("he-IL"), false));
            classes = "";
            for (int i = 0; i < list.Count; i++)
            {
                classes += $@"<label for=""{i}"">{list[i]}<input type=""checkbox"" id=""{i}"" onchange=""checkboxStatusChange()"" name=""class"" value=""{list[i]}"" /></label>";
            }

            if (Request.Form["submit"] != null)
            {
                string fname = Request.Form["fname"];
                string lname = Request.Form["lname"];
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                string grade = Request.Form["class"];

            }
        }

    }
}