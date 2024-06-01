using JonathanNordmanProject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace JonathanNordmanProject
{

    public class ShahafServer
    {

        private IWebDriver driver;
        private string url;
        private string fileName;

        public ShahafServer()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            this.driver = new ChromeDriver($"C:\\Users\\Yonatan\\OneDrive\\Desktop\\f\\Coding\\C#\\Yud Aleph 2023-2024\\Web\\L2\\testapi\\testapi\\drivers\\", options);
            this.url = "https://alon.iscool.co.il/default.aspx";
            this.fileName = "db.mdf";
        }


        public void LoopThroughLessons()
        {
            List<string> classes = GetClasses();
            foreach (string grade in classes)
            {
                UpdateSchedule(grade);
            }
        }
        /// <summary>
        /// Lists all the available classes in shahaf's database
        /// </summary>
        public List<string> GetClasses()
        {
            driver.Navigate().GoToUrl(url);
            ClickElement(By.LinkText("מערכת שעות"));
            IWebElement dropdownTd = driver.FindElement(By.Id("dnn_ctr1214_TimeTableView_TdClassesList"));
            IWebElement dropdown = dropdownTd.FindElement(By.TagName("select"));
            List<IWebElement> list = new List<IWebElement>(dropdown.FindElements(By.TagName("option")));
            List<string> classes = new List<string>();
            foreach (var element in list)
            {
                classes.Add(element.Text);
            }
            return classes;
        }
        /// <summary>
        /// Either adds or updates an existing lesson
        /// </summary>
        public void UpdateDatabase(string subject, string room, string teacher, int hour, int day, string grade)
        {
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and hour={hour} and day={day}";
            string sql;
            if (MyAdoHelper.IsExist(fileName, selectSql))
            {
                sql = $"UPDATE Tschedule SET teacher=N'{teacher}' and subject=N'{subject}' and room=N'{room} WHERE class=N'{grade}' and hour='{hour}' and day='{day}'";
            } else
            {
                sql = $"INSERT INTO Tschedule(subject, teacher, room, class, hour, day) VALUES(N'{subject}', N'{teacher}', N'{room}', N'{grade}', '{hour}', '{day}'";
            }
            MyAdoHelper.DoQuery(fileName, sql);

        }

        /// <summary>
        /// Gets the schedule of a class 
        /// </summary>
        public void UpdateSchedule(string grade)
        {
            driver.Navigate().GoToUrl(url);
            ClickElement(By.LinkText("מערכת שעות"));
            IWebElement dropdownTd = driver.FindElement(By.Id("dnn_ctr1214_TimeTableView_TdClassesList"));
            IWebElement dropdown = dropdownTd.FindElement(By.TagName("select"));
            SelectElement select = new SelectElement(dropdown);
            select.SelectByText(grade);
            IWebElement table = driver.FindElement(By.ClassName("TTTable"));
            List<IWebElement> rows = new List<IWebElement>(table.FindElements(By.TagName("tr")));
            for (int i = 1; i < rows.Count; i++)
            {
                List<IWebElement> cells = new List<IWebElement>(rows[i].FindElements(By.TagName("td")));
                for (int j = 1; j < cells.Count; j++)
                {
                    List<IWebElement> lessonsPerHour = new List<IWebElement>(cells[j].FindElements(By.ClassName("TTLesson")));
                    foreach (var lesson in lessonsPerHour)
                    {
                        string subject;
                        string room;
                        try
                        {
                            subject = lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[0].Split('(')[0].Trim();
                            room = lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[0].Split('(')[1].Replace(")", "").Trim();

                        }
                        catch
                        {
                            subject = lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[0].Trim();
                            room = null;
                        }
                        string teacher = lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[1].Trim();

                        UpdateDatabase(subject, room, teacher, i - 1, j, grade);

                        /*
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine($"Hour {i - 1} Day {j}");
                        Console.WriteLine($"subject: {subject}");
                        Console.WriteLine($"room: {room}");
                        Console.WriteLine($"teacher: {teacher}");
                        Console.WriteLine();
                        */


                    }

                }
            }

        }

        /// <summary>
        /// Finds the element and clicks on it
        /// </summary>
        public void ClickElement(By by)
        {
            this.driver.FindElement(by).Click();
        }

        /// <summary>
        /// Executes a javascript script on the current site
        /// </summary>
        public object ExecuteScript(string script)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            return jsExecutor.ExecuteScript(script);
        }




    }
}