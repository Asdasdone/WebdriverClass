using OpenQA.Selenium;

namespace WebdriverClass
{
    class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }
    }
}
