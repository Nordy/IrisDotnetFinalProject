using JonathanNordmanProject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

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
                UpdateChanges(grade);
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
        /// Gets information from the database
        /// </summary>
        public DataTable GetFromDatabase(string grade, int hour, int day, string teacher)
        {
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
            if (MyAdoHelper.IsExist(fileName, selectSql))
                return MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            return null;
        }
        /// <summary>
        /// Gets the changes and updates them
        /// </summary>
        public void UpdateChanges(string grade)
        {
            driver.Navigate().GoToUrl(url);
            ClickElement(By.LinkText("שינויים"));
            List<IWebElement> changeElements = new List<IWebElement>(driver.FindElements(By.ClassName("MsgCell")));
            foreach (IWebElement element in changeElements)
            {
                string text = element.Text;
                string date = text.Split(',')[0].Trim();
                int lesson = int.Parse(text.Split(',')[1].Replace("שיעור", "").Trim());
                string msg = text.Replace($"{text.Split(',')[0]},{text.Split(',')[1]}", "").Trim();
                DateTime datetime;
                if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime))
                {
                    int dayOfWeek = (int)datetime.DayOfWeek;


                    DateTime currentDate = DateTime.Now;

                    // Calculate the start of the current week (Sunday)
                    DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);

                    // Calculate the end of the current week (Saturday)
                    DateTime endOfWeek = startOfWeek.AddDays(6);

                    // Check if the given date is within this week and not in the past
                    bool isInThisWeekAndNotPast = datetime >= currentDate && datetime >= startOfWeek && datetime <= endOfWeek;
                    if (datetime >= currentDate && datetime >= startOfWeek && datetime <= endOfWeek)
                    {
                        string room = null;
                        string teacher;
                        int newLesson;
                        if (msg.Contains("ביטול שעור"))
                        {
                            teacher = msg.Split(',')[0].Trim();
                            CancelLesson(lesson, dayOfWeek, grade, teacher);
                        }

                        else if (msg.Contains("החלפת חדר לקבוצה"))
                        {
                            teacher = msg.Split(',')[1].Replace("החלפת חדר לקבוצה", "").Trim();
                            string[] roomSplits = msg.Split('-');
                            room = roomSplits[roomSplits.Length - 1].Replace("לחדר", "").Trim();
                            ChangeRoomOfLesson(lesson, dayOfWeek, grade, teacher, room);

                        }
                        else if (msg.Contains("הזזת שיעור"))
                        {
                            teacher = msg.Split(':')[1].Split(',')[1];
                            newLesson = int.Parse(msg.Split(',')[2].Split(new string[] {"לשיעור"}, StringSplitOptions.None)[1]);
                            MoveLessonToDifferentTime(lesson, newLesson, dayOfWeek, grade, teacher);
                        }

                    }
                }
            }
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
                    try
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

                            AddLesson(subject, room, teacher, i - 1, j, grade);

                            /*
                            Console.OutputEncoding = Encoding.UTF8;
                            Console.WriteLine($"Hour {i - 1} Day {j}");
                            Console.WriteLine($"subject: {subject}");
                            Console.WriteLine($"room: {room}");
                            Console.WriteLine($"teacher: {teacher}");
                            Console.WriteLine();
                            */


                        }
                    } catch { }
                    
                }
            }

        }
        /// <summary>
        /// Moves a lesson to a different time (with status 3 for the removed lesson, and 4 for the added one)
        /// </summary>
        public void MoveLessonToDifferentTime(int hour, int newHour, int day, string grade, string teacher)
        {
            EditLesson(hour, day, grade, teacher, 3); // removed lesson
            CancelLesson(newHour, day, grade, teacher);
            string room = GetFromDatabase(grade, hour, day, teacher).Rows[0]["room"].ToString();
            EditLesson(newHour, day, grade, teacher, 4, room); // added lesson

        }
        /// <summary>
        /// Cancels a lesson (with status 1)
        /// </summary>
        public void CancelLesson(int hour, int day, string grade, string teacher)
        {
            EditLesson(hour, day, grade, teacher, 1);
        }

        public void ChangeRoomOfLesson(int hour, int day, string grade, string teacher, string room)
        {
            EditLesson(hour, day, grade, teacher, 2, room);
        }
        /// <summary>
        /// Edits lessons
        /// </summary>
        /// <param name="operation">1: Cancel, 2: Change room, 3: Move to different time, 4: Added lesson (time moved)</param>
        public void EditLesson(int hour, int day, string grade, string teacher, int operation)
        {
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
            string sql;
            var room = GetFromDatabase(grade, hour, day, teacher).Rows[0]["room"];
            if (MyAdoHelper.IsExist(fileName, selectSql))
            {
                sql = $"UPDATE Tschedule SET operation='{operation}' and room=N'{room}' WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
                MyAdoHelper.DoQuery(fileName, sql);
            }
            else
            {
                var subject = GetFromDatabase(grade, hour, day, teacher).Rows[0]["subject"];
                sql = $"INSERT INTO Tschedule(subject, teacher, room, class, hour, day, operation) VALUES(N'{subject}', N'{teacher}', N'{room}', N'{grade}', '{hour}', '{day}', '{operation}'";
            }
        }
        /// <summary>
        /// Edits lessons
        /// </summary>
        /// <param name="operation">1: Cancel, 2: Change room, 3: Move to different time, 4: Added lesson (time moved)</param>
        public void EditLesson(int hour, int day, string grade, string teacher, int operation, string room)
        {
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
            string sql;
            if (MyAdoHelper.IsExist(fileName, selectSql))
            {
                sql = $"UPDATE Tschedule SET operation='{operation}' and room=N'{room}' WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
                MyAdoHelper.DoQuery(fileName, sql);
            }
            else
            {
                var subject = GetFromDatabase(grade, hour, day, teacher).Rows[0]["subject"];
                sql = $"INSERT INTO Tschedule(subject, teacher, room, class, hour, day, operation) VALUES(N'{subject}', N'{teacher}', N'{room}', N'{grade}', '{hour}', '{day}', '{operation}'";
            }
        }
        /// <summary>
        /// Either adds or updates an existing lesson
        /// </summary>
        public void AddLesson(string subject, string room, string teacher, int hour, int day, string grade)
        {
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' hour={hour} and day={day}";
            string sql;
            if (!MyAdoHelper.IsExist(fileName, selectSql))
            {
                sql = $"INSERT INTO Tschedule(subject, teacher, room, class, hour, day, operation) VALUES(N'{subject}', N'{teacher}', N'{room}', N'{grade}', '{hour}', '{day}', '0'";
                MyAdoHelper.DoQuery(fileName, sql);
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