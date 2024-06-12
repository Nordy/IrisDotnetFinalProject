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
        public bool status;
        public string error;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLoggedIn"] != null)
            {
                Response.Redirect("Profile.aspx");
                return;
            }
            navbar = Components.Navbar((Session["isLoggedIn"] != null ? (bool)Session["isLoggedIn"] : false), "Signup", (Session["isAdmin"] != null ? (bool)Session["isAdmin"] : false), (Session["fname"] != null ? (string)Session["fname"] : null));
            title = Components.Title();
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

            status = true;
            error = "";

            if (Request.Form["submit"] != null)
            {
                status = true;
                error = "";
                string fname = Request.Form["fname"];
                string lname = Request.Form["lname"];
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                string grade = Request.Form["class"];
                string mashovId = Request.Form["mashovId"];
                string mashovPassword = Request.Form["mashovPassword"];
                bool isAdmin = false;
                if (!Check_Input(username) || !Check_Input(password) || !Check_Input(fname) || !Check_Input(lname) || !Check_Input(grade))
                {
                    status = false;
                    error = "Incorrect Format!";
                    return;

                }
                string selectSql = $"SELECT * FROM Tusers WHERE username='{username}'";
                if (!MyAdoHelper.IsExist(fileName, selectSql))
                {
                    string sql = $"INSERT INTO Tusers(username, password, fname, lname, class, isAdmin, mashovId, mashovPassword) VALUES(N'{username}', N'{password}', N'{fname}', N'{lname}', N'{grade}', '{isAdmin}', '{mashovId}', N'{mashovPassword}')";
                    MyAdoHelper.DoQuery(fileName, sql);
                    Session["username"] = username;
                    Session["isAdmin"] = isAdmin;
                    Session["fname"] = fname;
                    Session["lname"] = lname;
                    Session["isLoggedIn"] = true;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    status = false;
                    error = "Username already exists!";
                }
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