using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading;

namespace BetterSchool.apis
{
    public static class MashovApi
    {
        private static string url = "https://web.mashov.info/students/login";
        private static string fileName = "db.mdf";
        private static string path = HttpContext.Current.Server.MapPath("drivers");
        private static ChromeOptions options = new ChromeOptions();
        private static IWebDriver driver;
        private static WebDriverWait wait;

        public static void UpdateUpcoming(string username)
        {
            options.AddArgument("--headless");
            Console.OutputEncoding = Encoding.UTF8;
            driver = new ChromeDriver(path, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            IWebElement schoolSelector = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("schoolSelector")));
            schoolSelector.SendKeys("תיכון אלון");
            driver.FindElement(By.Id("schoolSelector")).SendKeys(Keys.Enter);
            driver.FindElement(By.Id("usernameInput")).SendKeys(GetID(username));
            driver.FindElement(By.Id("passwordInput")).SendKeys(GetPassword(username));
            driver.FindElement(By.Id("submitButton")).Click();
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl("https://web.mashov.info/students/main/students/homework");
            IWebElement waitElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("mat-mdc-list-item")));
            List<IWebElement> gradeContainers = new List<IWebElement>(driver.FindElements(By.ClassName("mat-mdc-list-item-unscoped-content")));
            foreach (IWebElement containerElement in gradeContainers)
            {
                ExecuteScript("document.getElementsByClassName(\"mat-mdc-list-item-unscoped-content\")[0].children[0].children[1].children[2].innerText ");
                IWebElement childOne = containerElement.FindElements(By.XPath("*"))[0];
                IWebElement childTwo = childOne.FindElements(By.XPath("*"))[1];
                List<IWebElement> childThrees = new List<IWebElement>(childTwo.FindElements(By.XPath("*")));
                string date = childThrees[0].Text;
                string subject = childThrees[1].FindElements(By.TagName("strong"))[0].Text;
                subject = subject.Trim();
                string task = childThrees[1].Text.Replace(subject, "");
                task = task.Trim();

                if (task[0] == '-')
                {
                    task = task.Remove(0);
                }
                task += $" {childThrees[2].Text}";
                task.Trim();
                InsertUpcoming(username, task, subject, date);
            }
            driver.Close();
        }

        public static void InsertUpcoming(string username, string task, string subject, string date)
        {
            subject = subject.Replace("\'", "\'\'");
            date = date.Replace("\'", "\'\'");
            task = task.Replace("\'", "\'\'");
            string selectSql = $"SELECT * FROM Tupcoming WHERE username=N'{username}' and subject=N'{subject}' and task=N'{task}' and date=N'{date}'";
            if (!MyAdoHelper.IsExist(fileName, selectSql))
            {
                string sql = $"INSERT INTO Tupcoming(username, task, subject, date) VALUES(N'{username}', N'{task}', N'{subject}', N'{date}')";
                MyAdoHelper.DoQuery(fileName, sql);
            }
        }

        /// <summary>
        /// Empties the database
        /// </summary>
        public static void CleanUpcoming()
        {
            string sql = $"DELETE FROM Tupcoming";
            MyAdoHelper.DoQuery(fileName, sql);
        }

        public static void UpdateUpcoming()
        {
            CleanGrades();
            string selectSql = $"SELECT * FROM Tusers WHERE mashovId IS NOT NULL and mashovPassword IS NOT NULL";
            DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                UpdateUpcoming((string)table.Rows[i]["username"]);
            }
        }

        public static void UpdateGrades(string username)
        {
            options.AddArgument("--headless");
            Console.OutputEncoding = Encoding.UTF8;
            driver = new ChromeDriver(path, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            IWebElement schoolSelector = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("schoolSelector")));
            schoolSelector.SendKeys("תיכון אלון");
            driver.FindElement(By.Id("schoolSelector")).SendKeys(Keys.Enter);
            driver.FindElement(By.Id("usernameInput")).SendKeys(GetID(username));
            driver.FindElement(By.Id("passwordInput")).SendKeys(GetPassword(username));
            driver.FindElement(By.Id("submitButton")).Click();
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl("https://web.mashov.info/students/main/students/regularGrades");
            IWebElement waitElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("mat-mdc-list-item-unscoped-content")));
            List<IWebElement> gradeContainers = new List<IWebElement>(driver.FindElements(By.ClassName("mat-mdc-list-item-unscoped-content")));
            foreach (IWebElement containerElement in gradeContainers)
            {
                ExecuteScript("document.getElementsByClassName(\"mat-mdc-list-item-unscoped-content\")[0].children[1].children[0].children[1].innerText");
                ExecuteScript("document.getElementsByClassName(\"mat-mdc-list-item-unscoped-content\")[0].children[1].children[1].getElementsByClassName(\"mshv-larger-text\")[0].innerText");
                List<IWebElement> childOnes = new List<IWebElement>(containerElement.FindElements(By.XPath("*")));
                List<IWebElement> doubleTrouble = new List<IWebElement>(childOnes[1].FindElements(By.XPath("*")));
                List<IWebElement> detailElements = new List<IWebElement>(doubleTrouble[0].FindElements(By.XPath("*")));
                string task = detailElements[0].FindElement(By.TagName("strong")).Text;
                string date = detailElements[1].Text;
                string subject = detailElements[2].Text;
                string teacher = detailElements[3].Text;
                string grade;
                try
                {
                    grade = doubleTrouble[1].FindElements(By.ClassName("mshv-extra-large-text"))[0].Text;
                    if (grade.Length < 1)
                    {
                        grade = doubleTrouble[1].FindElements(By.ClassName("mshv-larger-text"))[0].Text;
                    }
                }
                catch (Exception ex)
                {
                    grade = doubleTrouble[1].FindElements(By.ClassName("mshv-larger-text"))[0].Text;

                }

                InsertGrade(username, subject, task, teacher, grade, date);

            }
            driver.Close();
        }
        /// <summary>
        /// Adds a grade to the database
        /// </summary>
        public static void InsertGrade(string username, string subject, string task, string teacher, string grade, string date)
        {
            subject = subject.Replace("\'", "\'\'");
            teacher = teacher.Replace("\'", "\'\'");
            date = date.Replace("\'", "\'\'");
            task = task.Replace("\'", "\'\'");
            string selectSql = $"SELECT * FROM Tgrades WHERE username=N'{username}' and subject=N'{subject}' and task=N'{task}' and teacher=N'{teacher}' and grade='{grade}' and date='{date}'";
            if (!MyAdoHelper.IsExist(fileName, selectSql))
            {
                string sql = $"INSERT INTO Tgrades(username, subject, task, teacher, grade, date) VALUES(N'{username}', N'{subject}', N'{task}', N'{teacher}', N'{grade}', '{date}')";
                MyAdoHelper.DoQuery(fileName, sql);
            }
        }


        public static void UpdateGrades()
        {
            CleanGrades();
            string selectSql = $"SELECT * FROM Tusers WHERE mashovId IS NOT NULL and mashovPassword IS NOT NULL";
            DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                UpdateGrades((string)table.Rows[i]["username"]);
            }
        }

        /// <summary>
        /// Empties the database
        /// </summary>
        public static void CleanGrades()
        {
            string sql = $"DELETE FROM Tgrades";
            MyAdoHelper.DoQuery(fileName, sql);
        }

        public static string GetID(string username)
        {
            string selectSql = $"SELECT * FROM Tusers WHERE username=N'{username}'";
            DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            string mashovId = (table.Rows[0]["mashovId"] != DBNull.Value) ? (string)table.Rows[0]["mashovId"] : null;
            return mashovId;
        }

        public static string GetPassword(string username)
        {
            string selectSql = $"SELECT * FROM Tusers WHERE username=N'{username}'";
            DataTable table = MyAdoHelper.ExecuteDataTable(fileName, selectSql);
            string mashovId = (table.Rows[0]["mashovPassword"] != DBNull.Value) ? (string)table.Rows[0]["mashovPassword"] : null;
            return mashovId;
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
