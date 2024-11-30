using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Collections;
using System.Linq;

namespace WebdriverClass
{
    internal class WikipediaPage : BasePage
    {
        public WikipediaPage(IWebDriver driver) : base(driver) { }

        public void OpenWikipedia()
        {
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.wikipedia.org");
        }
    }
    public class SearchWidgets
    {
        private IWebDriver driver;


        public SearchWidgets(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void SearchFor(string searchTerm)
        {
            driver.FindElement(By.Id("searchInput")).Clear();
            driver.FindElement(By.Id("searchInput")).SendKeys(searchTerm);
            try
            {
                driver.FindElement(By.CssSelector("form#search-form")).Submit();
            }
            catch (StaleElementReferenceException ex)
            {
                driver.FindElement(By.CssSelector("form#search-form")).Submit();
            }
        }
    }
    public class ResultsWidgets
    {
        private IWebDriver driver;
        public ResultsWidgets(IWebDriver driver)
        {
            this.driver = driver;
        }
        public string GetActualTitle()
        {
            return driver.FindElement(By.ClassName("videojatekinfobox")).Text;
        }
    }

    [TestClass]
    public class BadandoPageObjectTest : TestBase
    {
        private SearchWidgets searchWidget;
        private ResultsWidgets resultsWidget;
        private WikipediaPage wikipediapage;
        [Test, TestCaseSource("TestData")]
        public void SearchTest(string search, string desc)
        {
            searchWidget = new SearchWidgets(Driver);
            resultsWidget = new ResultsWidgets(Driver);
            wikipediapage = new WikipediaPage(Driver);
            wikipediapage.OpenWikipedia();
            searchWidget.SearchFor(search);
            string actualTitle = resultsWidget.GetActualTitle();
            NUnit.Framework.Assert.True(desc.Equals(actualTitle));
        }
        static IEnumerable TestData()
        {
            var projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));

            var filePath = System.IO.Path.Combine(projectRoot, "test.xml");

            var doc = XElement.Load(filePath);
            return
                from vars in doc.Descendants("testData")
                let search = vars.Attribute("search").Value
                let desc = vars.Attribute("desc").Value
                select new object[] { search, desc };
        }
    }
}
