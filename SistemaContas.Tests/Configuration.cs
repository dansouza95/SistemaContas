﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SistemaContas.Tests
{
    [Binding]
    public class Configuration
    {
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get { return driver ?? (driver = new ChromeDriver()); }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Driver.Close();
            Driver.Quit();
            driver = null;
        }

    }
}
