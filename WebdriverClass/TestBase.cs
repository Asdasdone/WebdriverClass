using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebdriverClass
{
    [TestFixture]
    public class TestBase
    {
        IWebDriver driver;

        public WebDriverWait Wait { get; set; }

        public IWebDriver Driver
        {
            get
            { return driver; }
            set
            {
                driver.Quit();
                driver = value;
            }
        }

        [SetUp]
        protected void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--lang=hu");

            driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        [TearDown]
        protected void Teardown()
        {
            driver.Quit();
        }
    }
}
