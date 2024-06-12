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
    public partial class Admin : System.Web.UI.Page
    {
        public string navbar;
        public string title;
        public string users;
        public string classes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isAdmin"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            navbar = Components.Navbar((Session["isLoggedIn"] != null ? (bool)Session["isLoggedIn"] : false), "Admin", (Session["isAdmin"] != null ? (bool)Session["isAdmin"] : false), (Session["fname"] != null ? (string)Session["fname"] : null));
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
            string selectSql = "SELECT * FROM Tusers";
            table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            users = "";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                users += $@"
                    <tr>
                        <form method=""post"" action=""Admin.aspx"">
                            <td><input type=""text"" name=""username"" value=""{table.Rows[i]["username"]}"" readonly/></td>
                            <td><input type=""text"" name=""password"" value=""{table.Rows[i]["password"]}"" /></td>
                            <td><input type=""text"" name=""mashovId"" value=""{table.Rows[i]["mashovId"]}"" /></td>
                            <td><input type=""text"" name=""mashovPassword"" value=""{table.Rows[i]["mashovPassword"]}"" /></td>
                            <td><input type=""text"" name=""fname"" value=""{table.Rows[i]["fname"]}"" /></td>
                            <td><input type=""text"" name=""lname"" value=""{table.Rows[i]["lname"]}"" /></td>
                            <td><input type=""text"" name=""class"" value=""{table.Rows[i]["class"]}"" /></td>
                            <td><input type=""checkbox"" name=""isAdmin"" value=""true"" {((bool)table.Rows[i]["isAdmin"] == true ? "checked" : "")}/></td>
                            <td><input type=""submit"" class=""updateButton"" name=""submit"" value=""Update"" /></td>
                            <td><input type=""submit"" class=""deleteButton"" name=""submit"" value=""Delete"" /></td>
                        </form>
                    </tr>
                ";
            }
            if (Request.Form["submit"] == "Update")
            {
                string fname = Request.Form["fname"];
                string lname = Request.Form["lname"];
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                string grade = Request.Form["class"];
                string mashovId = Request.Form["mashovId"];
                string mashovPassword = Request.Form["mashovPassword"];
                bool isAdmin = (Request.Form["isAdmin"] == "true") ? true : false;
                string sql = $"UPDATE Tusers SET password=N'{password}', fname=N'{fname}', lname=N'{lname}', class=N'{grade}', mashovId='{mashovId}', mashovPassword=N'{mashovPassword}' WHERE username=N'{username}'";
                MyAdoHelper.DoQuery(fileName, sql);
                Response.Redirect("Admin.aspx");
            }
            if (Request.Form["submit"] == "Delete")
            {
                string username = Request.Form["username"];
                string sql = $"DELETE FROM Tusers WHERE username=N'{username}'";
                MyAdoHelper.DoQuery(fileName, sql);
                Response.Redirect("Admin.aspx");
            }
            if (Request.Form["submit"] == "Search fname")
            {
                selectSql = $"SELECT * FROM Tusers WHERE fname=N'{Request.Form["fname"]}'";
                table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
                users = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    users += $@"
                    <tr>
                        <form method=""post"" action=""Admin.aspx"">
                            <td><input type=""text"" name=""username"" value=""{table.Rows[i]["username"]}"" readonly/></td>
                            <td><input type=""text"" name=""password"" value=""{table.Rows[i]["password"]}"" /></td>
                            <td><input type=""text"" name=""mashovId"" value=""{table.Rows[i]["mashovId"]}"" /></td>
                            <td><input type=""text"" name=""mashovPassword"" value=""{table.Rows[i]["mashovPassword"]}"" /></td>
                            <td><input type=""text"" name=""fname"" value=""{table.Rows[i]["fname"]}"" /></td>
                            <td><input type=""text"" name=""lname"" value=""{table.Rows[i]["lname"]}"" /></td>
                            <td><input type=""text"" name=""class"" value=""{table.Rows[i]["class"]}"" /></td>
                            <td><input type=""checkbox"" name=""isAdmin"" value=""true"" {((bool)table.Rows[i]["isAdmin"] == true ? "checked" : "")}/></td>
                            <td><input type=""submit"" class=""updateButton"" name=""submit"" value=""Update"" /></td>
                            <td><input type=""submit"" class=""deleteButton"" name=""submit"" value=""Delete"" /></td>
                        </form>
                    </tr>
                ";
                }
            }
            if (Request.Form["submit"] == "Search class")
            {
                selectSql = $"SELECT * FROM Tusers WHERE class=N'{Request.Form["class"]}'";
                table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
                users = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    users += $@"
                    <tr>
                        <form method=""post"" action=""Admin.aspx"">
                            <td><input type=""text"" name=""username"" value=""{table.Rows[i]["username"]}"" readonly/></td>
                            <td><input type=""text"" name=""password"" value=""{table.Rows[i]["password"]}"" /></td>
                            <td><input type=""text"" name=""mashovId"" value=""{table.Rows[i]["mashovId"]}"" /></td>
                            <td><input type=""text"" name=""mashovPassword"" value=""{table.Rows[i]["mashovPassword"]}"" /></td>
                            <td><input type=""text"" name=""fname"" value=""{table.Rows[i]["fname"]}"" /></td>
                            <td><input type=""text"" name=""lname"" value=""{table.Rows[i]["lname"]}"" /></td>
                            <td><input type=""text"" name=""class"" value=""{table.Rows[i]["class"]}"" /></td>
                            <td><input type=""checkbox"" name=""isAdmin"" value=""true"" {((bool)table.Rows[i]["isAdmin"] == true ? "checked" : "")}/></td>
                            <td><input type=""submit"" class=""updateButton"" name=""submit"" value=""Update"" /></td>
                            <td><input type=""submit"" class=""deleteButton"" name=""submit"" value=""Delete"" /></td>
                        </form>
                    </tr>
                ";
                }
            }
            if (Request.Form["submit"] == "Clear Search")
            {
                Response.Redirect("Admin.aspx");
            }

        }
        protected void UpdateSchedules(object sender, EventArgs e)
        {
            ShahafApi.UpdateSchedules();
        }
        protected void CleanChanges(object sender, EventArgs e)
        {
            ShahafApi.CleanChanges();        
        }

        protected void UpdateChanges(object sender, EventArgs e)
        {
            ShahafApi.UpdateChanges();
        }
    }
}