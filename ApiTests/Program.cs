using System.Collections.Generic;
using System.IO;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class ShahafServer
{

    private IWebDriver driver;
    private string url;
    public ShahafServer()
    {
        string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        this.driver = new ChromeDriver($"{path}/drivers/");
        ChromeOptions options = new ChromeOptions();
        this.url = "https://alon.iscool.co.il/default.aspx";
        getSchedule("י6");
    }

    /// <summary>
    /// Gets the schedule of a grade
    /// </summary>
    /// <param name="grade"></param>
    public void getSchedule(string grade)
    {
        driver.Navigate().GoToUrl(url);
        ClickElement(By.LinkText("מערכת שעות"));
        ClickElement(By.XPath($"//a[text()=='${grade}'"));
        IWebElement table = driver.FindElement(By.ClassName("TTTable"));
        List<IWebElement> rows = new List<IWebElement>(table.FindElements(By.TagName("tr")));
        for (int i = 1; i < rows.Count; i++)
        {
            List<IWebElement> cells = new List<IWebElement>(rows[i].FindElements(By.TagName("td")));
            for (int j = 1; j < cells.Count; i++)
            {
                List<IWebElement> subjectPerHour = new List<IWebElement>(cells[i].FindElements(By.ClassName("TTLesson")));
                foreach (var subject in subjectPerHour)
                {
                    Console.WriteLine(subject.Text);
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

    public object ExecuteScript(string script)
    {
        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
        return jsExecutor.ExecuteScript(script);
    }




}