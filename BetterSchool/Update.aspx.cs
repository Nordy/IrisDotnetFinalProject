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
    public partial class Update : System.Web.UI.Page
    {
        public string navbar;
        public string title;
        public string fname;
        public string lname;
        public string password;
        public string grade;
        public string mashovId;
        public string mashovPassword;
        public string classes;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["isLoggedIn"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            navbar = Components.Navbar((Session["isLoggedIn"] != null ? (bool)Session["isLoggedIn"] : false), "Profile", (Session["isAdmin"] != null ? (bool)Session["isAdmin"] : false), (Session["fname"] != null ? (string)Session["fname"] : null));
            title = Components.Title();
            fname = (string)Session["fname"];
            lname = (string)Session["lname"];
            string fileName = "db.mdf";
            string selectSql = $"SELECT * FROM Tusers WHERE username='{Session["username"]}'";
            DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            password = (string)table.Rows[0]["password"];
            grade = (string)table.Rows[0]["class"];
            mashovId = (table.Rows[0]["mashovId"] != DBNull.Value) ? (string)table.Rows[0]["mashovId"] : null;
            mashovPassword = (table.Rows[0]["mashovPassword"] != DBNull.Value) ? (string)table.Rows[0]["mashovPassword"] : null;
            string selectDistinctSql = "SELECT DISTINCT class FROM Tschedule";
            DataTable distinctTable = MyAdoHelper.ExecuteDataTable(fileName, selectDistinctSql);
            List<string> list = new List<string>();

            for (int i = 0; i < distinctTable.Rows.Count; i++)
            {
                list.Add((string)(distinctTable.Rows[i][0]));
            }
            list.Sort(StringComparer.Create(CultureInfo.GetCultureInfo("he-IL"), false));
            classes = "";
            for (int i = 0; i < list.Count; i++)
            {
                classes += $@"<label for=""{i}"">{list[i]}<input type=""checkbox"" id=""{i}"" onchange=""checkboxStatusChange()"" name=""class"" value=""{list[i]}"" /></label>";
            }

            if (Request.Form["Submit"] != null)
            {
                fname = (Check_Input(Request.Form["fname"])) ? Request.Form["fname"] : fname;
                lname = (Check_Input(Request.Form["lname"])) ? Request.Form["lname"] : lname;
                password = (Check_Input(Request.Form["password"])) ? Request.Form["password"] : password;
                grade = (Check_Input(Request.Form["class"])) ? Request.Form["class"] : grade;
                mashovId = (Check_Input(Request.Form["mashovId"])) ? Request.Form["mashovId"] : mashovId;
                mashovPassword = (Check_Input(Request.Form["mashovPassword"])) ? Request.Form["mashovPassword"] : mashovPassword;
                string sql = $"UPDATE Tusers SET password=N'{password}', fname=N'{fname}', lname=N'{lname}', class=N'{grade}', mashovId='{mashovId}', mashovPassword=N'{mashovPassword}' WHERE username=N'{Session["username"]}'";
                MyAdoHelper.DoQuery(fileName, sql);
                Session["fname"] = fname;
                Session["lname"] = lname;
                Response.Redirect("Dashboard.aspx");
            }

        }

        /// <summary>
        /// Checks if an input is valid (true = valid, false = invalid)
        /// </summary>
        protected bool Check_Input(string input)
        {
            if (input == null || input.Length < 1)
                return false;
            return true;
        }
    }
}