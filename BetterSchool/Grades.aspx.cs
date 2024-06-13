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
    public partial class Grades : System.Web.UI.Page
    {
        public string navbar;
        public string title;
        public List<Dictionary<string, string>> grades;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLoggedIn"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            else if (MashovApi.GetID((string)Session["username"]) == null || MashovApi.GetPassword((string)Session["username"]) == null)
            {
                Response.Redirect("Update.aspx");
                return;
            }
            navbar = Components.Navbar((Session["isLoggedIn"] != null ? (bool)Session["isLoggedIn"] : false), "Grades", (Session["isAdmin"] != null ? (bool)Session["isAdmin"] : false), (Session["fname"] != null ? (string)Session["fname"] : null));
            title = Components.Title();
            grades = new List<Dictionary<string, string>>();
            string selectSql = $"SELECT * FROM Tgrades WHERE username=N'{(string)Session["username"]}'";
            string fileName = "db.mdf";
            DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                grades.Add(new Dictionary<string, string>()
                {
                    { "teacher", (string)table.Rows[i]["teacher"] },
                    { "subject", (string)table.Rows[i]["subject"] },
                    { "grade", (string)table.Rows[i]["grade"] },
                    { "date", (string)table.Rows[i]["date"] },
                    { "task", (string)table.Rows[i]["task"] }
                });
            }
        }
    }
}