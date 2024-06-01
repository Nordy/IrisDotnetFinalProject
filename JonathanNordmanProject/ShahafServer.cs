using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JonathanNordmanProject
{

    public class ShahafServer
    {
        private IWebDriver driver;
        
        
        public ShahafServer()
        {
            Setup();

        }

        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver($"{path}/drivers/");
            // driver options

        }


    }
}