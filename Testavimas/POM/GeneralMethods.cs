using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Testavimas.POM
{
    internal class GeneralMethods
    {
        IWebDriver driver;

        public GeneralMethods(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement WaitUntilMethod(string xPath, IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromSeconds(0.5);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(d => d.FindElement(By.XPath(xPath)));
        }

        public void ScrollToElement(string xpath)
        {
            IWebElement el = driver.FindElement(By.XPath(xpath));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", el);

        }

        public void ClosePopUp(string xPath)

        {
            try
            {
                By element = By.XPath(xPath);
                driver.FindElement(element).Click();
            }
            catch (NoSuchElementException ex)
            {
                
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
