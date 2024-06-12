using BetterSchool.apis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterSchool
{
    public partial class Schedule : System.Web.UI.Page
    {
        public string navbar;
        public string title;
        public string classes;
        public string[,] schedule;
        public bool showSchedule;
        public string[] times;
        public string currentGrade;
        protected void Page_Load(object sender, EventArgs e)
        {
            navbar = Components.Navbar((Session["isLoggedIn"] != null ? (bool)Session["isLoggedIn"] : false), "Schedule", (Session["isAdmin"] != null ? (bool)Session["isAdmin"] : false), (Session["fname"] != null ? (string)Session["fname"] : null));
            title = Components.Title();
            times = new string[]{ "7:45 - 8:30", "8:30 - 9:15", "9:15 - 10:00",
            "10:20 - 11:05", "11:05 - 11:50", "12:05 - 12:50", "12:50 - 13:35", 
            "13:55 - 14:40", "14:45 - 15:30", "15:35 - 16:20", "16:20 - 17:05", 
            "17:15 - 18:00", "18:05 - 18:50"};
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
            if (Session["isLoggedIn"] != null)
            {
                string selectSql = $"SELECT * FROM Tusers WHERE username='{Session["username"]}'";
                table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
                currentGrade = (string)table.Rows[0]["class"];
                string[] gradesUnfiltered = currentGrade.Split(',');
                string[] grades = new string[gradesUnfiltered.Length];
                for (int i = 0; i < gradesUnfiltered.Length; i++)
                {
                    grades[i] = gradesUnfiltered[i].Trim();
                }
                schedule = GetSchedule(grades);
                showSchedule = true;
            } else
            {
                showSchedule = false;
            }
            
            if (Request.Form["submit"] != null)
            {
                currentGrade = Request.Form["class"];
                string[] gradesUnfiltered = currentGrade.Split(',');
                string[] grades = new string[gradesUnfiltered.Length];
                for (int i = 0; i < gradesUnfiltered.Length; i++)
                {
                    grades[i] = gradesUnfiltered[i].Trim();
                }
                schedule = GetSchedule(grades);
                showSchedule = true;
            }
        }
        protected string[,] EmptySchedule()
        {
            string[,] schedule = new string[7, 13];
            for (int i = 1;i<7;i++)
            {
                for (int j = 0;j<13;j++)
                {
                    schedule[i,j] = "<div class=\"lesson\">";
                }
            }
            return schedule;
        }
        protected string[,] GetSchedule(string[] grades)
        {
            string fileName = "db.mdf";
            string selectSql;
            string[,] lessons = EmptySchedule();
            foreach (string grade in grades)
            {
                for (int day = 1; day < 7; day++)
                {
                    for (int hour = 0; hour < 13; hour++)
                    {
                        selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and day='{day}' and hour='{hour}'";
                        DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            lessons[day, hour] += $@"
                                <div class=""subject {((int)table.Rows[i]["operation"] == 1 ? "subjectCancel"
                                : (int)table.Rows[i]["operation"] == 3 ? "subjectMove"
                                : (int)table.Rows[i]["operation"] == 4 ? "subjectAdd" : "")}"">
                                    <h3>{table.Rows[i]["subject"]} {table.Rows[i]["operation"]}</h3>
                                    <p>{(table.Rows[i]["teacher"] != DBNull.Value ? table.Rows[i]["teacher"] : "")}</p>
                                    <p>{(table.Rows[i]["teacher"] != DBNull.Value && table.Rows[i]["room"] != DBNull.Value ? "•" : "")}</p>
                                    <p>{(table.Rows[i]["room"] != DBNull.Value ? table.Rows[i]["room"] : "")}</p>
                                </div>
                            ";
                        }
                        lessons[day, hour] += "</div>";
                        Thread.Sleep(30);
                    }
                }
            }

            

            return lessons;

        }
    }
}