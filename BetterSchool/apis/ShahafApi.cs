using BetterSchool.apis;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace BetterSchool.apis
{

    public static class ShahafApi
    {

        private static string url = "https://alon.iscool.co.il/default.aspx";
        private static string fileName = "db.mdf";
        private static string path = HttpContext.Current.Server.MapPath("drivers");
        private static ChromeOptions options = new ChromeOptions();
        private static IWebDriver driver;
        private static WebDriverWait wait;
        private static bool status = false;

        /// <summary>
        /// Gets the status of the database (false = unoccupied, true = occupied)
        /// </summary>
        public static bool GetStatus()
        {
            return status;
        }
        /// <summary>
        /// Loops through the classes and updates each schedule
        /// </summary>
        public static void UpdateSchedules()
        {

            Console.OutputEncoding = Encoding.UTF8;
            List<string> classes = GetClasses();
            options.AddArgument("--headless");
            driver = new ChromeDriver(path, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            status = true;
            foreach (string grade in classes)
            {
                Console.WriteLine($"Current grade: {grade}");
                UpdateSchedules(grade);
                Thread.Sleep(1000);
            }
            driver.Close();
            status = false;

        }
        /// <summary>
        /// Loops through the classes and updates each change
        /// </summary>
        public static void UpdateChanges()
        {

            List<string> classes = GetClasses();
            options.AddArgument("--headless");
            driver = new ChromeDriver(path, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            status = true;
            foreach (string grade in classes)
            {
                Console.WriteLine($"Current grade: {grade}");
                UpdateChanges(grade);
                Thread.Sleep(1000);
            }
            driver.Close();
            status = false;
        }

        /// <summary>
        /// Lists all the available classes in shahaf's database
        /// </summary>
        public static List<string> GetClasses()
        {
            options.AddArgument("--headless");
            driver = new ChromeDriver(path, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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
            driver.Close();
            return classes;
        }
        /// <summary>
        /// Gets information from the database
        /// </summary>
        public static DataTable GetFromDatabase(string grade, int hour, int day, string teacher)
        {
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' and hour='{hour}' and day='{day}'";
            if (MyAdoHelper.IsExist(fileName, selectSql))
                return MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            return null;
        }

        public static void CleanChanges()
        {
            string sql = $"DELETE FROM Tschedule WHERE operation!='0'";
            MyAdoHelper.DoQuery(fileName, sql);
        }

        /// <summary>
        /// Gets the changes and updates them
        /// </summary>
        public static void UpdateChanges(string grade)
        {
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
                        dayOfWeek += 1;
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
                            newLesson = int.Parse(msg.Split(',')[2].Split(new string[] { "לשיעור" }, StringSplitOptions.None)[1]);
                            MoveLessonToDifferentTime(lesson, newLesson, dayOfWeek, grade, teacher);
                        }

                    }
                }
            }
        }
        /// <summary>
        /// Gets the schedule of a class 
        /// </summary>
        public static void UpdateSchedules(string grade)
        {
            ClickElement(By.LinkText("מערכת שעות"));
            IWebElement dropdownTd = driver.FindElement(By.Id("dnn_ctr1214_TimeTableView_TdClassesList"));
            IWebElement dropdown = dropdownTd.FindElement(By.TagName("select"));
            SelectElement select = new SelectElement(dropdown);

            select.SelectByText(grade.Replace("\'\'", "\'"));
            IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("TTTable")));
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
                            string teacher;
                            try
                            {
                                subject = (lesson.Text.Length > 1) ? lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[0].Split('(')[0].Trim() : "";
                                room = (lesson.Text.Length > 1) ? lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[0].Split('(')[1].Replace(")", "").Trim() : "";

                            }
                            catch (Exception err)

                            {
                                subject = (lesson.Text.Length > 1) ? lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[0].Trim() : "";
                                room = null;
                            }
                            try
                            {
                                teacher = lesson.Text.Split(new string[] { "\n" }, StringSplitOptions.None)[1].Trim();
                            }
                            catch (Exception err)
                            {
                                teacher = "";

                            }
                            if ((subject.Length > 1) && (teacher.Length > 1))
                            {
                                AddLesson(subject, room, teacher, i - 1, j, grade);
                            }


                            /*
                            Console.WriteLine($"Hour {i - 1} Day {j}");
                            Console.WriteLine($"subject: {subject}");
                            Console.WriteLine($"room: {room}");
                            Console.WriteLine($"teacher: {teacher}");
                            Console.WriteLine();
                            */


                        }
                    }
                    catch (IOException exception)
                    {
                    }

                }
            }

        }
        /// <summary>
        /// Moves a lesson to a different time (with status 3 for the removed lesson, and 4 for the added one)
        /// </summary>
        public static void MoveLessonToDifferentTime(int hour, int newHour, int day, string grade, string teacher)
        {
            EditLesson(hour, day, grade, teacher, 3); // removed lesson
            CancelLessons(newHour, day, grade);
            string room = GetFromDatabase(grade, hour, day, teacher).Rows[0]["room"].ToString();
            EditLesson(newHour, day, grade, teacher, 4, room); // added lesson

        }
        /// <summary>
        /// Cancels a lesson (with status 1)
        /// </summary>
        public static void CancelLesson(int hour, int day, string grade, string teacher)
        {
            EditLesson(hour, day, grade, teacher, 1);
        }
        /// <summary>
        /// Cancels a lesson (with status 1)
        /// </summary>
        public static void CancelLessons(int hour, int day, string grade)
        {
            EditLessons(hour, day, grade, 1);
        }

        public static void ChangeRoomOfLesson(int hour, int day, string grade, string teacher, string room)
        {
            EditLesson(hour, day, grade, teacher, 2, room);
        }
        /// <summary>
        /// Edits lessons
        /// </summary>
        /// <param name="operation">1: Cancel, 2: Change room, 3: Move to different time, 4: Added lesson (time moved)</param>
        public static void EditLesson(int hour, int day, string grade, string teacher, int operation)
        {
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
            string sql;

            var room = GetFromDatabase(grade, hour, day, teacher).Rows[0]["room"];
            teacher = teacher.Replace("\'", "\'\'");
            grade = grade.Replace("\'", "\'\'");

            if (MyAdoHelper.IsExist(fileName, selectSql))
            {
                sql = $"UPDATE Tschedule SET operation='{operation}', room=N'{room}' WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
            }
            else
            {
                var subject = GetFromDatabase(grade, hour, day, teacher).Rows[0]["subject"];
                sql = $"INSERT INTO Tschedule(subject, teacher, room, class, hour, day, operation) VALUES(N'{subject}', N'{teacher}', N'{room}', N'{grade}', '{hour}', '{day}', '{operation}')";
            }
            MyAdoHelper.DoQuery(fileName, sql);

        }
        /// <summary>
        /// Edits lessons
        /// </summary>
        /// <param name="operation">1: Cancel, 2: Change room, 3: Move to different time, 4: Added lesson (time moved)</param>
        public static void EditLesson(int hour, int day, string grade, string teacher, int operation, string room)
        {

            room = (room != null) ? room.Replace("\'", "\'\'") : null;
            teacher = teacher.Replace("\'", "\'\'");
            grade = grade.Replace("\'", "\'\'");
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";
            string sql;
            if (MyAdoHelper.IsExist(fileName, selectSql))
            {
                sql = $"UPDATE Tschedule SET operation='{operation}', room=N'{room}' WHERE class=N'{grade}' and teacher=N'{teacher}' and hour={hour} and day={day}";

            }
            else
            {
                var subject = GetFromDatabase(grade, hour, day, teacher).Rows[0]["subject"];
                sql = $"INSERT INTO Tschedule(subject, teacher, room, class, hour, day, operation) VALUES(N'{subject}', N'{teacher}', N'{room}', N'{grade}', '{hour}', '{day}', '{operation}')";
            }
            MyAdoHelper.DoQuery(fileName, sql);

        }
        /// <summary>
        /// Edits lessons
        /// </summary>
        /// <param name="operation">1: Cancel, 2: Change room, 3: Move to different time, 4: Added lesson (time moved)</param>
        public static void EditLessons(int hour, int day, string grade, int operation)
        {

            grade = grade.Replace("\'", "\'\'");
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and hour={hour} and day={day}";
            string sql;
            if (MyAdoHelper.IsExist(fileName, selectSql))
            {
                sql = $"UPDATE Tschedule SET operation='{operation}' WHERE class=N'{grade}' and hour={hour} and day={day}";
                MyAdoHelper.DoQuery(fileName, sql);
            }

        }
        /// <summary>
        /// Either adds or updates an existing lesson
        /// </summary>
        public static void AddLesson(string subject, string room, string teacher, int hour, int day, string grade)
        {

            room = (room != null) ? room.Replace("\'", "\'\'") : null;
            teacher = teacher.Replace("\'", "\'\'");
            grade = grade.Replace("\'", "\'\'");
            subject = subject.Replace("\'", "\'\'");
            string selectSql = $"SELECT * FROM Tschedule WHERE class=N'{grade}' and teacher=N'{teacher}' and hour='{hour}' and day='{day}'";
            string sql;
            if (!MyAdoHelper.IsExist(fileName, selectSql))
            {

                sql = $"INSERT INTO Tschedule(subject, teacher, room, class, hour, day, operation) VALUES(N'{subject}', N'{teacher}', N'{room}', N'{grade}', '{hour}', '{day}', '0')";
                MyAdoHelper.DoQuery(fileName, sql);

            }


        }


        /// <summary>
        /// Finds the element and clicks on it
        /// </summary>
        public static void ClickElement(By by)
        {
            driver.FindElement(by).Click();
        }

        /// <summary>
        /// Executes a javascript script on the current site
        /// </summary>
        public static object ExecuteScript(string script)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            return jsExecutor.ExecuteScript(script);
        }




    }
}