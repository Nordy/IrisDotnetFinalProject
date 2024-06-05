using BetterSchool.apis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterSchool
{
    public partial class Login : System.Web.UI.Page
    {
        protected string navbar;
        public string title;
        public bool status;
        public string error;
        protected void Page_Load(object sender, EventArgs e)
        {
            navbar = Components.Navbar((Session["isLoggedIn"] != null ? (bool)Session["isLoggedIn"] : false), "Login", (Session["isAdmin"] != null ? (bool)Session["isAdmin"] : false), (Session["fname"] != null ? (string)Session["fname"] : null));
            title = Components.Title();
            status = true;
            error = "";

            if (Request.Form["Submit"] != null)
            {
                status = true;
                error = "";
                string username = (string)Request.Form["username"];
                string password = (string)Request.Form["password"];
                if (!Check_Input(username) || !Check_Input(password))
                {
                    status = false;
                    error = "Incorrect Username or Password!";
                    return;
                }


                string fileName = "db.mdf";
                string selectSql = $"SELECT * FROM Tusers WHERE username='{username}' and password='{password}'";
                if (MyAdoHelper.IsExist(fileName, selectSql))
                {
                    DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
                    Session["username"] = table.Rows[0]["username"];
                    Session["isAdmin"] = table.Rows[0]["isAdmin"];
                    Session["fname"] = table.Rows[0]["fname"];
                    Session["lname"] = table.Rows[0]["lname"];
                    Session["isLoggedIn"] = true;
                    Response.Redirect("Dashboard.aspx");
                    return;

                } else
                {
                    status = false;
                    error = "Incorrect Username or Password!";
                    return;
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